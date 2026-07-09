// Import styles and components
import Modal from "./Modal";
import "../styles/ConfirmModal.css";

// Confirm pop-up component
export default function ConfirmModal({ isOpen, onClose, onConfirm, message }) {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Confirm Deletion">
            <p className="confirm-message">{message}</p>
            <div className="confirm-actions">
                <button className="btn-secondary" onClick={onClose}>Cancel</button>
                <button className="btn-danger" onClick={onConfirm}>Delete</button>
            </div>
        </Modal>
    );
}