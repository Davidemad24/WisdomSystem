// Import dependencies, hooks, context, api and validators
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { validateEmail, validatePassword, validateCode } from "../utils/validator";
import * as authApi from "../apis/authApis";

// Custom hook for handling forget password functionality
export default function useForgetPassword() {
    // Set steps, inputs, errors, and submitting state
    const [step, setStep] = useState(1); // 1 = email, 2 = code + new password
    const [email, setEmail] = useState("");
    const [code, setCode] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [errors, setErrors] = useState({});
    const [submitting, setSubmitting] = useState(false);

    // Set navigation hook
    const navigate = useNavigate();

    // Handle sending verification email
    const handleSendEmail = async (e) => {
        // Prevent default form submission and validate email
        e.preventDefault();
        if (!validateEmail(email)) {
            setErrors({ email: "Please enter a valid email address." });
            return;
        }

        // Clear errors, set submitting state, and attempt to send verification email
        setErrors({});
        setSubmitting(true);
        try {
            const response = await authApi.sendVerificationEmail(email.trim());
            if (response.status === 200) {
                toast.success(response.data || "Verification code sent to your email.");
                setStep(2);
            }
        } catch (err) {
            toast.error(err.response?.data || "Failed to send verification code.");
        } finally {
            setSubmitting(false);
        }
    };

    const handleResetPassword = async (e) => {
        // Prevent default form submission and validate inputs
        e.preventDefault();
        const newErrors = {};

        // Validate email, code, password, and confirmPassword
        if (!validateCode(code)) 
            newErrors.code = "Please enter a valid 6-digit code.";
        if (!validatePassword(password))
            newErrors.password =
                "Password must be 8-20 characters with an uppercase, lowercase, digit and special character.";
        if (password !== confirmPassword) 
            newErrors.confirmPassword = "Passwords do not match.";

        // Update errors state and return
        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        // Clear errors, set submitting state, and attempt to reset password
        setErrors({});
        setSubmitting(true);
        try {
            // confirmPassword is intentionally never sent to the API
            const response = await authApi.resetPassword(email.trim(), Number(code), password);
            toast.success(response.data || "Password reset successfully.");
            navigate("/login", { replace: true });
        } catch (err) {
            toast.error(err.response?.data || "Failed to reset password.");
        } finally {
            setSubmitting(false);
        }
    };

    // Return state and handlers for use in components
    return {
        step, email, setEmail, code, setCode, password, setPassword,
        confirmPassword, setConfirmPassword, errors, submitting,
        handleSendEmail, handleResetPassword,
    };
}