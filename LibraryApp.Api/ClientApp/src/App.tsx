// import { useState, useEffect } from "react";
// import reactLogo from "./assets/react.svg";
// import viteLogo from "/vite.svg";
// import "./App.css";

import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import PublicRoutes from "./routes/PublicRoutes";
import AdminRoutes from "./routes/AdminRoutes";
// import AuthRoutes from "./routes/AuthRoutes";
import LoginPage from "@/pages/auth/Login";
import RegisterPage from "@/pages/auth/Register";
import AuthLayout from "@/pages/auth/AuthLayout";
// import NotFound from "./pages/NotFound";

const App = () => {
  return (
    <Router>
      <Routes>
        {/* Auth Routes */}
        {/* <Route path="/login/*" element={<AuthRoutes />} /> */}
        {/* <Route path="/register/*" element={<AuthRoutes />} /> */}
        {/* Auth routes */}
        <Route element={<AuthLayout />}>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
        </Route>

        {/* Public Routes */}
        <Route path="/*" element={<PublicRoutes />} />

        {/* Admin Routes */}
        <Route path="/admin/*" element={<AdminRoutes />} />

        {/* Fallback for unmatched routes */}
        {/* <Route path="*" element={<NotFound />} /> */}
      </Routes>
    </Router>
  );
};

export default App;
