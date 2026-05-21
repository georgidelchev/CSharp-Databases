<div align="center">

# C# Databases

**MS SQL Server & Entity Framework Core — SoftUni Course Repository**

[![SoftUni](https://img.shields.io/badge/SoftUni-Databases-7C3AED?style=flat-square&logo=academia&logoColor=white)](https://softuni.bg)
[![T-SQL](https://img.shields.io/badge/T--SQL-49%25-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://github.com/georgidelchev/CSharp-Databases/search?l=tsql)
[![C#](https://img.shields.io/badge/C%23-49%25-239120?style=flat-square&logo=csharp&logoColor=white)](https://github.com/georgidelchev/CSharp-Databases/search?l=c%23)
[![License](https://img.shields.io/badge/License-MIT-6366F1?style=flat-square)](LICENSE)
[![Stars](https://img.shields.io/github/stars/georgidelchev/CSharp-Databases?style=flat-square&color=F59E0B)](https://github.com/georgidelchev/CSharp-Databases/stargazers)
[![Forks](https://img.shields.io/github/forks/georgidelchev/CSharp-Databases?style=flat-square&color=22C55E)](https://github.com/georgidelchev/CSharp-Databases/network/members)

<br/>

*From raw SQL queries and database design to full ORM mastery with Entity Framework Core —*  
*plus real-world competitive SQL problems from HackerRank.*

<br/>

</div>

---

## 📖 About

This repository contains my coursework and solutions for the **C# Databases** track at [SoftUni](https://softuni.bg), covering two major pillars of modern data-driven .NET development:

- **MS SQL Server** — relational database design, T-SQL querying, stored procedures, functions, triggers, transactions, and performance optimization.
- **Entity Framework Core** — the leading ORM for .NET: code-first modeling, migrations, LINQ queries, relationships, and advanced EF Core patterns.

Additionally, the repo includes a bonus collection of competitive SQL problems solved on **HackerRank**.

---

## 🗂️ Repository Structure

### 🗄️ 01 — MS SQL Problems

> *Mastering relational databases and T-SQL from the ground up.*

A comprehensive collection of SQL exercises covering:

- Database design & normalization (1NF, 2NF, 3NF)
- DDL — creating tables, constraints, indexes
- DML — `SELECT`, `INSERT`, `UPDATE`, `DELETE`
- Joins, subqueries, CTEs, and window functions
- Stored procedures, user-defined functions & triggers
- Transactions and error handling
- Performance tuning & execution plans

📁 [Browse module →](./01%20-%20%5BMS%20SQL%20Problems%5D)

---

### ⚙️ 02 — Entity Framework Core

> *Working with databases the .NET way — code-first, type-safe, and productive.*

In-depth EF Core coverage including:

- Code-First approach & database migrations
- DbContext, DbSet, and model configuration (Fluent API & Data Annotations)
- One-to-one, one-to-many, and many-to-many relationships
- LINQ queries — filtering, projection, grouping, aggregation
- Eager, lazy, and explicit loading
- Transactions & concurrency handling
- AutoMapper integration & DTOs
- JSON processing with EF Core

📁 [Browse module →](./02%20-%20%5BEntity%20Framework%20Core%5D)

---

### 🏆 HackerRank — MS SQL Server Problems

> *Competitive SQL problem solving on a real-world judge platform.*

A curated set of HackerRank SQL challenges solved in T-SQL — ranging from basic queries to advanced analytical problems.

📁 [Browse solutions →](./HackerRank%20-%20MS%20SQL%20SERVER%20Problems)

---

## 🛠️ Tech Stack

| Layer | Technologies |
|-------|-------------|
| Database | Microsoft SQL Server (MSSQL) |
| Query Language | T-SQL |
| ORM | Entity Framework Core |
| Language | C# (.NET) |
| IDE | Visual Studio / SSMS |
| Platform | [SoftUni](https://softuni.bg) · [HackerRank](https://www.hackerrank.com) |

---

## 🚀 Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/georgidelchev/CSharp-Databases.git
   cd CSharp-Databases
   ```

2. **For SQL scripts** — open any `.sql` file in SQL Server Management Studio (SSMS) or Azure Data Studio and execute against your local MSSQL instance.

3. **For EF Core projects** — open the `.sln` file in Visual Studio, then apply migrations:
   ```bash
   dotnet restore
   dotnet ef database update
   dotnet run
   ```

> **Prerequisites:** SQL Server (2019+), .NET SDK, Visual Studio or Rider.

---

## 👤 Author

**Georgi Delchev**

[![GitHub](https://img.shields.io/badge/GitHub-georgidelchev-181717?style=flat-square&logo=github)](https://github.com/georgidelchev)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-delchevgeorgi-0A66C2?style=flat-square&logo=linkedin)](https://www.linkedin.com/in/delchevgeorgi/)
[![Facebook](https://img.shields.io/badge/Facebook-georgi.d99-1877F2?style=flat-square&logo=facebook)](https://www.facebook.com/georgi.d99/)

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

<div align="center">
<sub>Made with ☕ and determination · <a href="https://softuni.bg">SoftUni</a></sub>
</div>
