// Import apiClient
import apiClient from "./apiClient";

// Login API call
export const login = (email, password) =>
    apiClient.post("/Authentication/Login", { email, password });

// Register API call
export const register = (name, email, password) =>
    apiClient.post("/Authentication/Register", { name, email, password });

// Send verification code API call
export const sendVerificationEmail = (email) =>
    apiClient.post(`/Authentication/SendVerificationEmail?email=${encodeURIComponent(email)}`);

// Reset password API call
export const resetPassword = (email, code, password) =>
    apiClient.patch("/Authentication/ResetPassword", { email, code, password });

// Logout API call
export const logout = (refreshToken) =>
    apiClient.post("/Authentication/Logout", JSON.stringify(refreshToken));