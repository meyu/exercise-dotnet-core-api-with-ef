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
  "資料庫連線名稱(SQLite)": "Data Source=資料庫檔案的路徑",
  "資料庫連線名稱(MSSQL)": "Server=(localdb)\\MSSQLLocalDB;Database=資料庫名稱;Trusted_Connection=True;"
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

> DB First 提醒：記得將此時產生的初版設定 `*_版本名稱.cs` 內 `up()` 及 `down()` 的內容先全部註解掉。

```bash
dotnet ef database update -v --context 資料庫名稱Context
```

## 建立 API (Controller)

由指令產生

```bash
dotnet aspnet-codegenerator controller -async -api --relativeFolderPath Controllers --dataContext 資料庫名稱Context --model 資料名稱 --controllerName 資料名稱[複數或單數]Controller
```

以手工打造

```csharp
// 多筆
[HttpGet("自訂路徑")]
public async Task<ActionResult<IEnumerable<模型名稱>>> Get自訂名稱()
{
    return await _context.模型名稱.ToListAsync();
}
// 單筆
[HttpGet("{id:int}")]
public async Task<ActionResult<IEnumerable<模型名稱>>> Get自訂名稱(long id)
{
    var r = await (
        from d in _context.模型名稱
        where d.欄位名稱 == id
        select d
    ).SingleAsync();

    if (r == null)
    {
        return NotFound();
    }
    return r;
}
```

懷念 SQL

```csharp
// 簡易
[HttpGet("自訂路徑")]
public async Task<ActionResult<IEnumerable<模型名稱>>> Get自訂名稱()
{
    return await _context.模型名稱
    .FromSqlRaw("SELECT * FROM 資料表/檢示表; EXEC dbo.預儲程式;")
    .ToListAsync();
}
// 加參數
[HttpGet("{id:int}")]
public async Task<ActionResult<模型名稱>> Get自訂名稱(long id)
{
    var 參數 = id;
    var r = await _context.模型名稱
    .FromSqlInterpolated($"SELECT * FROM 資料 WHERE 欄位 = {參數}")
    .SingleAsync();

    if (r == null)
    {
        return NotFound();
    }
    return r;
}
```

## 強化資料異動作業

### 擴充資料欄位

1. 編修 Models 的屬性
1. 進行資料庫移輚

### 覆寫共用之儲存方案

於 Context 內 override OnSaveSync()

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
{
    var entities = this.ChangeTracker.Entries();
    foreach (var entry in entities)
    {
        if (entry.State == EntityState.Modified)
        {
          // 若有該屬性，則寫入指定資料；若無，則沒關係，系統不會中斷。
          entry.CurrentValues.SetValues(new { 共有之屬性名稱 = 指定資料 });
        }
    }
    return await base.SaveChangesAsync();
}
```

### 刪除，只是標記為刪除

修改 Controller 的 HttpDelete 介面，將

```csharp
_context.資料模型.Remove(傳入之資料);
```

改為

```csharp
傳入之資料.標記為刪除的屬性名稱 = true;
_context.資料模型.Update(傳入之資料);`
```

## 還沒想清楚的事

- Model 的時間型別
- 寫入的時間不像當下時間
- 如何留存/查看 CUD 的 LOG
- SaveChangesAsync 裡的 Console.Write 會顯示在哪裡
- 編寫 Store Procedure，要在資料庫裡？會影響到 migrations 嗎？
- 目前沒能成功讀取 user-secrets...
- connectionString 在正式/開發環境中，各應存放在何處？ 推薦的做法是...?

## 額外了解到的冷知識備忘

- ef migrations 的 database update 會 create database 嗎？ 會，另外，sqlite 也會以新增一個資料庫檔案作收。
- 有多個 Context (或資料庫) 時，ef 的指令要指定好 --context 才行
- 在 vs code 裡，可使用 shift+cmd+b 來進行 build，但要先在 .vscode/tasks.json 裡設定好。

## Code First 的作法

1. 建立資料模型：於 `Models` 資料夾下建立 `資料庫名稱.cs`。範例：

   ```csharp
   namespace 專案名稱.Models {
       public class Blog {
           public int BlogId { get; set; }
           public string Url { get; set; }
           public int Rating { get; set; }
           public List<Post> Posts { get; set; }
       }
       public class Post {
           public int PostId { get; set; }
           public string Title { get; set; }
           public string Content { get; set; }
           public int BlogId { get; set; }
           public Blog Blogs { get; set; }
       }
       public class 資料庫名稱Context : DbContext {
           public DbSet<Blog> Blogs { get; set; }
           public DbSet<Post> Posts { get; set; }
           public 資料庫名稱Context (DbContextOptions<資料庫名稱Context> options) : base (options) { }
       }
   }
   ```

2. 至 `appsettings.json` 設定資料庫連線字串。
3. 於 `Startup.cs` 的 `ConfigureServices()` 內註冊連線 Provider。
4. 進行資料庫移轉。
