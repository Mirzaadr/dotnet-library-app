"use client";

import { cn } from "@/lib/utils";
import BookCoverSvg from "./BookCoverSvg";
import { Image } from "@imagekit/react";
// import config from "@/lib/config";

type BookCoverVariant = "extraSmall" | "small" | "medium" | "regular" | "wide";
const variantStyles: Record<BookCoverVariant, string> = {
  extraSmall: "book-cover_extra_small",
  small: "book-cover_small",
  medium: "book-cover_medium",
  regular: "book-cover_regular",
  wide: "book-cover_wide",
};

interface BookCoverProps {
  className?: string;
  variant?: BookCoverVariant;
  coverColor: string;
  coverImage?: string;
}

const BookCover = ({
  className,
  variant = "regular",
  coverColor = "#012B48",
  coverImage = "https://placehold.co/400x600.png",
}: BookCoverProps) => {
  return (
    <div
      className={cn(
        "relative transition-all duration-300",
        variantStyles[variant],
        className
      )}
    >
      <BookCoverSvg coverColor={coverColor} />

      <div
        className="absolute z-10"
        style={{ left: "12%", width: "87.5%", height: "88%" }}
      >
        {coverImage.startsWith("http") ? (
          <img
            src={coverImage}
            alt="book-cover"
            className="rounded-sm object-cover w-full h-full"
          />
        ) : (
          <Image
            src={coverImage}
            urlEndpoint="https://ik.imagekit.io/mirzaadr"
            alt="book-cover"
            fill
            className="rounded-sm object-fill h-full w-full"
          />
        )}
      </div>
    </div>
  );
};

export default BookCover;
