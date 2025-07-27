import { cn } from "@/lib/utils";
import BookCover from "./BookCover";
// import BorrowInfo from "./BorrowInfo";
import { Skeleton } from "@/components/ui/skeleton";
import type { Book } from "@/types/Book";
import type { BorrowedBook } from "@/types/BorrowedBook";

const BookCard = ({
  id,
  title,
  genre,
  // rating,
  coverColor,
  coverUrl,
  // borrowDate,
  // dueDate,
  // receiptId,
  isLoanedBook = false,
}: Book & BorrowedBook) => {
  return (
    <li className={cn(isLoanedBook && "borrowed-book")}>
      <a
        href={`/books/${id}`}
        className={cn(isLoanedBook && "w-full flex flex-col items-center")}
      >
        <div
          className={cn("", isLoanedBook && "borrowed-book_cover")}
          style={isLoanedBook ? { backgroundColor: coverColor + "4D" } : {}}
        >
          <BookCover
            coverColor={coverColor}
            coverImage={coverUrl}
            variant={isLoanedBook ? "medium" : "regular"}
          />
        </div>

        <div
          className={cn("mt-4 w-full", !isLoanedBook && "xs:max-w-40 max-w-28")}
        >
          <p className="book-title" title={title}>
            {title}
          </p>
          <p className="book-genre">{genre}</p>
        </div>

        {/* {isLoanedBook && borrowDate && dueDate && (
          <BorrowInfo
            borrowDate={borrowDate}
            dueDate={dueDate}
            receiptId={receiptId || ""}
          />
        )} */}
      </a>
    </li>
  );
};

BookCard.Skeleton = function BookCardSkeleton() {
  return (
    <li className="h-[311px]">
      <Skeleton className="book-cover_regular" />
      <div className="mt-4 xs:max-w-40 max-w-28">
        <Skeleton className="book-title-skeleton h-5 w-[80%]" />
        <Skeleton className="book-genre-skeleton h-4 w-[50%] mt-2" />
      </div>
    </li>
  );
};

export default BookCard;
