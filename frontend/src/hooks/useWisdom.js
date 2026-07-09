// Import dependencies, hooks, context, and API functions
import { useState, useEffect, useCallback } from "react";
import { toast } from "react-toastify";
import { useAuth } from "../context/AuthContext";
import * as wisdomApi from "../apis/wisdomApis";

// Custom hook for managing wisdoms
export default function useWisdom() {
    // Get user from auth context and set initial states
    const { user } = useAuth();
    const [wisdoms, setWisdoms] = useState([]);
    const [loading, setLoading] = useState(true);

    // Forms and confirmation states
    const [isFormOpen, setIsFormOpen] = useState(false);
    const [isConfirmOpen, setIsConfirmOpen] = useState(false);
    const [selectedWisdom, setSelectedWisdom] = useState(null);
    const [formMode, setFormMode] = useState("add");

    // Fetch user's wisdoms from API
    const fetchWisdoms = useCallback(async () => {
        // Set loading state
        setLoading(true);

        // Try to fetch wisdoms and handle errors
        try {
            const { data } = await wisdomApi.getUserWisdoms(user.id);
            setWisdoms(data);
        } catch {
            toast.error("Failed to load your wisdoms.");
        } finally {
            setLoading(false);
        }
    }, [user.id]);

    // Run fetchWisdoms on component mount and when user.id changes
    useEffect(() => {
        fetchWisdoms();
    }, [fetchWisdoms]);

    // Open add form modal
    const openAddForm = () => {
        setFormMode("add");
        setSelectedWisdom(null);
        setIsFormOpen(true);
    };

    // Open edit form modal with selected wisdom
    const openEditForm = (wisdom) => {
        setFormMode("edit");
        setSelectedWisdom(wisdom);
        setIsFormOpen(true);
    };

    // Open delete confirmation modal with selected wisdom
    const openDeleteConfirm = (wisdom) => {
        setSelectedWisdom(wisdom);
        setIsConfirmOpen(true);
    };

    // Handle form submission for adding or editing wisdom
    const handleFormSubmit = async (content) => {
        if (formMode === "add") {
            await wisdomApi.addWisdom(content, user.id);
            toast.success("Wisdom added successfully.");
        } else {
            await wisdomApi.updateWisdom(selectedWisdom.id, content, user.id);
            toast.success("Wisdom updated successfully.");
        }
        fetchWisdoms();
    };

    // Handle deletion of selected wisdom
    const handleDeleteConfirm = async () => {
        try {
            await wisdomApi.deleteWisdom(selectedWisdom.id);
            toast.success("Wisdom deleted successfully.");
            setIsConfirmOpen(false);
            fetchWisdoms();
        } catch {
            toast.error("Failed to delete wisdom.");
        }
    };

    // Return states and handlers for use in components
    return {
        wisdoms, loading,
        isFormOpen, setIsFormOpen,
        isConfirmOpen, setIsConfirmOpen,
        selectedWisdom, formMode,
        openAddForm, openEditForm, openDeleteConfirm,
        handleFormSubmit, handleDeleteConfirm,
    };
}