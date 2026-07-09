// Import dependencies, hooks, and styles
import { Link } from "react-router-dom";
import useForgetPassword from "../hooks/useForgetPassword";
import "../styles/Auth.css";

// ForgetPassword component
export default function ForgetPassword() {
    // Destructure state and handlers from custom hook
    const {
        step, email, setEmail, code, setCode, password, setPassword,
        confirmPassword, setConfirmPassword, errors, submitting,
        handleSendEmail, handleResetPassword,
    } = useForgetPassword();

    // Render forget password form based on current step
    return (
        <div className="auth-page">
            <div className="auth-card">
                <h1 className="auth-title">Reset Password</h1>
                <p className="auth-subtitle">
                    {step === 1
                        ? "Enter your email to receive a verification code"
                        : "Enter the code sent to your email and your new password"}
                </p>

                {step === 1 ? (
                    <form onSubmit={handleSendEmail} noValidate>
                        <div className="form-group">
                            <label htmlFor="email">Email</label>
                            <input id="email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
                            {errors.email && <span className="field-error">{errors.email}</span>}
                        </div>

                        <button type="submit" className="btn-primary btn-block" disabled={submitting}>
                            {submitting ? "Sending..." : "Send Verification Code"}
                        </button>
                    </form>
                ) : (
                    <form onSubmit={handleResetPassword} noValidate>
                        <div className="form-group">
                            <label htmlFor="code">Verification Code</label>
                            <input id="code" type="text" value={code} onChange={(e) => setCode(e.target.value)} />
                            {errors.code && <span className="field-error">{errors.code}</span>}
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">New Password</label>
                            <input
                                id="password"
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                            {errors.password && <span className="field-error">{errors.password}</span>}
                        </div>
                        <div className="form-group">
                            <label htmlFor="confirmPassword">Confirm New Password</label>
                            <input
                                id="confirmPassword"
                                type="password"
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                            />
                            {errors.confirmPassword && <span className="field-error">{errors.confirmPassword}</span>}
                        </div>
                        <button type="submit" className="btn-primary btn-block" disabled={submitting}>
                            {submitting ? "Resetting..." : "Reset Password"}
                        </button>
                    </form>
                )}
                <p className="auth-switch">
                    Remembered your password? <Link to="/login">Log in</Link>
                </p>
            </div>
        </div>
    );
}