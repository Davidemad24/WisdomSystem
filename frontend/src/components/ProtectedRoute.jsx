// Import libraries and hooks
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

// Component for protecting routes that require authentication
export default function ProtectedRoute() {
    // Get authentication status and loading state from context, and current location for redirecting after login
    const { isAuthenticated, loading } = useAuth();
    const location = useLocation();

    // Display loading indicator while checking authentication status
    if (loading) return <div className="page-loading">Loading...</div>;

    // Check authentication
    return isAuthenticated ? (
        <Outlet />
    ) : (
        <Navigate to="/login" state={{ from: location }} replace />
    );
}