import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "/Authentication": "https://localhost:5123",
      "/RefreshToken": "https://localhost:5123",
      "/Wisdom": "https://localhost:5123",
    },
  },
});