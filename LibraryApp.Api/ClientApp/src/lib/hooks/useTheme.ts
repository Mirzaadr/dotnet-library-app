import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../state/store";
import { setTheme, type Theme } from "../state/theme/themeSlice";
import { useEffect } from "react";

export function useTheme() {
  const theme = useSelector((state: RootState) => state.theme.theme);
  const dispatch = useDispatch<AppDispatch>();

  const toggleTheme = () => {
    const systemTheme = window.matchMedia("(prefers-color-scheme: dark)")
      .matches
      ? "dark"
      : "light";

    let nextTheme: Theme;
    if (theme === "system") {
      nextTheme = systemTheme === "dark" ? "light" : "dark";
    } else {
      nextTheme = theme === "dark" ? "light" : "dark";
    }

    dispatch(setTheme(nextTheme));
  };

  useEffect(() => {
    const root = window.document.documentElement;
    const darkQuery = window.matchMedia("(prefers-color-scheme: dark)");

    const applyTheme = () => {
      const systemTheme = darkQuery.matches ? "dark" : "light";
      const applied = theme === "system" ? systemTheme : theme;

      root.classList.remove("light", "dark");
      root.classList.add(applied);
    };

    applyTheme();

    if (theme === "system") {
      darkQuery.addEventListener("change", applyTheme);
      return () => darkQuery.removeEventListener("change", applyTheme);
    }
  }, [theme]);

  return {
    theme, // 'light' | 'dark' | 'system'
    toggleTheme, // toggles only between light and dark
  };
}
