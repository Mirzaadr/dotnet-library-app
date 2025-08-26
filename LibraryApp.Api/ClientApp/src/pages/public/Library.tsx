import { useGetBooksQuery } from "@/lib/state/api";
import { useSearchParams } from "react-router-dom";
import BookList from "./_components/BookList";
import SearchInput from "./_components/SearchInput";
import { Button } from "@/components/ui/button";
import Pagination from "./_components/Pagination";

const getPageFromParams = (params: URLSearchParams): number => {
  const pageParam = params.get("page");
  const parsedPage = pageParam ? parseInt(pageParam, 10) : NaN;
  return !isNaN(parsedPage) && parsedPage > 0 ? parsedPage : 1;
}

const Library = () => {
  let [searchParams] = useSearchParams();
  const page = getPageFromParams(searchParams);
  const search = searchParams.get("search");
  const sortBy = searchParams.get("sortBy");

  const { data: books, isLoading } = useGetBooksQuery({ page, search, sortBy});

  return (
    <>
      <section className="lg:h-[30vh] justify-center items-center flex">
        <div className="w-full md:w-[500px] lg:w-[700px] flex flex-col justify-center items-center">
          <span className="font-ibm-plex-sans font-semibold capitalize text-light-100 hidden md:block md:text-lg lg:text-xl">
            DISCOVER YOUR NEXT GREAT READ
          </span>
          <h1 className="hidden md:block md:text-4xl lg:text-6xl font-semibold text-light-400 text-wrap text-center mt-3">
            Explore and Search for
            <br /> <span className="text-primary">Any Book</span> In Our Library
          </h1>

          <SearchInput search={search || ""} />
        </div>
      </section>
      <section className="mt-10 md:mt-28">
        <div className="flex w-full items-center justify-between">
          <h2 className="font-bebas-neue text-4xl text-light-100">
            Search Results
          </h2>

          {/* <SortOption defaultValue={sortBy || ""} /> */}
        </div>

        {isLoading ? (
          <BookList.Skeleton pageSize={12} />
        ) : (
            <BookList
              books={books || []}
              emptyPage={
                <ul className="book-list justify-center items-center min-h-[331px] md:min-h-[662px]">
                  <li id="not-found">
                    <h4>No Results found</h4>
                    <p>
                      We couldn&apos;t find anybooks matching your search.
                      <br />
                      Try using different keywords or titles
                    </p>
                    <Button
                      onClick={async () => {
                        // "use server";
                        // redirect("/library");
                      }}
                      className="not-found-btn"
                    >
                      Clear Search
                    </Button>
                  </li>
                </ul>
              }
            />
        )}
        <Pagination page={page} count={(books?.length || 0) * 2} />
      </section>
    </>
  );
};

export default Library;
