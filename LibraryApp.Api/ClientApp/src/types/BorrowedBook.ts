import type { Book } from "./Book";

export type BorrowedBook = Book & {
  receiptId?: string;
  isLoanedBook?: boolean;
  borrowDate?: Date;
  dueDate?: Date;
};
