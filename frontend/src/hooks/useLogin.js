// Import hooks, navigation, and context
import { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { toast } from "react-toastify";
import { useAuth } from "../context/AuthContext";
import { validateEmail, validatePassword } from "../utils/validator";

// Custom hook for login functionality
export default function useLogin() {
    // State for email, password, errors, and submission status
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [errors, setErrors] = useState({});
    const [submitting, setSubmitting] = useState(false);

    // Use login function from auth context and navigation hooks
    const { login } = useAuth();
    const navigate = useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || "/";

    // Validate email and password inputs
    const validate = () => {
        const newErrors = {};
        // Check email and password
        if (!validateEmail(email)) 
            newErrors.email = "Please enter a valid email address.";
        if (!validatePassword(password))
            newErrors.password = "Password must be 8-20 characters with an uppercase, lowercase, digit and special character.";
        
        // Update errors state and return validation result
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    // Handle submit event for login form
    const handleSubmit = async (e) => {
        e.preventDefault();

        // Use validate function to check inputs before proceeding
        if (!validate()) return;

        // Set submitting state to true and attempt login
        setSubmitting(true);
        try {
            await login(email.trim(), password);
            navigate(from, { replace: true });
        } catch (err) {
            toast.error(err.response?.data || "Invalid email or password.");
        } finally {
            setSubmitting(false);
        }
    };

    // Return state and handlers for use in components
    return { email, setEmail, password, setPassword, errors, submitting, handleSubmit };
}