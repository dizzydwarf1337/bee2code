
## 🚀 Quick Start

### 1. Clone the Repository

```sh
git clone https://github.com/dizzydwarf1337/bee2code.git
cd bee2code
```

### 2. Database Connection

By default, the project is configured to use **LocalDB** on Windows.  
You can find the connection string in `API/appsettings.json`:

```json
"ConnectionStrings": {
  "BeeCodeConnection": "Data Source=(localdb)\\MSSQLLocalDB;Database=BeeCode;Integrated Security=True;TrustServerCertificate=True;"
}
```
**This means:**  
- If you are using Windows and have Visual Studio or SQL Server Express installed, you do not need to change anything.
- The database will be created automatically on first launch.

#### **To use a different SQL Server (e.g., remote, Docker, etc):**
Replace the connection string with your own, for example:
```json
"ConnectionStrings": {
  "BeeCodeConnection": "Server=localhost;Database=BeeCodeDb;User Id=sa;Password=yourStrong(!)Password;"
}
```
Be sure to use your actual server, database name, username, and password.

---

### 3. Build and Run the Server

Navigate to the API project folder and execute:

```sh
dotnet build
dotnet watch --project API
```

- The application will automatically apply migrations and create the database if it does not exist.
- All required tables and seed data (roles, default users) will be created.

---

### 4. Default User Accounts

On first run, the database is seeded with roles and users:

- **Admin**
  - Email: `admin@gmail.com`
  - Password: `SuperSecretAdminPassword1!`
- **Worker**
  - Email: `worker1@gmail.com` / `worker2@gmail.com`
  - Password: `WorkerPassword1!`
- **Patient**
  - Email: `patient1@gmail.com`
  - Password: `PatientPassword1!`

---

### 5. API Access

- By default, the server listens on `http://localhost:5000`  
  (or as set in `launchSettings.json`)
