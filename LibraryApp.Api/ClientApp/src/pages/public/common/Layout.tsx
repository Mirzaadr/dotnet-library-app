import AuthButton from "@/components/auth/AuthButton";
import { ModeToggle } from "@/components/core/ModeToggle";
import { Button } from "@/components/ui/button";

const Navbar = () => (
  <nav className="p-4 flex justify-between items-center">
    <a href="/" className="">
      <h1 className="text-xl font-bold">BookSmart</h1>
    </a>
    <div className="space-x-4">
      <a href="/" className="text-gray-600 hover:text-black">
        Home
      </a>
      <a href="/library" className="text-gray-600 hover:text-black">
        Discover
      </a>
      <a href="/profile" className="text-gray-600 hover:text-black">
        My Library
      </a>
    </div>
    <div className="flex gap-2">
      <ModeToggle />
      <AuthButton signInHref="/login">
        <Button className="bg-blue-600 text-white hover:bg-blue-700">
          Sign In
        </Button>
      </AuthButton>
    </div>
  </nav>
);

const Footer = () => (
  <footer className="py-8">
    <div className="max-w-6xl mx-auto text-center text-gray-600 px-6">
      © 2025 BookSmart •{" "}
      <a href="#" className="hover:text-primary">
        About
      </a>{" "}
      •{" "}
      <a href="#" className="hover:text-primary">
        Contact
      </a>{" "}
      •{" "}
      <a href="#" className="hover:text-primary">
        Privacy
      </a>
    </div>
  </footer>
);

const PublicLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <main className="root-container min-w-[360px]">
      <div className="mx-auto max-w-7xl w-full ">
        <Navbar />
        <div className="md:mt-20 pb-20">{children}</div>
      </div>
      <Footer />
    </main>
  );
};

export default PublicLayout;
