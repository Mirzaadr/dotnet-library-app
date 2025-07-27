import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { cn, getInitials } from "@/lib/utils";
import {
  BookIcon,
  BookmarkIcon,
  HomeIcon,
  LogOutIcon,
  User2Icon,
  UserIcon,
  type LucideIcon,
} from "lucide-react";
import { NavLink } from "react-router-dom";

interface SidebarItemProps {
  logo: LucideIcon;
  route: string;
  label: string;
}

const adminSideBarLinks: SidebarItemProps[] = [
  {
    logo: HomeIcon,
    route: "/admin",
    label: "Home",
  },
  {
    logo: User2Icon,
    route: "/admin/users",
    label: "All Users",
  },
  {
    logo: BookIcon,
    route: "/admin/books",
    label: "All Books",
  },
  {
    logo: BookmarkIcon,
    route: "/admin/borrow-records",
    label: "Borrow Records",
  },
  {
    logo: UserIcon,
    route: "/admin/account-requests",
    label: "Account Requests",
  },
];

const Sidebar = () => {
  // const pathName = usePathname();
  return (
    <div className="admin-sidebar">
      <div>
        <div className="logo">
          <img
            src={"/icons/admin/logo.svg"}
            alt="logo"
            height={37}
            width={37}
          />

          <h1>BookSmart</h1>
        </div>

        <div className="mt-10 flex flex-col gap-5">
          {adminSideBarLinks.map((link) => {
            const Icon = link.logo;

            return (
              <NavLink to={link.route} key={link.route} end>
                {({ isActive }) => (
                  <div
                    className={cn(
                      "link",
                      isActive && "bg-primary-admin shadow-sm"
                    )}
                  >
                    <div className="relative size-5">
                      <Icon
                        className={`${isActive ? "brightness-0 invert" : ""}`}
                      />
                    </div>

                    <p className={cn(isActive ? "text-white" : "text-dark")}>
                      {link.label}
                    </p>
                  </div>
                )}
              </NavLink>
            );
          })}
        </div>
      </div>

      <div className="user items-center">
        <Avatar>
          <AvatarFallback className="bg-amber-100">
            {getInitials("IN")}
          </AvatarFallback>
        </Avatar>

        <div className="flex flex-col max-md:hidden grow">
          <p className="font-semibold text-dark-200">{"Username"}</p>
          <p className="text-xs text-light-500">{"user@mail.com"}</p>
        </div>

        <Button
          size="icon"
          variant="ghost"
          className="size-8 rounded-[20px] max-md:hidden"
        >
          <LogOutIcon className="text-red-800" />
        </Button>
      </div>
    </div>
  );
};

const Header = () => {
  return (
    <header className="flex lg:items-end items-start justify-between lg:flex-row flex-col gap-5 sm:mb-10 mb-5">
      <div>
        <h2 className="text-2xl font-semibold text-dark-400">{"Username"}</h2>
        <p className="text-base text-slate-500">
          Monitor all your users and books here
        </p>
      </div>

      {/* <p>Search</p> */}
    </header>
  );
};

const AdminLayout = ({ children }: { children: React.ReactNode }) => {
  return (
    <main className="flex min-h-screen w-full flex-row">
      <Sidebar />

      <div className="flex w-[calc(100%-264px)] flex-1 flex-col bg-light-300 p-5 xs:p-10 min-w-[360px]">
        <Header />
        {children}
      </div>
    </main>
  );
};

export default AdminLayout;
