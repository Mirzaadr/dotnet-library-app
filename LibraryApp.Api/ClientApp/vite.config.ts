import path from "path";
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";
import { execSync } from "child_process";

let backendUrl = "http://localhost:3001"; // default

try {
  backendUrl = execSync("node ./read-port.cjs").toString().trim();
  console.log(`✔ Backend URL detected: ${backendUrl}`);
} catch {
  console.warn(
    "⚠ Failed to load backend URL from launchSettings.json. Using default."
  );
}

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  server: {
    // port: 3002,
    proxy: {
      "/api": {
        target: "http://localhost:5001", //backendUrl,
        changeOrigin: true,
        // secure: backendUrl.startsWith("https://") ? false : true,
        secure: false,
      },
    },
  },
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
  build: {
    outDir: "../wwwroot",
    emptyOutDir: true,
  },
});
