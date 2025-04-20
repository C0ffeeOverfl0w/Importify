# 📥 Importify

**Importify** is a clean-architecture-based ASP.NET Core MVC application for importing, validating, 
and managing employee records from CSV files. 
Built to showcase best practices modular design, error handling, and testability.

---

## 🚀 Features

- ✅ CSV import with row-level validation
- 🧠 Duplicate detection (file and database)
- 📊 Modern UI with sorting, searching, editing😅😅😅
- 💥 Import error reporting and breakdown
- 🧪 Unit-tested services (CsvHelper, business rules)
- 🧱 Clean architecture (Domain → Application → Infrastructure → Web)

---

## 🧱 Tech Stack

- **.NET 9**
- **ASP.NET Core MVC**
- **Entity Framework Core** (SQLite or LocalDb)
- **CsvHelper** for parsing CSV files
- **Bootstrap 5 + DataTables** for responsive tables
- **Moq + xUnit** for unit testing

---

## 📁 Project Structure

```
Importify
├── Domain              // Entities, ValueObjects, interfaces
├── Application         // DTOs, services, use cases
├── Infrastructure      // EF Core, configs, repositories
├── Web                 // MVC controllers, Razor pages, UI
├── Tests               // Unit tests
```

---

## ⚙️ Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/importify.git
   cd importify
   ```

2. **Update the database:**
   ```bash
   dotnet ef database update --project Importify.Infrastructure
   ```

3. **Run the application:**
   ```bash
   dotnet run --project Importify.Web
   ```

4. Visit `http://localhost:5000` in your browser

---

## 📄 CSV Format

CSV must contain the following headers:
```csv
Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname,Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address,Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date
```

See `CsvEmployeeModelMap.cs` for the full column mapping.

---

## 🧪 Testing

```bash
dotnet test
```

Use simplified headers in test files if needed:
```csv
PayrollNumber,Forenames,Surname,DateOfBirth,...
```

---

## 🤝 Contributing

Pull requests and issues are welcome. Let's improve Importify together!

---

## 📜 License

MIT © [YourName]
