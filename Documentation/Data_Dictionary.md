# Data Dictionary - Portify: Automated Portfolio Generator

This section contains the data dictionary for the Portify project, providing detailed information about the database structure, table definitions, and field constraints.

---

## 1. Users Table
**Purpose:** Stores information about users (Admins and regular Users).

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for each user. |
| **FullName** | NVARCHAR(100) | NOT NULL | The user's full name. |
| **Email** | NVARCHAR(100) | NOT NULL, UNIQUE | User's email address used for login. |
| **PasswordHash** | NVARCHAR(255) | NOT NULL | Hashed version of the user's password. |
| **Role** | NVARCHAR(20) | NOT NULL, CHECK (Admin/User) | Defines user access level (Admin or User). |
| **IsBlocked** | BIT | DEFAULT(0) | Indicates if the user is blocked from the system. |
| **CreatedAt** | DATETIME | DEFAULT(GETDATE()) | The date and time when the user registered. |

---

## 2. Templates Table
**Purpose:** Stores metadata for available portfolio templates.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the template. |
| **TemplateName** | NVARCHAR(100) | NOT NULL | Name of the template design. |
| **Description** | NVARCHAR(255) | NULL | Brief description of the template's style. |
| **ThumbnailPath** | NVARCHAR(255) | NULL | Path to the preview image for the template. |
| **HtmlPath** | NVARCHAR(255) | NOT NULL | Path to the base HTML file for generation. |
| **CssPath** | NVARCHAR(255) | NOT NULL | Path to the associated CSS file. |
| **JsPath** | NVARCHAR(255) | NULL | Path to the associated JavaScript file. |
| **ConfigPath** | NVARCHAR(255) | NOT NULL | Path to the template configuration file. |
| **IsActive** | BIT | DEFAULT(1) | Flag to show/hide the template for selection. |
| **CreatedAt** | DATETIME | DEFAULT(GETDATE()) | Date the template was added. |

---

## 3. Portfolios Table
**Purpose:** Stores the core portfolio instances created by users.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the portfolio. |
| **UserId** | INT | FOREIGN KEY (Users.Id) | ID of the user who owns the portfolio. |
| **TemplateId** | INT | FOREIGN KEY (Templates.Id) | ID of the selected template. |
| **Title** | NVARCHAR(150) | NULL | User-defined title for the portfolio. |
| **AboutMe** | NVARCHAR(MAX) | NULL | "About Me" section content. |
| **CreatedAt** | DATETIME | DEFAULT(GETDATE()) | Date when the portfolio was generated. |

---

## 4. PortfolioPersonalInfo Table
**Purpose:** Stores personal details specific to a generated portfolio.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the entry. |
| **PortfolioId** | INT | FOREIGN KEY (Portfolios.Id) | Links info to a specific portfolio. |
| **FullName** | NVARCHAR(100) | NULL | Name to display on the portfolio. |
| **Profession** | NVARCHAR(100) | NULL | Professional title (e.g., Full Stack Dev). |
| **Email** | NVARCHAR(100) | NULL | Contact email for the portfolio visitor. |
| **Phone** | NVARCHAR(20) | NULL | Contact phone number. |
| **Location** | NVARCHAR(100) | NULL | User's location (City, Country). |
| **ProfileImagePath**| NVARCHAR(255) | NULL | Path to the uploaded profile picture. |

---

## 5. Experiences Table
**Purpose:** Stores work experience entries for resumes/portfolios.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the entry. |
| **PortfolioId** | INT | FOREIGN KEY (Portfolios.Id) | Links experience to a portfolio. |
| **CompanyName** | NVARCHAR(150) | NULL | Name of the employer/company. |
| **Role** | NVARCHAR(100) | NULL | Job title/position held. |
| **StartDate** | DATE | NULL | Date the role began. |
| **EndDate** | DATE | NULL | Date the role ended (null if current). |
| **Description** | NVARCHAR(MAX) | NULL | Description of responsibilities. |

---

## 6. Projects Table
**Purpose:** Stores project details to be showcased in the portfolio.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the project. |
| **PortfolioId** | INT | FOREIGN KEY (Portfolios.Id) | Links project to a portfolio. |
| **ProjectTitle** | NVARCHAR(150) | NULL | Name of the project. |
| **Description** | NVARCHAR(MAX) | NULL | Overview of the project work. |
| **TechStack** | NVARCHAR(255) | NULL | Technologies used (e.g., React, SQL). |
| **GitHubLink** | NVARCHAR(255) | NULL | URL to the source code repository. |
| **LiveLink** | NVARCHAR(255) | NULL | URL to the hosted project demo. |

---

## 7. Skills Table
**Purpose:** Stores technical or soft skills and their proficiency levels.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the entry. |
| **PortfolioId** | INT | FOREIGN KEY (Portfolios.Id) | Links skill to a portfolio. |
| **SkillName** | NVARCHAR(100) | NULL | Name of the skill (e.g., Java). |
| **SkillLevel** | NVARCHAR(50) | NULL | Level (e.g., Beginner, Advanced). |

---

## 8. SocialLinks Table
**Purpose:** Stores social media URLs for the portfolio.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the link. |
| **PortfolioId** | INT | FOREIGN KEY (Portfolios.Id) | Links social link to a portfolio. |
| **Platform** | NVARCHAR(50) | NULL | e.g. LinkedIn, Twitter, GitHub. |
| **Url** | NVARCHAR(255) | NULL | Link to the user's profile. |

---

## 9. Education Table
**Purpose:** Stores academic qualifications and education history.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the entry. |
| **PortfolioId** | INT | FOREIGN KEY (Portfolios.Id) | Links education to a portfolio. |
| **Degree** | NVARCHAR(150) | NULL | Name of the degree or course. |
| **Institution** | NVARCHAR(150) | NULL | Name of the school, college, or university. |
| **Year** | NVARCHAR(50) | NULL | Passing year or duration. |

---

## 10. Feedback Table
**Purpose:** Stores user ratings and feedback for the platform itself.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the feedback. |
| **UserId** | INT | FOREIGN KEY (Users.Id) | The user giving the feedback. |
| **PortfolioId** | INT | FOREIGN KEY (Portfolios.Id) | Optional reference to a portfolio. |
| **Rating** | INT | CHECK (1-5) | Star rating given by the user. |
| **Message** | NVARCHAR(MAX) | NULL | Review comment or message. |
| **CreatedAt** | DATETIME | DEFAULT(GETDATE()) | Date the feedback was submitted. |

---

## 11. Downloads Table
**Purpose:** Tracks portfolio export/download history.

| Field Name | Data Type | Constraints | Description |
| :--- | :--- | :--- | :--- |
| **Id** | INT | PRIMARY KEY, IDENTITY | Unique identifier for the record. |
| **PortfolioId** | INT | FOREIGN KEY (Portfolios.Id) | The portfolio that was downloaded. |
| **DownloadedAt** | DATETIME | DEFAULT(GETDATE()) | Timestamp of the download action. |
