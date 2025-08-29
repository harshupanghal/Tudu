### 1. Remove all migrations (repeat until none left OR delete Migrations folder)
dotnet ef migrations remove -p Tudu.Infrastructure -s Tudu.Ui

### 2. Drop database
dotnet ef database drop -p Tudu.Infrastructure -s Tudu.Ui

### 3. Create new migration
dotnet ef migrations add InitialCreate -p Tudu.Infrastructure -s Tudu.Ui

### 4. Apply migration (create schema in DB)
dotnet ef database update -p Tudu.Infrastructure -s Tudu.Ui



 Normal Workflow (when changing models)
### After editing entity classes / DbContext:
dotnet ef migrations add AddDueDate -p Tudu.Infrastructure -s Tudu.Ui
dotnet ef database update -p Tudu.Infrastructure -s Tudu.Ui


 Wipe Data Only (keep schema)
DELETE FROM Users;
DELETE FROM UserTasks;


or in C# startup:

db.Database.ExecuteSqlRaw("DELETE FROM [Users]");
db.Database.ExecuteSqlRaw("DELETE FROM [UserTasks]");