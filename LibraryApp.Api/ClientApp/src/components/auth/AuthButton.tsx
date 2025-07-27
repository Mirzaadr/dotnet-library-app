// import { signOut } from 'next-auth/react';
"use client";
import { cn } from "@/lib/utils";
import { useNavigate } from "react-router-dom";

type AuthButtonProps = {
  children: React.ReactNode;
  mode?: "modal" | "redirect";
  type?: "signin" | "signup";
  asChild?: boolean;
  className?: React.HTMLProps<HTMLDivElement>["className"];
  signInHref?: string;
};

const AuthButton = ({
  children,
  // mode = "modal",
  // type = "signin",
  // asChild,
  className,
  signInHref = "/auth/signin",
}: AuthButtonProps) => {
  const redirect = useNavigate();
  const onClick = () => {
    redirect(signInHref || "/");
  };

  // if (mode === "modal") {
  //   return (
  //     <Dialog>
  //       <DialogTrigger asChild={asChild}>{children}</DialogTrigger>
  //       <DialogHeader className="hidden">
  //         <DialogTitle />
  //       </DialogHeader>
  //       <DialogContent className="w-auto border-none bg-transparent p-0">
  //         {type === "signin" ? (
  //           <SigninForm showFooter={false} />
  //         ) : (
  //           <SignupForm showFooter={false} />
  //         )}
  //       </DialogContent>
  //     </Dialog>
  //   );
  // }
  return (
    <div onClick={onClick} className={cn("cursor-pointer", className)}>
      {children}
    </div>
  );
};

export default AuthButton;
