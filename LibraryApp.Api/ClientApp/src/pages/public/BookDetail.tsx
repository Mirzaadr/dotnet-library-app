import { useGetBookByIdQuery, useGetBooksQuery } from "@/lib/state/api";
import { useParams } from "react-router-dom";
import { Video } from "@imagekit/react";
import Markdown from "react-markdown";
import BookOverview from "../book/BookOverview";
import { Skeleton } from "@/components/ui/skeleton";
import BookList from "./_components/BookList";

const BookDetail = () => {
  const { id: bookId } = useParams<{ id: string }>();
  const { data: bookDetails, isLoading } = useGetBookByIdQuery(bookId || "");
  
  if (isLoading) return <LoadingPage />;
  if (!bookDetails) return <div>Book not found</div>;
  
  return (
    <>
      <BookOverview {...bookDetails} userId="" fullDescription/>

      <div className="book-details">
        <div className="flex-1">
          <section className="flex flex-col gap-7">
            <h3>Video</h3>
            {/* <BookVideo videoUrl={bookDetails.videoUrl} /> */}
            <Video 
              urlEndpoint="https://ik.imagekit.io/mirzaadr" 
              src={bookDetails.videoUrl} 
              controls={true} 
              className="w-full rounded-xl"
            />
          </section>
          <section className="mt-10 flex flex-col gap-7">
            <h3>Summary</h3>

            <Markdown className="space-y-5 text-xl text-light-100">
              {bookDetails.summary}
            </Markdown>
          </section>
        </div>
        <div className="flex-1">
          <h3>More Books</h3>

          <RecommendedBooks genre={bookDetails.genre} author={bookDetails.author} />

        </div>
      </div>
    </>
  );
};

const RecommendedBooks = ({ genre, author } : { genre: string; author: string; }) => {
  const { data: recommended, isLoading: isLoadingRecom } = useGetBooksQuery({ search: `${genre}`, size: 6 });

  if (isLoadingRecom) return <BookList.Skeleton pageSize={6} />
  return (
    <BookList
      books={recommended || []}
    />
  )

}

const LoadingPage = () => {
  return (
    <>
      <BookOverview.Skeleton />

      <div className="book-details">
        <div className="flex-1">
          <section className="flex flex-col gap-7">
            <h3>Video</h3>
            <Skeleton className="h-[300px]" />
          </section>
          <section className="mt-10 flex flex-col gap-7">
            <h3>Summary</h3>

            <div className="space-y-5 text-xl text-light-100">
              <Skeleton className="h-[500px]" />
            </div>
          </section>
        </div>
        <div className="flex-1">
          <h3>More Books</h3>
          <BookList.Skeleton pageSize={6} />
        </div>
      </div>
    </>
  );
};

export default BookDetail;
