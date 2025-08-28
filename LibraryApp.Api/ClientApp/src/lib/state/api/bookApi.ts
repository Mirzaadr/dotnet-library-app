import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQueryWithReauth } from "./baseApi";
import type { Book } from "@/types/Book";

interface GetBooksParams {
  page?: number | null;
  size?: number | null;
  search?: string | null;
  sortBy?: string | null;
}

export interface Pagination {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number;
  totalPages: number;
}

export interface PaginatedBooksResponse {
  data: Book[];
  pagination: Pagination;
}

export const bookApi = createApi({
  reducerPath: "bookApi",
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getBooks: builder.query<PaginatedBooksResponse, GetBooksParams>({
      query: ({ page, size, search, sortBy }) => ({
        url: "/v1/books",
        method: "GET",
        params: {
          page: page || 1,
          pageSize: size || undefined,
          search: search || undefined,
          sortBy: sortBy || undefined,
        },
      }),
      transformResponse: (response: Book[], meta: { response?: Response }) => {
        // Extract pagination info from response headers
        const paginationHeader = meta?.response?.headers?.get("Pagination");
        let pagination: Pagination = {
          currentPage: 1,
          itemsPerPage: 10,
          totalItems: 0,
          totalPages: 0,
        };
        // const totalItems = meta?.response?.headers?.get('X-Total-Count') || '0';
        // const currentPage = meta?.response?.headers?.get('X-Page') || '1';
        // const pageSize = meta?.response?.headers?.get('X-Page-Size') || '10';
        // const isNext = meta?.response?.headers?.get('X-Has-Next-Page') === 'true';

        if (paginationHeader) {
          try {
            // Parse the Pagination JSON header
            pagination = JSON.parse(paginationHeader);
          } catch (error) {
            console.error("Failed to parse Pagination header:", error);
          }
        }

        return {
          data: response, // Books data
          pagination: pagination,
        };
      },
    }),
    getBookById: builder.query<Book, string>({
      query: (id: string) => ({
        url: `/v1/books/${id}`,
        method: "GET",
      }),
    }),
  }),
});

export const { useGetBooksQuery, useGetBookByIdQuery } = bookApi;
