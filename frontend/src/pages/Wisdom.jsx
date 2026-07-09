// Import hooks, components, form, confirm pop-up and styles
import Navbar from "../components/Navbar";
import WisdomCard from "../components/WisdomCard";
import WisdomFormModal from "../components/WisdomFormModal";
import ConfirmModal from "../components/ConfirmModal";
import useWisdom from "../hooks/useWisdom";
import "../styles/Wisdom.css";

// Wisdom component
export default function Wisdom() {
    // Destructure state and handlers from custom hook
    const {
        wisdoms, loading,
        isFormOpen, setIsFormOpen,
        isConfirmOpen, setIsConfirmOpen,
        selectedWisdom, formMode,
        openAddForm, openEditForm, openDeleteConfirm,
        handleFormSubmit, handleDeleteConfirm,
    } = useWisdom();

    // Render wisdom page
    return (
        <div className="wisdom-page">
            <Navbar />
            <div className="wisdom-container">
                <div className="wisdom-header">
                    <h1>Your Wisdoms</h1>
                    <button className="btn-primary" onClick={openAddForm}>+ Add Wisdom</button>
                </div>

                {loading ? (
                    <p className="wisdom-status">Loading...</p>
                ) : wisdoms.length === 0 ? (
                    <p className="wisdom-status">No wisdoms yet. Share your first one!</p>
                ) : (
                    <div className="wisdom-grid">
                        {wisdoms.map((w, index) => (
                            <WisdomCard
                                key={w.id}
                                wisdom={w}
                                index={index}
                                onEdit={openEditForm}
                                onDelete={openDeleteConfirm}
                            />
                        ))}
                    </div>
                )}
            </div>

            <WisdomFormModal
                isOpen={isFormOpen}
                onClose={() => setIsFormOpen(false)}
                onSubmit={handleFormSubmit}
                initialContent={formMode === "edit" ? selectedWisdom?.content : ""}
                mode={formMode}
            />

            <ConfirmModal
                isOpen={isConfirmOpen}
                onClose={() => setIsConfirmOpen(false)}
                onConfirm={handleDeleteConfirm}
                message="Are you sure you want to delete this wisdom? This action cannot be undone."
            />
        </div>
    );
}