# EFCore

## 项目情况说明

### EFCoreBasics

[EFCore基本教程-youtube](https://www.youtube.com/watch?v=MkswRgChbEU&list=PLdHN14J7CHtaNHnAPOk_yEq0GidtbTizl
)  

- 正向数据库工程，通过代码实现创建表，实现基础的增删查改。


## Issues

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




