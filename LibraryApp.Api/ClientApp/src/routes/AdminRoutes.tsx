import { Routes, Route, Navigate } from "react-router-dom";
// import { useAuth } from "@/context/AuthContext";
import Dashboard from "@/pages/admin/Dashboard";
import Users from "@/pages/admin/Users";
import Books from "@/pages/admin/Books";
import BookForm from "@/pages/admin/BookForm";
import Records from "@/pages/admin/Records";
import AdminProfile from "@/pages/admin/AdminProfile";
import AdminLayout from "@/pages/admin/common/Layout";
import NotFound from "@/pages/NotFound";

const RequireAdmin = ({ children }: { children: React.ReactNode }) => {
  // const { user } = useAuth();
  const user = { role: "admin" };
  return user?.role === "admin" ? children : <Navigate to="/" />;
};

const AdminRoutes = () => (
  <RequireAdmin>
    <AdminLayout>
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/users" element={<Users />} />
        <Route path="/books" element={<Books />} />
        <Route path="/books/new" element={<BookForm />} />
        <Route path="/books/:id/edit" element={<BookForm />} />
        <Route path="/borrow-records" element={<Records />} />
        <Route path="/profile" element={<AdminProfile />} />

        {/* Fallback for unmatched routes */}
        <Route path="/*" element={<NotFound />} />
      </Routes>
    </AdminLayout>
  </RequireAdmin>
);

export default AdminRoutes;
