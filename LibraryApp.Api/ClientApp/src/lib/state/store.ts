import { configureStore } from "@reduxjs/toolkit";
import { authApi } from "./api";
import authReducer from "./auth/authSlice";
import themeReducer from "./theme/themeSlice";

const store = configureStore({
  reducer: {
    auth: authReducer,
    theme: themeReducer,
    [authApi.reducerPath]: authApi.reducer,
  },
  middleware: (getDefaultMiddleware) => {
    return getDefaultMiddleware().concat(authApi.middleware);
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
