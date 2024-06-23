# EFCore

## 项目情况说明

### EFCoreBasics

[EFCore基本教程-youtube](https://www.youtube.com/watch?v=MkswRgChbEU&list=PLdHN14J7CHtaNHnAPOk_yEq0GidtbTizl
)  

- 正向数据库工程，通过代码实现创建表，实现基础的增删查改。

### ContosoPizza

[Working with an Existing Database [2 of 5] | Entity Framework Core for Beginners](https://www.youtube.com/watch?v=DCYVfLT5_QI)

反向EFCore，从已经建立好的数据库，反向建立EFCore类

## Issues

### 实现反向EFCore

Scaffold-DbContext [数据库连接字符串] [Microsoft.EntityFrameworkCore.SqlServer] -ContextDir Data -OutputDir Models -Project [项目名称]  
将根据连接的数据库，还原全部实体  

### 反向EFCore，给实体属性上增加注释

添加 -DataAnnotation

### 如果要更改数据库框架的处理办法

[更该数据库框架的方法建议](https://www.youtubetrimmer.com/view/?v=DCYVfLT5_QI&start=214)

### 表新增字段的方法

- 更新 Entity 类
---
- dotnet CLI： dotnet ef migrations add AddDescriptionToMyEntity  
**OR**
- Add-Migration Add-Migration AddDescriptionToMyEntity    
生成迁移文件
---
- 检查迁移文件
---
- dotnet CLI：dotnet ef database update  
**OR**
- PowerShell：Update-Database  
更新数据库
---
- 通过代码更新字段  

```CShaper
context.Database.Migrate();  // Apply pending migrations automatically
```




