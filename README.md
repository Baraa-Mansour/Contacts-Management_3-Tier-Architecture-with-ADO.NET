# 📇 Contacts Management — 3-Tier Architecture with ADO.NET

A .NET Framework 4.8 solution that demonstrates a classic 3-tier architecture using ADO.NET for data access. It includes a Windows Forms UI, a Console test harness, and separate layers for Business Logic, Data Access, and Contracts (DTOs).

The main purpose of this project is to show how to structure a data-driven application into clean, testable layers while using raw ADO.NET.

>⚠️ Practice Project
>
>This is a learning/practice project designed to demonstrate a classic 3-tier architecture using ADO.NET in .NET Framework 4.8.
>
> It is not intended for production use. The code prioritizes clarity of layering and raw ADO.NET techniques over security, performance, or robustness.

## 🎯 Purpose

- Demonstrate a 3-tier architecture:
  - Presentation Tier — WinForms UI and Console App

  - Business Logic Tier — `Contacts-BLL`

  - Data Access Tier — `Contacts-DAL`

  - Contracts / DTOs — `Contacts-Contracts` (shared models)

- Demonstrate ADO.NET fundamentals:
  - Parameterized queries

  - `SqlDataReader` → DTO mapping

  - `DataTable.Load(reader)` for grid binding

  - `ExecuteScalar`, `ExecuteNonQuery`, `ExecuteReader`

  - `SCOPE_IDENTITY()` to return newly inserted IDs

  - `DBNull.Value` handling for nullable columns

- Provide full CRUD for `Contacts` and `Countries`.

## 🏗️ Architecture

```text
┌─────────────────────────────────────────────────────────────┐
│  Presentation Tier                                          │
│  ┌──────────────────────┐   ┌───────────────────────────┐   │
│  │ Contacts-WinForm     │   │ Contacts-ConsoleApp       │   │
│  │ (ContactsFrm,        │   │ (Program.cs test harness) │   │
│  │  ContactAddUpdateFrm)│   │                           │   │
│  └──────────┬───────────┘   └─────────────┬─────────────┘   │
│             │                             │                 │
│             └──────────────┬──────────────┘                 │
│                            ▼                                │
│  Business Logic Tier                                        │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Contacts-BLL                                         │   │
│  │ ContactBL  |  CountryBL                              │   │
│  │ - Mode (ADD/UPDATE)                                  │   │
│  │ - Save(), FindById(), FindByName(), Delete(), etc.   │   │
│  └──────────────────────────┬───────────────────────────┘   │
│                             ▼                               │
│  Data Access Tier                                           │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Contacts-DAL                                         │   │
│  │ ContactDA  |  CountryDA  |  DataAccessSettings       │   │
│  │ - ADO.NET: SqlConnection, SqlCommand, SqlDataReader  │   │
│  └──────────────────────────┬───────────────────────────┘   │
│                             ▼                               │
│                     SQL Server Database                     │
│                     (Contacts, Countries)                   │
└─────────────────────────────────────────────────────────────┘

Shared across all layers:
┌─────────────────────────────────────────────────────────────┐
│ Contacts-Contracts                                          │
│ ContactDto  |  CountryDto                                   │
└─────────────────────────────────────────────────────────────┘
```

### Dependency flow

`WinForm / ConsoleApp → BLL → DAL → SQL Server`
All layers reference `Contacts-Contracts` for DTOs.

## 📂 Solution Structure

```text
Contacts.sln
│
├── Contacts-Contracts/          # Shared DTOs (no dependencies)
│   ├── DTOs/
│   │   ├── ContactDto.cs
│   │   └── CountryDto.cs
│   └── Contacts-Contracts.csproj
│
├── Contacts-DAL/                # Data Access Layer (ADO.NET)
│   ├── ContactDA.cs
│   ├── CountryDA.cs
│   ├── DataAccessSettings.cs    # Connection string
│   └── Contacts-DAL.csproj
│
├── Contacts-BLL/                # Business Logic Layer
│   ├── ContactBL.cs
│   ├── CountryBL.cs
│   └── Contacts-BLL.csproj
│
├── Contacts-ConsoleApp/         # Console test harness
│   ├── Program.cs
│   ├── App.config
│   └── Contacts-ConsoleApp.csproj
│
└── Contacts-WinForm/            # Windows Forms UI
    ├── ContactsFrm.cs
    ├── ContactsFrm.Designer.cs
    ├── ContactAddUpdateFrm.cs
    ├── ContactAddUpdateFrm.Designer.cs
    ├── Program.cs
    ├── App.config
    ├── images/
    └── Contacts-WinForm.csproj
```

## ✨ Features

### Contacts

- Add, update, delete, and list contacts

- Find contact by ID

- Check existence by ID

- Optional profile image path (stored in DB, file loaded from disk)

- Date of birth, address, phone, email, country

### Countries

- Add, update, delete, and list countries

- Find by ID or by name

- Check existence by ID or name

- Optional ISO code and phone code

### WinForms UI

- ContactsFrm — DataGridView with all contacts

- Right-click context menu: Update / Delete

- Add button opens the add/update form

- ContactAddUpdateFrm — shared form for Add and Update modes

- Country dropdown loaded from DB

- Date picker for DOB

- Image picker (OpenFileDialog) + remove image link

- Save button switches the form to Update mode after first save

### Console App

- A test harness in `Program.cs` with methods like:
  - `TestAddContact`, `TestUpdateContact`, `TestDeleteContact`, `TestGetAllContact`, `TestIsExist`

  - `TestAddCountry`, `TestUpdateCountry`, `TestDeleteCountry`, `TestGetAllCountries`, `TestFindCountryByName`, `TestCountryIsExistByName`

## 🗄️ Database Schema

The code expects a SQL Server database named `Contacts` with two tables.

```sql
CREATE DATABASE Contacts;
GO

USE Contacts;
GO

CREATE TABLE Countries
(
    CountryID   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CountryName NVARCHAR(100)     NOT NULL,
    Code        NVARCHAR(10)      NULL,
    PhoneCode   NVARCHAR(10)      NULL
);
GO

CREATE TABLE Contacts
(
    ContactID   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    FirstName   NVARCHAR(50)      NOT NULL,
    LastName    NVARCHAR(50)      NOT NULL,
    Email       NVARCHAR(100)     NOT NULL,
    Phone       NVARCHAR(30)      NOT NULL,
    Address     NVARCHAR(200)     NOT NULL,
    DateOfBirth DATETIME          NOT NULL,
    CountryID   INT               NOT NULL,
    ImagePath   NVARCHAR(255)     NULL,
    CONSTRAINT FK_Contacts_Countries
        FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);
GO
```

## 🚀 Getting Started

### Requirements

- Windows OS

- Visual Studio 2019 / 2022 with .NET desktop development workload

- .NET Framework 4.8

- SQL Server (Express or full) with a `Contacts` database

### Steps

1. Clone the repository

   ```bash
   git clone https://github.com/Baraa-Mansour/Contacts-Management_3-Tier-Architecture-with-ADO.NET.git
   ```

2. Create the database

   Run the SQL script above in SQL Server Management Studio.

3. Update the connection string

   Open `Contacts-DAL/DataAccessSettings.cs` and adjust:

   ```csharp
   public const string connectionString =
       "server=.;database=Contacts;user id=sa;password=sa123456";
   ```

4. Run
   - Set `Contacts-WinForm` as the startup project to use the GUI.

   - Or set `Contacts-ConsoleApp` as startup to run the console tests.

## 🕹️ Usage

### Windows Forms

1. Launch `Contacts-WinForm`.

2. The main grid loads all contacts from the database.

3. Click Add to create a new contact.

4. Right-click any row → Update or Delete.

5. In the add/update form:

    - Fill in first name, last name, email, phone, address, DOB, country.

    - Click Set Image to attach a profile picture (optional).

    - Click Remove Image to clear it.

    - Click Save to persist.

6. After a successful save, the form switches to Update Contact mode and shows the new ID.

### Use Console App

1. Open `Contacts-ConsoleApp/Program.cs` and uncomment the test you want to run.

2. Run the console project to see the results printed to the terminal.
