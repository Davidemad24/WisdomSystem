// Import apiClient
import apiClient from "./apiClient";

// Get user wisdoms API call
export const getUserWisdoms = (userId) =>
    apiClient.get(`/Wisdom/GetUserWisdoms?userId=${userId}`);

// Add wisdom API call
export const addWisdom = (content, userId) =>
    apiClient.post("/Wisdom/AddWisdom", { content, userId });

// Update wisdom API call
export const updateWisdom = (id, content, userId) =>
    apiClient.patch("/Wisdom/UpdateWisdom", { id, content, userId });

// Delete wisdom API call
export const deleteWisdom = (id) =>
    apiClient.delete(`/Wisdom/DeleteWisdom?id=${id}`);