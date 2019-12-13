# 回家作業：練習 ASP․NET Core Web API 與 Entity Framework Core 整合開發

## 要求達成

1. 請以 ContosoUniversity 資料庫為主要資料來源
1. 須透過 DB First 流程建立 EF Core 實體資料模型
1. 須對資料庫進行版控 (使用資料庫移轉方式)
1. 須對每一個表格設計出完整的 CRUD 操作 APIs
1. 針對 Departments 表格的 CUD 操作需用到預存程序
1. 請在 CoursesController 中設計 vwCourseStudents 與 vwCourseStudentCount 檢視表的 API 輸出
1. 請用 Raw SQL Query 的方式查詢 vwDepartmentCourseCount 檢視表的內容
1. 請修改 Course, Department, Person 表格，新增 DateModified 欄位(datetime)，並且這三個表格的資料透過 Web API 更新時，都要自動更新該欄位為更新當下的時間 (請新增資料庫移轉紀錄)
1. 請修改 Course, Department, Person 表格欄位，新增 IsDeleted 欄位 (bit)，且讓所有刪除這三個表格資料的 API 都不能真的刪除資料，而是標記刪除即可，標記刪除後，在 GET 資料的時候不能輸出該筆資料。(請新增資料庫移轉紀錄)

## 建立專案

起手式

```bash
dotnet new api -n 專案名稱 && cd 專案名稱 &&
git init &&
touch readme.md &&
code .
```

添加 `.gitignore`

> In Visual Studio Code, press `shift+cmd+p`, Choose `Add gitignore` and select `VisualStudio`.

## 建立資料模型

取得 SQLite 資料庫檔案，放入專案目錄，並增加 Entity Framework Core 套件

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools &&
dotnet add package Microsoft.EntityFrameworkCore.Design &&
dotnet add package Microsoft.EntityFrameworkCore.Sqlite &&
dotnet add package Microsoft.EntityFrameworkCore.SqlServer &&
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

至 `appsettings.json` 設定資料庫連線字串

```jsonc
"ConnectionStrings": {
  "資料庫連線名稱": "Data Source=資料庫檔案的路徑",
}
```

產生資料庫模型

```bash
dotnet ef dbcontext scaffold Name=資料庫連線名稱 Microsoft.EntityFrameworkCore.Sqlite --output-dir Models
```

## 進行資料庫版控

於 `Startup.cs` 的 `ConfigureServices()` 內添加

```csharp
services.AddDbContext<資料庫名稱Context>(options =>
  options.UseSqlite(Configuration.GetConnectionString("資料庫連線名稱"))
);
```

設置資料庫移轉

```bash
dotnet ef migrations add 版本名稱 --context 資料庫名稱Context
```

> 將所產生的 `*_版本名稱.cs` 內 `up()` 及 `down()` 的內容全部註解掉。

```bash
dotnet ef database update -v --context 資料庫名稱Context
```

## 建立基本 Controller

```bash
dotnet aspnet-codegenerator controller -async -api --relativeFolderPath Controllers --dataContext 資料庫名稱Context --model 資料名稱 --controllerName 資料名稱[複數或單數]Controller
```
