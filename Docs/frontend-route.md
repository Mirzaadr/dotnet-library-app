# 📘 Frontend Route Documentation

This document outlines all the routes for the Library App frontend, including access control, layout usage, and descriptions.

---

## 🔐 Auth Routes

| Path       | Component        | Access      | Layout       | Description                       |
| ---------- | ---------------- | ----------- | ------------ | --------------------------------- |
| `/sign-in` | `SignInPage`     | Public Only | `AuthLayout` | User login form                   |
| `/sign-up` | `SignUpPage`     | Public Only | `AuthLayout` | New user registration             |
| `/forgot`  | `ForgotPassword` | Public Only | `AuthLayout` | (Optional) Password reset request |
| `/reset`   | `ResetPassword`  | Public Only | `AuthLayout` | (Optional) Reset password form    |

---

## 🌍 Public Routes (Authenticated Users)

| Path           | Component        | Access        | Layout         | Description                    |
| -------------- | ---------------- | ------------- | -------------- | ------------------------------ |
| `/`            | `HomePage`       | Authenticated | `PublicLayout` | Featured + recommended books   |
| `/library`     | `LibraryPage`    | Authenticated | `PublicLayout` | Browse/search all books        |
| `/books/:id`   | `BookDetailPage` | Authenticated | `PublicLayout` | Detailed book info             |
| `/my-profile`  | `ProfilePage`    | Authenticated | `PublicLayout` | User account info and settings |
| `/receipt/:id` | `ReceiptPage`    | Authenticated | `PublicLayout` | Detailed borrow info           |
| `/history`     | `BorrowHistory`  | Authenticated | `PublicLayout` | List of borrow/return history  |

---

## 🛠️ Admin Routes (Admin Only)

| Path                      | Component        | Access | Layout        | Description                      |
| ------------------------- | ---------------- | ------ | ------------- | -------------------------------- |
| `/admin`                  | `AdminDashboard` | Admin  | `AdminLayout` | Dashboard with app stats         |
| `/admin/users`            | `UserManager`    | Admin  | `AdminLayout` | View and manage users            |
| `/admin/account-requests` | `UserManager`    | Admin  | `AdminLayout` | View and manage account approval |
| `/admin/books`            | `BookManager`    | Admin  | `AdminLayout` | Manage book inventory            |
| `/admin/books/:id`        | `BookDetail`     | Admin  | `AdminLayout` | Show book details                |
| `/admin/books/new`        | `BookForm`       | Admin  | `AdminLayout` | Add a new book                   |
| `/admin/books/edit/:id`   | `BookForm`       | Admin  | `AdminLayout` | Edit existing book               |
| `/admin/borrow-records`   | `RecordManager`  | Admin  | `AdminLayout` | View/manage borrow records       |
| `/admin/profile`          | `AdminProfile`   | Admin  | `AdminLayout` | Admin account page (optional)    |

---

## 🚫 Error & Fallback Routes

| Path   | Component      | Description                          |
| ------ | -------------- | ------------------------------------ |
| `*`    | `NotFoundPage` | Fallback page for unknown routes     |
| `/403` | `AccessDenied` | (Optional) Shown on forbidden access |

---

## 🧱 Layout Overview

| Layout         | Purpose                        |
| -------------- | ------------------------------ |
| `AuthLayout`   | Login/Register/Forgot Password |
| `PublicLayout` | Header, footer, nav for users  |
| `AdminLayout`  | Sidebar, admin content         |

---

## 🔐 Access Control Notes

- Auth routes: only visible to unauthenticated users.
- Public routes: require user to be logged in.
- Admin routes: require user to be logged in with admin role.

Use `RequireAuth` or `RequireAdmin` components with React Router to enforce these access rules.

---

## 🛡️ Route Protection Strategy

- Authenticated-only routes are wrapped with `RequireAuth`
- Admin-only routes are wrapped with `RequireAdmin`
- Unauthenticated-only routes use `RequireUnauth`

Example:

```tsx
<Route
  path="/admin"
  element={
    <RequireAdmin>
      <AdminLayout />
    </RequireAdmin>
  }
/>
```
