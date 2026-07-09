// Import styles and context
import { useAuth } from "../context/AuthContext";
import "../styles/Navbar.css";

// Navbar component
export default function Navbar() {
    // Use user and logout function from auth context
    const { user, logout } = useAuth();

    // Navbar render
    return (
        <nav className="navbar">
            <span className="navbar-brand">🦉 Wisdom</span>
            <div className="navbar-right">
                <span className="navbar-user">Hi, {user?.name?.split(" ")[0]}</span>
                <button className="btn-secondary" onClick={logout}>Logout</button>
            </div>
        </nav>
    );
}