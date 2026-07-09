// Import libraries
import axios from "axios";

// Base URL for the API, either from environment variables or a default value
const BASE_URL = import.meta.env.VITE_API_BASE_URL || "http://localhost:5123";

// Create an Axios instance with the base URL and default headers
const apiClient = axios.create({
    baseURL: BASE_URL,
    headers: { "Content-Type": "application/json" },
});

// Plain instance with no interceptors — used only for the refresh call
// to avoid an infinite refresh loop.
const plainAxios = axios.create({
    baseURL: BASE_URL,
    headers: { "Content-Type": "application/json" },
});


// Request interceptor to add the Authorization header with the token from session storage
apiClient.interceptors.request.use((config) => {
    const token = sessionStorage.getItem("token");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

// variables to manage token refresh state and subscribers
let isRefreshing = false;
let refreshSubscribers = [];


const subscribeTokenRefresh = (cb) => refreshSubscribers.push(cb);
const onRefreshed = (token) => {
    refreshSubscribers.forEach((cb) => cb(token));
    refreshSubscribers = [];
};

// Function to clear session data and redirect to the login page
function clearSessionAndRedirect() {
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("refreshToken");
    sessionStorage.removeItem("expiresOn");
    sessionStorage.removeItem("user");
    window.location.href = "/login";
}

// Check refresh token expiration and existence before making a request
apiClient.interceptors.response.use(
    (response) => response,
        async (error) => {
            const originalRequest = error.config;

            // Check unauthorized error and if the request has not been retried yet
            if (error.response?.status === 401 && !originalRequest._retry) {
                // Check refresh token existence
                const refreshToken = sessionStorage.getItem("refreshToken");
                if (!refreshToken) {
                    clearSessionAndRedirect();
                    return Promise.reject(error);
                }

                // Check if a token refresh is already in progress
                if (isRefreshing) {
                    return new Promise((resolve) => {
                    subscribeTokenRefresh((newToken) => {
                        originalRequest.headers.Authorization = `Bearer ${newToken}`;
                        resolve(apiClient(originalRequest));
                    });
                    });
                }

                // Mark the request as retried and set the refreshing flag
                originalRequest._retry = true;
                isRefreshing = true;

                try {
                    // Send refresh token as JSON string in the request body
                    const { data } = await plainAxios.post(
                        "/RefreshToken/RefreshToken",
                        JSON.stringify(refreshToken)
                    );

                    // Add refreshed token to session storage and reset the refreshing state
                    sessionStorage.setItem("token", data.token);
                    sessionStorage.setItem("refreshToken", data.refreshToken);
                    sessionStorage.setItem("expiresOn", data.expiresOn);
                    isRefreshing = false;
                    onRefreshed(data.token);

                    // Retry the original request with the new token
                    originalRequest.headers.Authorization = `Bearer ${data.token}`;
                    return apiClient(originalRequest);
                } catch (refreshError) {
                    // Clear session and redirect to login
                    isRefreshing = false;
                    clearSessionAndRedirect();
                    return Promise.reject(refreshError);
                }
            }

        // Rreject the promise 
        return Promise.reject(error);
    }
);

export default apiClient;