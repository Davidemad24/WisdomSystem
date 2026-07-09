// Import dependencies, services, and styles
import { Link } from "react-router-dom";
import useLogin from "../hooks/useLogin";
import "../styles/Auth.css";

// Login page component
export default function Login() {
    // Use custom login hook to manage state and handle form submission
    const { email, setEmail, password, setPassword, errors, submitting, handleSubmit } = useLogin();

    // Login render
    return (
        <div className="auth-page">
            <div className="auth-card">
                <h1 className="auth-title">Welcome Back</h1>
                <p className="auth-subtitle">Log in to continue your journey of wisdom</p>
                <form onSubmit={handleSubmit} noValidate>
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input id="email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
                        {errors.email && <span className="field-error">{errors.email}</span>}
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input
                            id="password"
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        {errors.password && <span className="field-error">{errors.password}</span>}
                    </div>
                    <Link to="/forget-password" className="forgot-link">Forgot Password?</Link>
                    <button type="submit" className="btn-primary btn-block" disabled={submitting}>
                        {submitting ? "Logging in..." : "Log In"}
                    </button>
                </form>
                <p className="auth-switch">
                    Don't have an account? <Link to="/register">Sign up</Link>
                </p>
            </div>
        </div>
    );
}