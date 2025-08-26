import BookCard from "../../book/BookCard";
import type { Book } from "@/types/Book";

const BookList = ({
  books = [],
  emptyPage,
}: {
  books?: Book[];
  emptyPage?: React.ReactNode;
}) => {
  if (books.length === 0) {
    return !!emptyPage ? (
      emptyPage
    ) : (
      <ul className="book-list justify-center items-center">
        <li id="not-found">
          <h4>No Results found</h4>
        </li>
      </ul>
    );
  }

  return (
    <ul className="book-list justify-center md:justify-start">
      {books.map((book) => (
        <BookCard key={book.id} {...book} />
      ))}
    </ul>
  );
};

BookList.Skeleton = function BookListSkeleton({ pageSize = 12 }) {
  return (
    <ul className="book-list justify-center md:justify-start">
      {[...Array(pageSize).keys()].map((i) => (
        <BookCard.Skeleton key={i} />
      ))}
    </ul>
  );
};

export default BookList;