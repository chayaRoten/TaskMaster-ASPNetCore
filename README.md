# Task Management System

This project is a task management system built using **ASP.NET Core MVC** and **Web API**. The system allows users to manage their to-do items, with robust user authentication and role-based access control, including Google OAuth2 integration for user authentication.

## Features

- **User Authentication:** Google OAuth2 for secure user login.
- **Role-Based Access:** Only administrators can manage users; regular users can only manage their own to-do items.
- **Task Management:** Users can create, read, update, and delete their to-do items.
- **Logging:** Each request is logged with detailed information including request time, controller/action name, logged-in user, and operation duration.
- **JSON Storage:** To-do items and users are stored in JSON files, with easy transition to a database through an injected service interface.
- **Client-Side Management:** A user-friendly interface allows users to manage tasks, with a dedicated admin interface for user management.

## API Endpoints

### To-Do Items

| URL               | Method | Authorization | Description                | Request Body | Response Body          |
|-------------------|--------|---------------|----------------------------|--------------|------------------------|
| `/api/todo`       | GET    | User/Admin    | Get all user's to-do items | None         | List of to-do items     |
| `/api/todo/{id}`  | GET    | User/Admin    | Get a user's to-do item by ID | None     | To-do item              |
| `/api/todo`       | POST   | User/Admin    | Add a new to-do item       | To-do item   | To-do item location     |
| `/api/todo/{id}`  | PUT    | User/Admin    | Update user's to-do item   | To-do item   | Updated to-do item      |
| `/api/todo/{id}`  | DELETE | User/Admin    | Delete user's to-do item   | None         | None                    |

### Users

| URL               | Method | Authorization | Description                   | Request Body | Response Body          |
|-------------------|--------|---------------|-------------------------------|--------------|------------------------|
| `/api/user`       | GET    | User/Admin    | Get my user                   | None         | User                   |
| `/api/user`       | GET    | Admin         | Get all users                 | None         | List of users           |
| `/api/user`       | POST   | Admin         | Add a new user                | User         | User location          |
| `/api/user/{id}`  | DELETE | Admin         | Delete user and all their to-do's | None     | None                    |
| `/api/login`      | POST   | Login         | User login                    | User         | JWT                    |

## Server-Side Notes

- Only administrators can add or delete users.
- Users can manage only their own to-do items and cannot access others' to-do items.
- To-do items and users are stored in JSON files, accessed via an injected service interface for easy transition to a database.
- Each request is logged, including start date & time, controller/action name, logged-in user (if any), and operation duration in milliseconds.

## Client-Side Notes

- The default page displays the user's to-do list and allows adding, updating, or deleting items.
- If no user is logged in (no token in local storage or expired token), the login page is shown instead.
- Users with admin privileges have links between the to-do list page and the users list page.
- A Postman button is available on the login page.

## Challenges

- Users can update their own details (name, password).
- Administrators can manage their own to-do items as regular users.
- Users can log in using their Google account.
