import { useGetBooksQuery } from "@/lib/state/api";
// import { Star } from "lucide-react";
import BookOverview from "../book/BookOverview";
import type { Book } from "@/types/Book";
import BookList from "../book/BookList";

// const HeroSection = () => (
//   <section className="bg-gray-100 p-6 md:flex items-center">
//     <img
//       src="https://via.placeholder.com/150"
//       alt="Book Cover"
//       className="w-40 h-60 rounded shadow-md mb-4 md:mb-0 md:mr-6"
//     />
//     <div>
//       <h2 className="text-2xl font-bold">The Great Book</h2>
//       <div className="flex items-center text-yellow-500 mt-1">
//         {[...Array(4)].map((_, i) => (
//           <Star key={i} className="w-5 h-5 fill-yellow-500" />
//         ))}
//         <Star className="w-5 h-5" />
//       </div>
//       <p className="text-gray-600 mt-2">by John Doe • Fiction</p>
//       <p className="mt-3 text-gray-700">
//         A thrilling adventure of mystery and discovery that will leave you on
//         the edge of your seat.
//       </p>
//       <div className="mt-4 space-x-3">
//         <button className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700">
//           View Book
//         </button>
//         <button className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700">
//           Borrow Now
//         </button>
//       </div>
//     </div>
//   </section>
// );

// const BookCard = ({ title, author }: { title: string; author: string }) => (
//   <div className="bg-white rounded shadow p-4 w-48">
//     <img
//       src="https://placehold.co/400x600.png"
//       alt={title}
//       className="w-[288px] h-40 object-cover rounded mb-2"
//     />
//     <h3 className="font-semibold text-lg">{title}</h3>
//     <p className="text-sm text-gray-600">{author}</p>
//     <button className="mt-2 w-full bg-blue-500 text-white py-1 rounded hover:bg-blue-600">
//       View
//     </button>
//   </div>
// );

// const RecommendedBooks = () => (
//   <section className="p-6">
//     <h2 className="text-xl font-bold mb-4">Recommended Books</h2>
//     <div className="flex flex-wrap gap-4">
//       {[
//         { title: "Book One", author: "Author A" },
//         { title: "Book Two", author: "Author B" },
//         { title: "Book Three", author: "Author C" },
//         { title: "Book Four", author: "Author D" },
//       ].map((book, i) => (
//         <BookCard key={i} {...book} />
//       ))}
//     </div>
//   </section>
// );

const dummyBook: Book = {
  id: "868bd202-c15c-411e-ba3e-269e72004ac9",
  title: "Midnight Train",
  author: "Angie Sage",
  genre: "Fantasy",
  rating: 4,
  // coverUrl: "/books/cover/50622142_UzTJzMkDs.jpg",
  coverUrl:
    "https://ik.imagekit.io/mirzaadr/books/cover/50622142_UzTJzMkDs.jpg",
  coverColor: "#3e96e5",
  description:
    "A girl must hone her magical gifts to stop an evil force in the New York Times–bestselling author’s sequel to Enchanter’s Twilight Hauntings.In the first book of the Enchanter’s Child duology, Alex discovered the Not only does she possess magical powers but her father is Hagos RavenStarr, who was once the king’s Enchanter.Alex is pursued by the fiendish Twilight Hauntings, monstrous Enchantments created because a prophecy foretold the king’s death at the hands of an Enchanter’s Child. The Twilight Hauntings are designed to rid the land of all Enchanters and their children, but Alex has other ideas. But to destroy the Twilight Hauntings, she must find the very thing that created them—a magical talisman called the Tau.In her search for the Tau, Alex enlists the reluctant help of her father and a strange assortment of characters along the way. As she travels, Alex hones her magical skills and learns that even family and friends can surprise her.",
  totalCopies: 5,
  availableCopies: 5,
  videoUrl:
    "/books/video/coverr-pouring-coffee-into-a-mug-1176-1080p_gRsMQ4B94.mp4",
  summary:
    "A girl must hone her magical gifts to stop an evil force in the New York Times–bestselling author’s sequel to Enchanter’s Twilight Hauntings.In the first book of the Enchanter’s Child duology, Alex discovered the Not only does she possess magical powers but her father is Hagos RavenStarr, who was once the king’s Enchanter.Alex is pursued by the fiendish Twilight Hauntings, monstrous Enchantments created because a prophecy foretold the king’s death at the hands of an Enchanter’s Child. The Twilight Hauntings are designed to rid the land of all Enchanters and their children, but Alex has other ideas. But to destroy the Twilight Hauntings, she must find the very thing that created them—a magical talisman called the Tau.In her search for the Tau, Alex enlists the reluctant help of her father and a strange assortment of characters along the way. As she travels, Alex hones her magical skills and learns that even family and friends can surprise her.",
};

const Home = () => {
  const { data: books, isLoading } = useGetBooksQuery({});
  return (
    <div>
      <BookOverview {...dummyBook} userId="" />
      {/* <RecommendedBooks /> */}
      {isLoading ? (
        <BookList.Skeleton title="Recommended" containerClassname="mt-28" />
      ) : (
        <BookList
          title="Recommended"
          books={books || []}
          containerClassname="mt-28"
        />
      )}

      {/* <div className="p-6">
        <h2>Books</h2>
        {isLoading && <p>Loading...</p>}
        {books && books.length === 0 && <p>No books to show</p>}
        <div className="flex gap-2 overflow-auto">
          {books?.map((book) => (
            <BookCard key={book.id} {...book} />
          ))}
        </div>
      </div> */}
    </div>
  );
};

export default Home;
