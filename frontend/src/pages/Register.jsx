// Import dependencies, services, and styles
import { Link } from "react-router-dom";
import useRegister from "../hooks/useRegister";
import "../styles/Auth.css";

export default function Register() {
    // Use custom hook for registration functionality
    const {
        name, setName, email, setEmail, password, setPassword,
        confirmPassword, setConfirmPassword, errors, submitting, handleSubmit,
    } = useRegister();

    // Render registration form
    return (
        <div className="auth-page">
            <div className="auth-card">
                <h1 className="auth-title">Join Wisdom</h1>
                <p className="auth-subtitle">Create an account to start collecting wisdom</p>
                <form onSubmit={handleSubmit} noValidate>
                    <div className="form-group">
                        <label htmlFor="name">Full Name (Quadruple)</label>
                        <input
                            id="name"
                            type="text"
                            placeholder="e.g. John Michael David Smith"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                        {errors.name && <span className="field-error">{errors.name}</span>}
                    </div>
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
                    <div className="form-group">
                        <label htmlFor="confirmPassword">Confirm Password</label>
                        <input
                            id="confirmPassword"
                            type="password"
                            value={confirmPassword}
                            onChange={(e) => setConfirmPassword(e.target.value)}
                        />
                        {errors.confirmPassword && <span className="field-error">{errors.confirmPassword}</span>}
                    </div>
                    <button type="submit" className="btn-primary btn-block" disabled={submitting}>
                        {submitting ? "Creating account..." : "Sign Up"}
                    </button>
                </form>
                <p className="auth-switch">Already have an account? <Link to="/login">Log in</Link></p>
            </div>
        </div>
    );
}