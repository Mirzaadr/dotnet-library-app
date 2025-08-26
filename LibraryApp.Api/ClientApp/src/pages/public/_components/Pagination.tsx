import { Button } from "@/components/ui/button";
import { ChevronLeft, ChevronRight } from "lucide-react";
import { cn } from "@/lib/utils";
import { useMemo, Fragment } from "react";

const ITEM_PER_PAGE = 12;

interface IPaginationProps {
  page: number;
  count: number;
}

const Pagination = ({ page, count }: IPaginationProps) => {
  const hasPrev = useMemo(() => ITEM_PER_PAGE * (page - 1) > 0, [page]);
  const hasNext = useMemo(
    () => ITEM_PER_PAGE * (page - 1) + ITEM_PER_PAGE < count,
    [page, count]
  );
  const maxPage = useMemo(() => Math.ceil(count / ITEM_PER_PAGE), [count]);

  const changePage = (newPage: number) => {
    const params = new URLSearchParams(window.location.search);
    params.set("page", newPage.toString());
    // router.push(`${window.location.pathname}?${params}`, { scroll: false });
    window.history.replaceState(null, "", `${window.location.pathname}?${params.toString()}`);
    history.scrollRestoration = 'manual';
    window.location.reload();
  };
  return (
    <div
      id="pagination"
      className="p-4 flex items-center space-x-2 text-gray-500 self-end"
    >
      <Button
        disabled={!hasPrev}
        size="icon"
        key={`button-prev`}
        onClick={() => {
          if (hasPrev) changePage(page - 1);
        }}
        className="pagination-btn_dark"
      >
        <ChevronLeft />
      </Button>
      <div className="flex items-center gap-2 text-sm">
        {Array.from({ length: maxPage }, (_, index) => {
          const pageIndex = index + 1;

          // if (pageIndex < page - 1 || pageIndex > page + 1) return null;

          if (
            pageIndex === page - 1 ||
            pageIndex === page ||
            pageIndex === page + 1 ||
            pageIndex === 1 ||
            pageIndex === maxPage
          ) {
            return (
              <Fragment key={index}>
                {pageIndex === maxPage && page < maxPage - 2 && (
                  <span>...</span>
                )}
                <Button
                  size="icon"
                  className={cn(
                    "rounded-sm",
                    page === pageIndex ? "bg-primary" : "bg-dark-300"
                  )}
                  onClick={() => changePage(pageIndex)}
                >
                  {pageIndex}
                </Button>
                {pageIndex === 1 && page > 3 && <span>...</span>}
              </Fragment>
            );
          } else {
            return null;
          }
        })}
      </div>
      <Button
        disabled={!hasNext}
        size="icon"
        key={`button-next`}
        onClick={() => {
          if (hasNext) changePage(page + 1);
        }}
        className="pagination-btn_dark"
      >
        <ChevronRight />
      </Button>
    </div>
  );
};

export default Pagination;