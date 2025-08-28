import { createApi } from "@reduxjs/toolkit/query/react";
import { baseQueryWithReauth } from "./baseApi";

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

export const { useGetUserQuery } = authApi;
