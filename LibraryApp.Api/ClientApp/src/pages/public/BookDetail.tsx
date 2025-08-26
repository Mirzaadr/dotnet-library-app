import { useGetBookByIdQuery } from "@/lib/state/api";
import { useParams } from "react-router-dom";
import { Video } from "@imagekit/react";
import Markdown from "react-markdown";
import BookOverview from "../book/BookOverview";

const BookDetail = () => {
  const { id: bookId } = useParams<{ id: string }>();
  const { data: bookDetails, isLoading } = useGetBookByIdQuery(bookId || "");

  if (isLoading) return <div>Loading...</div>;
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

          {/* <Suspense fallback={<BookList.Skeleton pageSize={6} />}>
            <BookList
              query={{
                OR: [
                  { author: bookDetails.author },
                  { genre: bookDetails.genre },
                ],
                NOT: { id },
              }}
              pageSize={6}
            />
          </Suspense> */}
        </div>
      </div>
    </>
  );
};

export default BookDetail;
