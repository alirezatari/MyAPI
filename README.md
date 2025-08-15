# Full-Stack JWT Secured API (.NET 7) + Web UI (React)

This project is a complete full-stack application featuring a secure .NET 7 Web API backend and a modern React frontend.

The backend includes JWT authentication, role-based authorization, EF Core for data access, and a paginated CRUD API for products.

The frontend is a responsive React application built with Vite and styled with Tailwind CSS, providing a user-friendly interface for logging in and managing products.

## How to Run the API (Backend)

The backend is a .NET 7 Web API project.

### Prerequisites
- .NET 7 SDK
- SQL Server instance (the connection string in `appsettings.Development.json` points to a local server, but can be changed).

### 1. Apply Database Migrations
This command will create the database and the necessary tables (`Users`, `Products`) based on the EF Core migrations. It will also seed the database with a default admin user and sample products.

From the root directory, run:
```bash
dotnet ef database update
```

### 2. Run the API
This command will start the backend server. By default, it runs on `http://localhost:5080`.

From the root directory, run:
```bash
dotnet run
```

### API Endpoints
- **Swagger UI:** `http://localhost:5080/swagger`
- **Login:** `POST /api/auth/login`
- **Register (Admin Only):** `POST /api/auth/register`
- **Products:** `GET, POST, PUT, DELETE /api/product`

---

## How to Run the Web App (Frontend)

The frontend is a React application located in the `/web` directory.

### 1. Configure Environment
The frontend needs to know the base URL of the API. Create a `.env.development` file inside the `/web` directory with the following content:

```
VITE_API_BASE_URL=http://localhost:5080
```

### 2. Install Dependencies
This will install all the necessary packages for the frontend application.

From the root directory, run:
```bash
cd web
yarn install
```
(You can also use `npm install` if you prefer)

### 3. Run the Development Server
This will start the frontend development server. By default, it runs on `http://localhost:5173` (for Vite).

From the `/web` directory, run:
```bash
yarn dev
```
(Or `npm run dev`)

---

## Post-Run Checklist

Once both the API and the Web App are running, you can verify the full application flow:

1.  [x] **Apply Migrations:** `dotnet ef database update` completes successfully.
2.  [x] **Run API:** `dotnet run` starts the server on `http://localhost:5080`.
3.  [x] **Run Web:** `yarn dev` (in `/web`) starts the server on its port (e.g., `http://localhost:5173`).
4.  [x] **Open Web App:** Navigate to the frontend URL in your browser. You should be redirected to the login page.
5.  [x] **Login:** Use the seeded credentials to log in:
    -   **Username:** `admin`
    -   **Password:** `Pass@123`
    -   Upon success, you should be redirected to the Products page.
6.  [x] **CRUD Flows:**
    -   Verify you can see the list of seeded products.
    -   Use the pagination controls.
    -   Create a new product using the "Create Product" button.
    -   Edit an existing product.
    -   Delete a product (you will be asked for confirmation).
7.  [x] **Swagger /authorize:**
    -   Navigate to `http://localhost:5080/swagger`.
    -   Click the "Authorize" button.
    -   Log in via the API (`/api/auth/login`) to get a token.
    -   Paste the token into the Swagger authorize dialog (e.g., `Bearer your_token_here`).
    -   Try executing a protected endpoint (like `GET /api/product`). It should succeed.
