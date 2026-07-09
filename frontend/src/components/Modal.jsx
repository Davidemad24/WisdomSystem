// Import styles
import "../styles/Modal.css";

// Modal component
export default function Modal({ isOpen, onClose, title, children }) {
    // Check rendring state
    if (!isOpen) return null;

    // Render modal
    return (
        <div className="modal-overlay" onClick={onClose}>
            <div className="modal-box" onClick={(e) => e.stopPropagation()}>
                <div className="modal-header">
                    <h2>{title}</h2>
                    <button className="modal-close" onClick={onClose} aria-label="Close">
                        &times;
                    </button>
                </div>
                <div className="modal-body">{children}</div>
            </div>
        </div>
    );
}