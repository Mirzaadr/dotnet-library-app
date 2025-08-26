import { Routes, Route } from "react-router-dom";
import Home from "@/pages/public/Home";
import Library from "@/pages/public/Library";
import BookDetail from "@/pages/public/BookDetail";
import Profile from "@/pages/public/Profile";
import BorrowHistory from "@/pages/public/BorrowHistory";
import PublicLayout from "@/pages/public/common/Layout";
import NotFound from "@/pages/NotFound";

const PublicRoutes = () => (
  <PublicLayout>
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/library" element={<Library />} />
      <Route path="/books/:id" element={<BookDetail />} />
      <Route path="/profile" element={<Profile />} />
      <Route path="/history" element={<BorrowHistory />} />

      {/* Fallback for unmatched routes */}
      <Route path="*" element={<NotFound />} />
    </Routes>
  </PublicLayout>
);

export default PublicRoutes;
