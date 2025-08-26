import { Input } from "@/components/ui/input";
import { Search } from "lucide-react";
import { useRef } from "react";

interface SearchInputProps {
  search: string;
}

const SearchInput = ({ search }: SearchInputProps) => {
  const inputRef = useRef<HTMLInputElement>(null);
//   const router = useRouter();

  const submitQuery = (query?: string) => {
    const params = new URLSearchParams(window.location.search);
    if (query === search) return;
    if (query) {
      params.set("search", query);
    } else {
      params.delete("search");
    }
    params.set("page", "1");
    window.history.replaceState(null, "", `${window.location.pathname}?${params.toString()}`);
    window.location.reload();
  };

  const onKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      e.preventDefault();
      submitQuery(inputRef.current?.value);
      inputRef.current?.blur();
    }
  };
  const onBlur = (e: React.FocusEvent<HTMLInputElement>) => {
    e.preventDefault();
    submitQuery(inputRef.current?.value);
  };

  return (
    <div
      className="search"
      onClick={() => {
        inputRef.current?.focus();
      }}
    >
      <Search
        className="size-5 text-primary"
        onClick={() => {
          inputRef.current?.focus();
        }}
      />
      <Input
        type="text"
        ref={inputRef}
        className="search-input"
        placeholder="Search for books"
        defaultValue={search}
        onKeyDown={onKeyDown}
        onBlur={onBlur}
      />
      {/* <button className="search-button">Search</button> */}
    </div>
  );
};

export default SearchInput;