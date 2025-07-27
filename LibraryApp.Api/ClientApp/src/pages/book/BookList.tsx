import { cn } from "@/lib/utils";
import BookCard from "./BookCard";
import { Skeleton } from "@/components/ui/skeleton";
import type { Book } from "@/types/Book";
import type { BorrowedBook } from "@/types/BorrowedBook";

interface BookListProps {
  title?: string;
  books: Book[] | BorrowedBook[];
  containerClassname?: string;
}

const BookList = ({ title, books, containerClassname }: BookListProps) => {
  return (
    <section className={cn("", containerClassname)}>
      <h2 className="font-bebas-neue text-4xl text-light-100">{title}</h2>
      {books.length < 2 ? (
        <ul className="book-list justify-center items-center">
          <li id="not-found">
            <h4>No Results found</h4>
          </li>
        </ul>
      ) : (
        <ul className="book-list justify-center md:justify-start">
          {books.map((book) => (
            <BookCard key={book.id} {...book} />
          ))}
        </ul>
      )}
    </section>
  );
};

BookList.Skeleton = function BookListSkeleton({
  title,
  containerClassname,
}: Omit<BookListProps, "books">) {
  return (
    <section className={cn("", containerClassname)}>
      {title ? (
        <h2 className="font-bebas-neue text-4xl text-light-100">{title}</h2>
      ) : (
        <Skeleton className="w-40 h-10 " />
      )}
      <ul className="book-list justify-center md:justify-start">
        {[...Array(12).keys()].map((i) => (
          <BookCard.Skeleton key={i} />
        ))}
      </ul>
    </section>
  );
};

export default BookList;
