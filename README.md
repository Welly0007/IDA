# IDA – Integrated Department & User Administration

Full‑stack solution for managing departments, users, employees and groups. Backend focuses on clean architecture & extensibility; frontend delivers a modular Angular UI.

## ✨ Highlights

-   Layered .NET backend (Domain / Repositories / Services / API)
-   Repository + Unit of Work patterns for transactional consistency
-   AutoMapper profile for DTO ↔ Entity mapping (stable external contracts)
-   Explicit DTO layer prevents over‑posting & leakage
-   Generic base repository reduces repetition (DRY)
-   Pagination, ordering & projection ready for scaling
-   Angular feature modules + services + dialog driven CRUD

## 🧱 Backend Architecture

| Folder                | Purpose                                                           |
| --------------------- | ----------------------------------------------------------------- |
| Entity/Models         | Core domain entities (User, Dept, Emp, Group, GroupUser, Citizen) |
| Entity/Dtos           | Transport types (UserDto, UserSaveDto, DeptDto, EmpDto)           |
| Entity/Interfaces     | Abstractions (Repositories, UnitOfWork, Entity contracts)         |
| Entity/Consts         | Shared enums / ordering helpers                                   |
| Core/Repositories     | Generic `BaseRepository<T>` implementation                        |
| Core/UnitOfWork       | Aggregates repositories; single Save boundary                     |
| Core/AppDbContext     | EF Core `ApplicationDbContext`                                    |
| Service/Services      | Business orchestration (e.g. `UserService`)                       |
| FirstTask/Controllers | Thin API endpoints mapping HTTP ↔ Services                        |
| MappingProfile        | AutoMapper configuration centralizing transformations             |

### Flow (Add User)

Controller → Service → UnitOfWork → Repository → DbContext → DB
Return path: Entity → Mapper → DTO → JSON

## 🧩 Patterns & Principles

| Pattern              | Benefit                                       |
| -------------------- | --------------------------------------------- |
| Repository           | Swappable persistence / test isolation        |
| Unit of Work         | Atomic multi-entity operations                |
| DTO + Mapping        | Versioned stable contracts, security          |
| Dependency Inversion | Controllers depend on abstractions            |
| SRP                  | Each layer has one reason to change           |
| Open/Closed          | Add new entity without breaking existing code |
| Projection (Select)  | Reduces payload & over-fetching               |

## 🚀 Scalability Hooks

-   Query shaping & pagination already in place
-   Clear seams for caching (decorate services) & cross-cutting (logging, auth)
-   Replace EF Core by re‑implementing repository + UoW only
-   Ready for CQRS split (DTO read models separate from entities)

## 🌐 Sample Endpoints

GET `/api/User?pageSize=10&pageNumber=1&ascending=true`
POST `/api/User`

```json
{
    "userName": "jdoe",
    "empName": "John Doe",
    "natId": "12345678901234",
    "deptId": 3,
    "extClctr": false
}
```

## 🖥️ Run Backend

```powershell
cd Backend
dotnet restore
dotnet build
dotnet run --project FirstTask/PresentationLayer.csproj
```

Adjust connection string in `appsettings*.json`.

## 🖥️ Run Frontend

```powershell
cd fronEnd
npm install
npm start
```

Default: http://localhost:4200 (expects API at https://localhost:7285).

## 🧪 Adding a New Entity (Quick Path)

1. Model in `Entity/Models`
2. DTO(s) + map in `MappingProfile`
3. (Optional) Custom repository or reuse base
4. Service method
5. Controller endpoint
6. Angular service + component/dialog

## 🔒 Future Enhancements

-   Authentication / Authorization (JWT or Identity)
-   Centralized validation (FluentValidation)
-   Caching (decorator around services)
-   Auditing / soft delete
-   Structured logging & metrics

## 📂 Root Structure (Trimmed)

```
Backend/   # .NET API + Domain + Services
fronEnd/   # Angular 13 application
```

## ✅ Why This Design Works

-   Fast onboarding: predictable folder semantics
-   Maintainable: low coupling between layers
-   Testable: services & repositories mockable
-   Evolvable: new features slot into existing seams

---

Internal project. Licensed @[EIS](https://eis.com.eg/)
