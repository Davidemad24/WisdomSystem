// Import hooks and api functions
import { createContext, useContext, useState, useEffect } from "react";
import * as authApi from "../apis/authApis";

// Create AuthContext
const AuthContext = createContext(null);

// Create AuthProvider component
export function AuthProvider({ children }) {
    // State to hold user and loading status
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    // Load user and token from session storage, and set loading to false after checking
    useEffect(() => {
        const storedUser = sessionStorage.getItem("user");
        const token = sessionStorage.getItem("token");
        if (storedUser && token) setUser(JSON.parse(storedUser));
        setLoading(false);
    }, []);

    // Persist session function that saves user data in session storage and updates user state
    const persistSession = (data) => {
        const userData = { id: data.id, name: data.name };
        sessionStorage.setItem("token", data.token);
        sessionStorage.setItem("refreshToken", data.refreshToken);
        sessionStorage.setItem("expiresOn", data.expiresOn);
        sessionStorage.setItem("user", JSON.stringify(userData));
        setUser(userData);
        return userData;
    };

    // Login function
    const login = async (email, password) => {
        const { data } = await authApi.login(email, password);
        return persistSession(data);
    };

    // Registter function
    const register = async (name, email, password) => {
        const { data } = await authApi.register(name, email, password);
        return persistSession(data); // auto-login on register
    };

    // Logout function
    const logout = async () => {
        const refreshToken = sessionStorage.getItem("refreshToken");
        try {
            if (refreshToken) await authApi.logout(refreshToken);
        } catch {
            // clear locally regardless of API result
        } finally {
            sessionStorage.removeItem("token");
            sessionStorage.removeItem("refreshToken");
            sessionStorage.removeItem("expiresOn");
            sessionStorage.removeItem("user");
            setUser(null);
        }
    };

    // Return the AuthContext.Provider with the user, isAuthenticated, loading, login, register, and logout functions
    const value = { user, isAuthenticated: !!user, loading, login, register, logout };
    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

// eslint-disable-next-line react-refresh/only-export-components
export function useAuth() {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used within an AuthProvider");
    return ctx;
}