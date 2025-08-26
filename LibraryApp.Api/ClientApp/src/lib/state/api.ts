import {
  fetchBaseQuery,
  type BaseQueryFn,
  type FetchArgs,
  type FetchBaseQueryError,
} from "@reduxjs/toolkit/query";
import { adjustUsedToken, authTokenChange, logoutUser } from "./auth/authSlice";
import type { RootState } from "./store";
import { createApi } from "@reduxjs/toolkit/query/react";
import type { Book } from "@/types/Book";

const BASE_URL_API = "http://localhost:5001/api";

export const baseQuery = fetchBaseQuery({
  baseUrl: BASE_URL_API,
  mode: "cors",
  prepareHeaders: (headers, { getState }) => {
    const token = (getState() as RootState).auth.usedToken;
    if (token) {
      headers.set("authorization", `Bearer ${token}`);
    }
    return headers;
  },
});

export const baseQueryWithReauth: BaseQueryFn<
  string | FetchArgs,
  unknown,
  FetchBaseQueryError
> = async (args, store, extraOptions) => {
  let result = await baseQuery(args, store, extraOptions);

  const authState = (store.getState() as RootState).auth;

  if (result.error && result.error.status === 401) {
    if (!authState.token || !authState.refreshToken) return result;

    // Update token to use refresh token
    store.dispatch(adjustUsedToken(authState.refreshToken as string));

    // Try to refresh the token
    const refreshResult = await baseQuery(
      "/refresh-token",
      store,
      extraOptions
    );

    if (refreshResult) {
      // Store the new tokens
      store.dispatch(
        authTokenChange({
          accessToken: (refreshResult.data as any).accessToken,
          refreshToken: authState.refreshToken as string,
        })
      );
      // Retry the original request
      result = await baseQuery(args, store, extraOptions);
    } else {
      store.dispatch(logoutUser());
    }
  }
  return result;
};

export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getUser: builder.query<any, void>({
      query: () => ({
        url: "/v1/user",
        method: "GET",
      }),
    }),
  }),
});

interface GetBooksParams {
  page?: number | null;
  size?: number | null;
  search?: string | null;
  sortBy?: string | null;
}

export const bookApi = createApi({
  reducerPath: "bookApi",
  baseQuery: baseQueryWithReauth,
  endpoints: (builder) => ({
    getBooks: builder.query<Book[], GetBooksParams>({
      query: ({ page, size, search, sortBy }) => ({
        url: "/v1/books",
        method: "GET",
        params: { 
          page: page || 1, 
          pageSize: size || undefined, 
          search: search || undefined, 
          sortBy: sortBy || undefined 
        },
      }),
    }),
    getBookById: builder.query<Book, string>({
      query: (id: string) => ({
        url: `/v1/books/${id}`,
        method: "GET",
    }),
  }),
  })
})

export const { useGetUserQuery } = authApi;
export const { useGetBooksQuery, useGetBookByIdQuery } = bookApi;
