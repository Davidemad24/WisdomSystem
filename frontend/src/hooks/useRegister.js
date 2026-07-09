// Import dependencies, hooks, context, and validators
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { useAuth } from "../context/AuthContext";
import { validateEmail, validatePassword, validateQuadrupleName } from "../utils/validator";

// Custom hook for registration functionality
export default function useRegister() {
    // Set statuses, inputs, and errors
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [errors, setErrors] = useState({});
    const [submitting, setSubmitting] = useState(false);

    // Use register function from auth context and navigation hook
    const { register } = useAuth();
    const navigate = useNavigate();

    // Validate inputs for registration form
    const validate = () => {
        const newErrors = {};

        // Validate name, email, password, and confirmPassword
        if (!validateQuadrupleName(name))
            newErrors.name = "Enter your full quadruple name (4 names separated by spaces).";
        if (!validateEmail(email)) 
            newErrors.email = "Please enter a valid email address.";
        if (!validatePassword(password))
            newErrors.password =
                "Password must be 8-20 characters with an uppercase, lowercase, digit and special character.";
        if (password !== confirmPassword) 
            newErrors.confirmPassword = "Passwords do not match.";

        // Update errors state and return validation result
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    // Handle submition
    const handleSubmit = async (e) => {
        // Prevent default form submission and validate inputs
        e.preventDefault();
        if (!validate()) return;

        // Set submitting state to true and attempt registration
        setSubmitting(true);
        try {
            // confirmPassword is intentionally never sent to the API
            await register(name.trim(), email.trim(), password);
            navigate("/", { replace: true });
        } catch (err) {
            toast.error(err.response?.data || "Registration failed. Please try again.");
        } finally {
            setSubmitting(false);
        }
    };

    // Return state and handlers for use in components
    return {
        name, setName, email, setEmail, password, setPassword,
        confirmPassword, setConfirmPassword, errors, submitting, handleSubmit,
    };
}