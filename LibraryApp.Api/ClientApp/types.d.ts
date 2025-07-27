type Book = {
  id: string;
  title: string;
  author: string;
  genre: string;
  rating: number;
  totalCopies: number;
  availableCopies: number;
  description: string;
  coverColor: string;
  coverUrl: string;
  videoUrl: string;
  summary: string;
  createdAt?: Date | null;
};

type BorrowedBook = Book & {
  receiptId?: string;
  isLoanedBook?: boolean;
  borrowDate?: Date;
  dueDate?: Date;
};

type UserData = {
  id: string;
  fullName: string;
  email: string;
  universityId: number;
  universityCard: string;
  status: "PENDING" | "APPROVED" | "REJECTED" | null;
  role: "USER" | "ADMIN";
  createdAt: Date;
  borrowRecords?: BorrowRecords[];
};

type BorrowRecords = {
  id: string;
  // userId: string,
  // bookId: string,
  borrowDate: Date;
  dueDate: Date;
  returnDate: Date | null;
  status: "BORROWED" | "RETURNED";
  createdAt: Date | null;
  books?: Book;
  users?: UserData;
};

interface AuthCredentials {
  fullName: string;
  email: string;
  password: string;
  universityId: number;
  universityCard: string;
}

interface BookParams {
  title: string;
  author: string;
  genre: string;
  rating: number;
  coverUrl: string;
  coverColor: string;
  description: string;
  totalCopies: number;
  videoUrl: string;
  summary: string;
}

interface BorrowBookParams {
  bookId: string;
  userId: string;
}
