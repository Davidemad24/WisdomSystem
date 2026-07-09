// Import hooks, styles and components
import { useState, useEffect } from "react";
import Modal from "./Modal";
import "../styles/WisdomFormModal.css";

// Wisdom form component
export default function WisdomFormModal({ isOpen, onClose, onSubmit, initialContent = "", mode }) {
    // State for content, error message and submission status
    const [content, setContent] = useState(initialContent);
    const [error, setError] = useState("");
    const [submitting, setSubmitting] = useState(false);

    // Reset content and error when modal opens or initialContent changes
    useEffect(() => {
        setContent(initialContent);
        setError("");
    }, [initialContent, isOpen]);

    // Handle form submission
    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!content.trim()) {
            setError("Wisdom content is required.");
            return;
        }
        setSubmitting(true);
        try {
            await onSubmit(content.trim());
            onClose();
        } catch (err) {
            setError(err.response?.data || "Something went wrong. Please try again.");
        } finally {
            setSubmitting(false);
        }
    };

    // Form
    return (
        <Modal isOpen={isOpen} onClose={onClose} title={mode === "add" ? "Add Wisdom" : "Update Wisdom"}>
            <form className="wisdom-form" onSubmit={handleSubmit}>
                <textarea
                    rows={5}
                    maxLength={200}
                    value={content}
                    onChange={(e) => setContent(e.target.value)}
                    placeholder="Share a piece of wisdom..."
                />
                {error && <div className="field-error">{error}</div>}
                <div className="wisdom-form-actions">
                    <button type="button" className="btn-secondary" onClick={onClose}>Cancel</button>
                    <button type="submit" className="btn-primary" disabled={submitting}>
                        {submitting ? "Saving..." : "Save"}
                    </button>
                </div>
            </form>
        </Modal>
    );
}