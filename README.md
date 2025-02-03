# dotnet

希望自己能在dotnet的路上持续不断地学习

## Entity Framework 6

- [ ] 使用ef6.exe
- [ ] 创建表
- [ ] 对现有数据库的反向工程

### VisualStudio2022程序包管理器

## Entity Framework Core

前置条件：

- SQL Server 基础知识 [链接](https://www.youtube.com/watch?v=8vTCyhDyRjg&list=PL82C6-O4XrHfZoh2ZH7-HCPyh9oHeYPnz)
- （@"Server = localhost; Database = BloggingDB; Trusted_Connection=True;TrustServerCertificate=True;"）

https://www.youtube.com/watch?v=FNJpXWPka30&list=PLdHN14J7CHtaNHnAPOk_yEq0GidtbTizl

## 迁移，更新

初始化数据库：
dotnet ef migrations add InitialCreate

## 属性关键字

- Required：使用null, 但是null不存储在数据库中
- string?: 允许EFCore知道某列是允许为null的

## ISSUE

### Add-Migration InitialCreate 迁移命令失败？

Visual Studio 中包管理调用失败，使用dotnet

### 链接失败

#### 使用 Windows 身份验证来连接 SQL Server

Trusted_Connection=True

#### 信任服务器的 SSL 证书

TrustServerCertificate=True