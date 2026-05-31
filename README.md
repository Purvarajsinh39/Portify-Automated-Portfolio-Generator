<!-- <p align="center">
  <img src="favicon.ico" alt="Portify Logo" width="80" />
</p> -->

<h1 align="center">Portify - Automated Portfolio Generator</h1>

<p align="center">
  <b>Build. Customize. Deploy. Share.</b><br/>
  A full-stack ASP.NET MVC web application that lets anyone create a professional portfolio website in minutes - no coding required.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Framework-ASP.NET%20MVC%205-blueviolet" />
  <img src="https://img.shields.io/badge/.NET-4.7.2-blue" />
  <img src="https://img.shields.io/badge/Database-SQL%20Server-orange" />
  <img src="https://img.shields.io/badge/AI-Gemini%20API-green" />
  <img src="https://img.shields.io/badge/Auth-Google%20OAuth-red" />
  <img src="https://img.shields.io/badge/Jobs-Hangfire-yellow" />
</p>

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Problem Statement](#2-problem-statement)
3. [Objectives](#3-objectives)
4. [Technologies Used](#4-technologies-used)
5. [Architecture Explanation](#5-architecture-explanation)
6. [Complete Workflow](#6-complete-workflow)
7. [Database Tables and Relationships](#7-database-tables-and-relationships)
8. [Source Code Structure Explanation](#8-source-code-structure-explanation)
9. [API Documentation](#9-api-documentation)
10. [Authentication & Security](#10-authentication--security)
11. [Design Patterns Used](#11-design-patterns-used)
12. [Key Features](#12-key-features)
13. [Deployment Process](#13-deployment-process)
14. [Challenges and Solutions](#14-challenges-and-solutions)
15. [Future Enhancements](#15-future-enhancements)

---

## 1. Project Overview

**Portify** is a web-based Automated Portfolio Generator built using **ASP.NET MVC 5** with **.NET Framework 4.7.2** and **Microsoft SQL Server**. It enables users - students, professionals, and freelancers - to create, customize, and deploy professional portfolio websites without writing a single line of code.

Users sign up, pick a template from 8 beautifully designed options, fill in their details through an interactive editor with live preview, and click "Save". Portify automatically generates a static HTML portfolio file, stores all data in a normalized relational database, and hosts the portfolio at a shareable URL - all in one click.

The platform also integrates **Google Gemini AI** for auto-generating "About Me" sections, an **AI-powered chatbot** for user support, **Hangfire** for background email processing, **Google OAuth** for one-click login, and a full **admin dashboard** with analytics, user management, and template management.

### At a Glance

| Aspect | Details |
|---|---|
| **Project Name** | Portify — Automated Portfolio Generator |
| **Type** | Full-Stack Web Application |
| **Framework** | ASP.NET MVC 5 (.NET Framework 4.7.2) |
| **Database** | Microsoft SQL Server (with ADO.NET) |
| **Frontend** | Bootstrap 5, jQuery 3.7, Custom CSS |
| **AI Integration** | Google Gemini API (Flash model) |
| **Authentication** | Session-based + Google OAuth 2.0 |
| **Background Jobs** | Hangfire with SQL Server storage |
| **Email** | SMTP (Gmail) with HTML templates |
| **Deployment** | Somee.com (Prod) / Azure App Service |
| **Templates** | 8 fully designed, responsive portfolio templates |

---

## 2. Problem Statement

Creating a professional portfolio website is essential for students, developers, designers, and freelancers to showcase their work. However, the process faces several challenges:

1. **Technical Barrier**: Building a portfolio requires knowledge of HTML, CSS, JavaScript, and hosting - skills that many non-technical users lack.
2. **Time-Consuming**: Even for developers, designing and coding a portfolio from scratch takes hours or days.
3. **Cost**: Professional portfolio builders (Wix, Squarespace) charge monthly subscriptions (₹500–₹2000/month).
4. **No Centralized Data**: Manually created portfolios scatter information across files with no structured storage.
5. **Lack of AI Assistance**: Users struggle to write compelling professional summaries and "About Me" sections.
6. **No Easy Sharing**: Generated portfolios often require separate hosting setup, making sharing difficult.

**Portify solves all these problems** by providing a free, template-based portfolio generator with AI assistance, one-click deployment, and shareable links.

---

## 3. Objectives

| # | Objective | How Portify Achieves It |
|---|---|---|
| 1 | Eliminate the need for coding knowledge | Template-based system with visual editor |
| 2 | Generate portfolios in under 5 minutes | Pre-built templates + auto-generation |
| 3 | Provide AI-powered content assistance | Google Gemini API integration for "About Me" |
| 4 | Enable one-click portfolio deployment | Static HTML generation with shareable URLs |
| 5 | Support multiple authentication methods | Email/Password + Google OAuth 2.0 |
| 6 | Implement email verification for security | OTP-based email verification via Hangfire |
| 7 | Provide admin oversight and analytics | Full admin dashboard with charts and user management |
| 8 | Allow portfolio download and sharing | HTML file download + "Copy Link" functionality |
| 9 | Offer AI-powered user support | Gemini-powered chatbot for instant help |
| 10 | Implement notification system | In-app + email notifications for new templates |

---

## 4. Technologies Used

### Backend

| Technology | Version | Purpose & Benefit |
|---|---|---|
| **ASP.NET MVC 5** | 5.2.9 | Web framework providing Model-View-Controller architecture for clean separation of concerns |
| **.NET Framework** | 4.7.2 | Runtime providing extensive class libraries, type safety, and Windows integration |
| **ADO.NET (SqlClient)** | Built-in | Direct database access using parameterized queries for full SQL control and performance |
| **Hangfire** | 1.8.23 | Background job processing for asynchronous email sending without blocking user requests |
| **Newtonsoft.Json** | 13.0.3 | JSON serialization/deserialization for API communication with Google Gemini |
| **Microsoft OWIN** | 3.0.0 | Middleware pipeline for hosting Hangfire dashboard within the MVC application |
| **Google.Apis.Auth** | 1.73.0 | Server-side Google JWT token validation for secure OAuth authentication |

### Frontend

| Technology | Version | Purpose & Benefit |
|---|---|---|
| **Bootstrap 5** | 5.2.3 | Responsive CSS framework for mobile-friendly, professional UI components |
| **jQuery** | 3.7.0 | DOM manipulation, AJAX calls, and event handling for interactive features |
| **jQuery Validation** | 1.19.5 | Client-side form validation for instant user feedback before server submission |
| **Font Awesome** | CDN | Scalable vector icons for navigation, buttons, and UI elements |
| **Chart.js** | CDN | Interactive charts in admin dashboard for visualizing usage statistics |
| **Custom CSS** | — | Chatbot styling, page transitions, and template-specific designs |

### Database

| Technology | Purpose & Benefit |
|---|---|
| **Microsoft SQL Server** | Enterprise-grade RDBMS with ACID compliance, stored procedures support, and robust indexing |
| **SQL Server Express** | Free local development database with full SQL Server compatibility |
| **Somee.com SQL Hosting** | Cloud-hosted SQL Server for production deployment |

### AI & External APIs

| Technology | Purpose & Benefit |
|---|---|
| **Google Gemini API** | AI content generation - auto-writes professional "About Me" summaries from user data |
| **Google OAuth 2.0** | One-click social login - reduces registration friction and improves user experience |
| **Gmail SMTP** | Transactional email delivery - sends OTPs, block/unblock notifications, and template alerts |

### DevOps & Tooling

| Technology | Purpose & Benefit |
|---|---|
| **Visual Studio 2022** | Full-featured IDE with IntelliSense, debugging, and NuGet package management |
| **NuGet** | .NET package manager for dependency management |
| **Git / GitHub** | Version control and source code collaboration |
| **Somee.com / Azure** | Production hosting for both web application and SQL Server database |

---

## 5. Architecture Explanation

Portify follows the **MVC (Model-View-Controller)** architectural pattern with additional service and data-access layers:

```
┌─────────────────────────────────────────────────────────────────┐
│                        CLIENT BROWSER                           │
│  (Bootstrap 5 + jQuery + Chart.js + Custom CSS/JS)              │
└───────────────────────────┬─────────────────────────────────────┘
                            │ HTTP Requests (GET/POST)
                            ▼
┌─────────────────────────────────────────────────────────────────┐
│                      ASP.NET MVC 5 SERVER                       │
│                                                                 │
│  ┌─────────────┐   ┌──────────────────┐   ┌─────────────────┐  │
│  │ Controllers  │──▶│     Models       │──▶│     Views       │  │
│  │             │   │  (Domain + DTOs) │   │  (.cshtml)      │  │
│  │ Home        │   │  User            │   │  Home/          │  │
│  │ Dashboard   │   │  Portfolio       │   │  Admin/         │  │
│  │ Portfolio   │   │  Template        │   │  User/          │  │
│  │ Chatbot     │   │  PortfolioData   │   │  Portfolio/     │  │
│  └──────┬──────┘   │  Feedback        │   │  Shared/        │  │
│         │          │  Notification    │   └─────────────────┘  │
│         │          └──────────────────┘                         │
│         ▼                                                       │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │              PortifyDbContext (Data Access Layer)         │   │
│  │         Raw ADO.NET with Parameterized SQL Queries        │   │
│  └────────────────────────┬─────────────────────────────────┘   │
│                           │                                     │
│  ┌────────────────┐  ┌────┴──────┐  ┌──────────────────────┐   │
│  │  EmailService  │  │ Hangfire  │  │  Google Gemini API   │   │
│  │  (SMTP Gmail)  │  │  (Jobs)   │  │  (AI Generation)     │   │
│  └────────────────┘  └──────────┘  └──────────────────────┘   │
└───────────────────────────┬─────────────────────────────────────┘
                            │ SQL Queries
                            ▼
┌─────────────────────────────────────────────────────────────────┐
│                   MICROSOFT SQL SERVER                           │
│                                                                 │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────────────┐   │
│  │  Users   │ │Templates │ │Portfolios│ │ HangFire Schema  │   │
│  │  Otps    │ │ Skills   │ │ Projects │ │  (Job Queue)     │   │
│  │Feedback  │ │Education │ │SocialLink│ │                  │   │
│  └──────────┘ └──────────┘ └──────────┘ └──────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
```

### Layer Responsibilities

| Layer | Responsibility |
|---|---|
| **Views** | Razor (.cshtml) pages rendering HTML with dynamic C# data binding |
| **Controllers** | Handle HTTP requests, enforce authentication, orchestrate business logic |
| **Models** | Domain entities (User, Portfolio, Template) + DTOs (PortfolioData) |
| **PortifyDbContext** | Data Access Layer - all SQL queries via ADO.NET with parameterized statements |
| **EmailService** | Constructs and sends HTML emails via Gmail SMTP |
| **Hangfire** | Processes background jobs (email sending) using SQL Server as persistent storage |
| **Template Engine** | Custom `RenderTemplate()` method replacing `{{placeholders}}` in HTML templates |

---

## 6. Complete Workflow

### 6.1 User Registration Flow

```
User enters details → Register(POST) → Check if email exists
    → If exists: show error
    → If new: Save to PendingRegistrations table
        → Generate 6-digit OTP → Save to Otps table
        → Enqueue email via Hangfire → Redirect to VerifyOtp page
            → User enters OTP → Validate against Otps table
                → If valid: Move user from PendingRegistrations to Users table
                → If invalid/expired: Show error, allow resend
```

### 6.2 Login Flow

```
User enters Email + Password → Login(POST) → Query Users table
    → If credentials match:
        → Check IsBlocked flag
            → If blocked: Show "You are blocked" message
            → If not: Set Session (UserId, UserRole, UserName) → Redirect to Home
    → If no match: Show "Invalid Email or Password"

Google Login:
    User clicks Google Sign-In → GoogleLogin(POST) → Validate JWT token
        → If email exists in Users: Set session → Redirect
        → If new: Auto-create User with random password → Set session → Redirect
```

### 6.3 Portfolio Creation Flow

```
User browses templates (Explore page) → Selects a template → Opens Editor
    → Fills in:
        • Personal Info (Name, Profession, Email, Phone, Location)
        • Skills (Name + Level)
        • Projects (Title, Description, TechStack, GitHub, Live links)
        • Experience (Company, Role, Dates, Description)
        • Education (Degree, Institution, Year)
        • Social Links (GitHub, LinkedIn)
        • About Me (manual or AI-generated via Gemini)
    → Live Preview updates in real-time via AJAX
    → Click "Save Portfolio" → Save(POST):
        1. Insert into Portfolios table (get new ID via SCOPE_IDENTITY)
        2. Insert into PortfolioPersonalInfo
        3. Insert into Skills (loop)
        4. Insert into Projects (loop)
        5. Insert into Experiences (loop)
        6. Insert into SocialLinks
        7. Insert into Education (loop)
        8. Read template HTML → Replace {{placeholders}} → Write static .html file
        9. Redirect to Dashboard with success message
```

### 6.4 Admin Workflow

```
Admin logs in → AdminDashboard:
    • View statistics (Total Users, Portfolios, Templates, Avg Rating)
    • View charts (Template Usage pie chart, Daily Portfolios, Daily Registrations)

Admin actions:
    • Manage Templates → Upload (ZIP/HTML), Toggle Active/Inactive, Delete
    • Manage Users → Block (with reason + email notification), Unblock (with email)
    • Manage Portfolios → View all user portfolios
    • View Feedback → See ratings and messages from users
```

---

## 7. Database Tables and Relationships

### 7.1 Entity-Relationship Overview

```
Users (1) ──────── (N) Portfolios (N) ──────── (1) Templates
  │                      │
  │                      ├── (1:1) PortfolioPersonalInfo
  │                      ├── (1:N) Skills
  │                      ├── (1:N) Projects
  │                      ├── (1:N) Experiences
  │                      ├── (1:N) SocialLinks
  │                      ├── (1:N) Education
  │                      ├── (1:N) Downloads
  │                      └── (1:N) Feedback
  │
  ├── (1:N) Notifications
  └── (1:N) Feedback

Otps (standalone — linked by Email string)
PendingRegistrations (standalone — temporary staging table)
HangFire.* (10 tables — managed by Hangfire library)
```

### 7.2 Application Tables (15 total)

| # | Table | Rows Purpose | Key Relationships |
|---|---|---|---|
| 1 | **Users** | User accounts (Admin + Regular) | PK: Id. Unique: Email. CHECK: Role in ('Admin','User') |
| 2 | **Templates** | Portfolio template metadata | PK: Id. Referenced by Portfolios.TemplateId |
| 3 | **Portfolios** | Portfolio instances | PK: Id. FK → Users.Id, Templates.Id |
| 4 | **PortfolioPersonalInfo** | Name, profession, contact | PK: Id. FK → Portfolios.Id (1:1) |
| 5 | **Skills** | Skill entries | PK: Id. FK → Portfolios.Id |
| 6 | **Projects** | Project showcase entries | PK: Id. FK → Portfolios.Id |
| 7 | **Experiences** | Work experience entries | PK: Id. FK → Portfolios.Id |
| 8 | **Education** | Academic qualifications | PK: Id. FK → Portfolios.Id |
| 9 | **SocialLinks** | GitHub, LinkedIn URLs | PK: Id. FK → Portfolios.Id |
| 10 | **Testimonials** | Testimonial entries | PK: Id. FK → Portfolios.Id |
| 11 | **Feedback** | User ratings & reviews | PK: Id. FK → Users.Id, Portfolios.Id, Templates.Id |
| 12 | **Notifications** | In-app notification messages | PK: Id. FK → Users.Id |
| 13 | **Downloads** | Portfolio download tracking | PK: Id. FK → Portfolios.Id |
| 14 | **Otps** | OTP codes for verification | PK: Id. Columns: Email, Code, Purpose, ExpiresAt, IsUsed |
| 15 | **PendingRegistrations** | Temporary pre-verification data | PK: Id. Stores user data before OTP confirmation |

### 7.3 Constraints Summary

- **Foreign Keys**: 14 FK constraints enforcing referential integrity
- **CHECK Constraints**: `Users.Role` restricted to 'Admin'/'User'; `Feedback.Rating` restricted to 1–5
- **UNIQUE**: `Users.Email` ensures no duplicate accounts
- **DEFAULTS**: `GETDATE()` for timestamps, `0` for IsBlocked/IsRead, `1` for IsActive/IsPublished

---

## 8. Source Code Structure Explanation

```
d:\Portify\
│
├── Portify.sln                       # Visual Studio solution file
├── Portify.csproj                    # Project file with all references and build config
├── Web.config                        # Main configuration (connection strings, assembly bindings)
├── AppSettings.config                # External config (API keys, DB connection strings, env switch)
├── Startup.cs                        # OWIN Startup — configures Hangfire server & dashboard
├── Global.asax / Global.asax.cs      # Application lifecycle — registers routes, filters, bundles
├── packages.config                   # NuGet package references
├── PortifyDB.sql                     # Complete database schema with all tables, FKs, indexes
│
├── Controllers/                      # REQUEST HANDLERS (Business Logic Layer)
│   ├── HomeController.cs             # Landing page, Login, Register, OTP, ForgotPassword, Logout
│   ├── DashboardController.cs        # User Dashboard, Admin Dashboard, Template/User management
│   ├── PortfolioController.cs        # Editor, Save, Preview, Download, Feedback, AI generation
│   └── ChatbotController.cs          # AI chatbot endpoint (Gemini API integration)
│
├── Models/                           # DATA MODELS + SERVICES
│   ├── User.cs                       # User entity with Data Annotations
│   ├── Portfolio.cs                  # Portfolio + related entities (Skill, Project, Experience, etc.)
│   ├── PortfolioData.cs              # DTO for JSON form data from Editor
│   ├── Template.cs                   # Template metadata entity
│   ├── Feedback.cs                   # Feedback entity with display properties
│   ├── Notification.cs               # Notification entity
│   ├── AdminDashboardViewModel.cs    # ViewModel for admin analytics (stats + chart data)
│   ├── PortifyDbContext.cs           # DATA ACCESS LAYER — 1076 lines of ADO.NET queries
│   ├── EmailService.cs               # HTML email templates + SMTP sending logic
│   └── HangfireAuthorizationFilter.cs  # Restricts Hangfire dashboard to admins/localhost
│
├── Views/                            # RAZOR VIEWS (Presentation Layer)
│   ├── Shared/                       # Shared layouts and partials
│   │   ├── _Layout.cshtml            # Master layout template
│   │   ├── _AdminNavbar.cshtml       # Admin navigation with sidebar
│   │   ├── _UserNavbar.cshtml        # User navigation with notification badge
│   │   ├── _Chatbot.cshtml           # Chatbot widget HTML (partial view)
│   │   ├── _PageTransitions.cshtml   # Page transition animations
│   │   └── Error.cshtml              # Error page
│   ├── Home/                         # Public-facing views
│   │   ├── Index.cshtml              # Landing page (hero, features, testimonials)
│   │   ├── Login.cshtml              # Login + Registration combined page
│   │   ├── VerifyOtp.cshtml          # OTP verification page
│   │   ├── ForgotPassword.cshtml     # Forgot password page
│   │   └── ResetPassword.cshtml      # Password reset page
│   ├── User/                         # Authenticated user views
│   │   ├── UserDashboard.cshtml      # Portfolio listing with download/copy-link/delete
│   │   ├── Explore.cshtml            # Template gallery with previews
│   │   ├── MyProfile.cshtml          # Profile settings (name, password, notifications)
│   │   └── Notifications.cshtml      # Notification inbox
│   ├── Admin/                        # Admin-only views
│   │   ├── AdminDashboard.cshtml     # Statistics cards + Chart.js analytics
│   │   ├── ManageTemplates.cshtml    # Template CRUD operations
│   │   ├── UploadTemplate.cshtml     # Template upload form (ZIP/HTML)
│   │   ├── UserManagement.cshtml     # Block/unblock users with reason
│   │   ├── ManagePortfolios.cshtml   # View all portfolios across users
│   │   └── AdminFeedback.cshtml      # View all user feedback and ratings
│   └── Portfolio/
│       └── Editor.cshtml             # Interactive portfolio editor with live preview
│
├── Templates/                        # PORTFOLIO TEMPLATES (8 designs)
│   ├── Template 1/ through Template 8/   # Individual template folders
│   └── Portfolio1.html through Portfolio8.html  # Standalone template files
│
├── Portfolios/                       # GENERATED STATIC PORTFOLIOS (user output)
│
├── Content/                          # CSS FILES
│   ├── bootstrap.css + variants      # Bootstrap 5 framework
│   ├── chatbot.css                   # AI chatbot widget styling
│   └── Site.css                      # Global custom styles
│
├── Scripts/                          # JAVASCRIPT FILES
│   ├── bootstrap.bundle.js           # Bootstrap 5 JS
│   ├── jquery-3.7.0.js               # jQuery library
│   ├── chatbot.js                    # Chatbot UI logic (toggle, send, receive, format)
│   └── jquery.validate*.js           # Form validation scripts
│
├── Documentation/                    # PROJECT DOCUMENTATION
│   ├── Data_Dictionary.md            # Complete data dictionary for all tables
│   ├── Deployed.md                   # Deployment feature documentation
│   ├── Diagrams/                     # UML Diagrams (Use Case, Activity, Class)
│   ├── Portify Documentation.pdf     # Full project documentation
│   └── Portify.pptx                  # Presentation slides
│
├── App_Start/                        # APPLICATION CONFIGURATION
│   ├── RouteConfig.cs                # URL routing ({controller}/{action}/{id})
│   ├── BundleConfig.cs               # CSS/JS bundling and minification
│   └── FilterConfig.cs               # Global MVC filters
│
└── fonts/, bin/, obj/, packages/     # Fonts, compiled output, NuGet packages
```

---

## 10. Authentication & Security

### 10.1 Authentication Methods

| Method | Implementation |
|---|---|
| **Email + Password** | Manual login via `HomeController.Login()`. Credentials checked against Users table. |
| **Google OAuth 2.0** | Google Sign-In button → JWT token sent to `HomeController.GoogleLogin()` → Validated server-side using `GoogleJsonWebSignature.ValidateAsync()` with audience check. |
| **OTP Email Verification** | 6-digit OTP generated, stored in Otps table with 5-minute expiry, sent via Hangfire background job. |

### 10.2 Session Management

```csharp
Session["UserId"]   = user.Id;       // Integer — used for all authorization checks
Session["UserRole"] = user.Role;     // "Admin" or "User" — determines access level
Session["UserName"] = user.FullName; // Display name in navbar
```

Every controller action checks `Session["UserId"]` before proceeding. Admin-only actions additionally verify `Session["UserRole"] == "Admin"`.

### 10.3 Security Features

| Feature | Implementation |
|---|---|
| **SQL Injection Prevention** | All queries use parameterized `SqlCommand` with `@parameters` |
| **Session-based Auth Guards** | Every protected endpoint checks Session before executing |
| **Role-based Access Control** | Admin vs User separation with Session["UserRole"] checks |
| **Blocked User Prevention** | `IsBlocked` flag checked during login - blocked users cannot access the system |
| **OTP Expiry** | OTPs expire after 5 minutes. Previous unused OTPs are invalidated on resend |
| **Google JWT Validation** | Server-side audience verification prevents token forgery |
| **Hangfire Dashboard Protection** | `HangfireAuthorizationFilter` restricts access to admins and localhost |
| **Input Validation** | `[ValidateInput(false)]` used only for HTML content; jQuery Validation on frontend |
| **Transactional Deletes** | Portfolio deletion uses `SqlTransaction` to ensure atomic cleanup of related records |

---

## 11. Design Patterns Used

| Pattern | Where Used | Explanation |
|---|---|---|
| **MVC (Model-View-Controller)** | Entire application | Controllers handle requests, Models define data, Views render HTML |
| **Repository Pattern (Simplified)** | `PortifyDbContext` | Single class encapsulating all database operations - acts as a data access repository |
| **DTO (Data Transfer Object)** | `PortfolioData`, `SkillEntry`, `ProjectEntry` | Flat objects that carry editor form data between client and server |
| **ViewModel** | `AdminDashboardViewModel` | Aggregates multiple data sources (stats + chart data) for a single view |
| **Template Method** | `RenderTemplate()` | Reads HTML template, replaces `{{placeholders}}` with user data - same algorithm, different templates |
| **Service Layer** | `EmailService` | Encapsulates email construction and SMTP sending, called by controllers |
| **Background Job / Fire-and-Forget** | Hangfire `BackgroundJob.Enqueue()` | Email sending is offloaded to background threads so users aren't blocked |
| **Guard Clause** | `GuardUser()` in PortfolioController | Early-return pattern checking authentication before method body executes |
| **Factory Method (Implicit)** | `MapUser()`, `MapTemplate()`, etc. | Static factory methods that construct domain objects from `SqlDataReader` |
| **Configuration Pattern** | `AppSettings.config` + `GetConnectionString()` | Environment-aware configuration switching between dev/prod connection strings |

---

## 12. Key Features

### For Users

- **8 Professional Templates** - Responsive, modern designs for different professions
- **Interactive Portfolio Editor** - Form-based editor with dynamic skill/project/experience sections
- **Live Preview** - Real-time portfolio preview via AJAX as you type
- **AI-Powered "About Me" Generator** - Gemini AI writes your professional summary
- **AI Chatbot Support** - Gemini-powered assistant for help and guidance
- **One-Click Deploy** - Static HTML automatically generated and hosted
- **Portfolio Download** - Download portfolio as standalone HTML file
- **Copy Link to Share** - Shareable URL for your hosted portfolio
- **Google OAuth Login** - Sign in with Google in one click
- **Email OTP Verification** - Secure registration with email verification
- **Password Reset via OTP** - Forgot password flow with email OTP
- **Profile Management** - Update name, password, notification preferences
- **In-App Notifications** - Real-time notification badge for new templates
- **Feedback & Rating System** - Rate templates with 1–5 stars and comments

### For Admins

- **Analytics Dashboard** - Total users, portfolios, templates, average rating
- **Interactive Charts** - Template usage (pie), daily portfolios (line), daily registrations (bar)
- **Template Management** - Upload (ZIP/HTML), toggle active/inactive, delete
- **User Management** - Block/unblock users with reason + automatic email notification
- **Portfolio Overview** - View all portfolios across all users
- **Feedback Monitoring** - View all user ratings and messages
- **Hangfire Dashboard** - Monitor background job queue and status

---

## 13. Deployment Process

### 13.1 Local Development Setup

```bash
# Prerequisites
# - Visual Studio 2022 (with ASP.NET workload)
# - SQL Server Express (LocalDB or full instance)
# - .NET Framework 4.7.2 Developer Pack

# 1. Clone the repository
git clone https://github.com/Purvarajsinh39/Portify-Automated-Portfolio-Generator.git

# 2. Open Portify.sln in Visual Studio

# 3. Create the database
#    - Open SQL Server Management Studio (SSMS)
#    - Create a new database named "PortifyDB"
#    - Execute PortifyDB.sql to create all tables and constraints

# 4. Configure connection string
#    - Open AppSettings.config
#    - Set dev="true"
#    - Update DevConnectionString with your SQL Server instance name

# 5. Restore NuGet packages
#    - Right-click solution → Restore NuGet Packages

# 6. Run the application
#    - Press F5 or Ctrl+F5 in Visual Studio
#    - Application starts at http://localhost:PORT/
```

### 13.2 Production Deployment (Somee.com)

```
1. Database:
   - Create MSSQL database on Somee.com
   - Execute PortifyDB.sql via their SQL Manager
   - Note the connection string provided by Somee

2. Configuration:
   - Set dev="false" in AppSettings.config
   - Update ProdConnectionString with Somee connection string

3. Publish:
   - Right-click project → Publish
   - Choose FTP/Web Deploy
   - Enter Somee hosting credentials
   - Deploy

4. Verify:
   - Navigate to your Somee URL
   - Test login, portfolio creation, and deployment features
```

### 13.3 Environment Switching

The `AppSettings.config` controls environment selection:

```xml
<add key="dev" value="true" />   <!-- true = local SQL Express, false = Somee production -->
```

`PortifyDbContext.GetConnectionString()` reads this flag and selects the appropriate connection string at runtime.

---

## 14. Challenges and Solutions

| # | Challenge | Solution Implemented |
|---|---|---|
| 1 | **Sending emails blocks the user request** | Integrated **Hangfire** for fire-and-forget background email jobs. User gets immediate response while email sends asynchronously. |
| 2 | **Template HTML has different placeholder styles** | Created a comprehensive `RenderTemplate()` method supporting both `{{FieldName}}` and `{{Bio.FieldName}}` placeholder formats. |
| 3 | **Portfolio deletion leaves orphaned records** | Used `SqlTransaction` with explicit deletion of all related table records (7 child tables) before deleting the parent portfolio row. |
| 4 | **File name conflicts when saving portfolios** | Appended Portfolio ID to filename (`Title_ID.html`) ensuring unique filenames even if titles match. |
| 5 | **Google OAuth JWT validation** | Used `Google.Apis.Auth` NuGet package with server-side `GoogleJsonWebSignature.ValidateAsync()` and audience verification. |
| 6 | **OTP security (reuse and expiry)** | OTPs expire after 5 minutes. On resend, all previous unused OTPs for that email/purpose are marked as used. |
| 7 | **Hangfire dashboard exposed to public** | Created `HangfireAuthorizationFilter` implementing `IDashboardAuthorizationFilter` — only allows localhost and admin-session users. |
| 8 | **Environment-specific connection strings** | External `AppSettings.config` file with `dev` flag — `GetConnectionString()` dynamically selects dev or prod connection. |
| 9 | **Real-time preview without page reload** | AJAX `POST` to `/Portfolio/Preview` returns rendered HTML, injected into an iframe for instant live preview. |
| 10 | **AI API errors shouldn't break the app** | All Gemini API calls wrapped in try-catch with user-friendly error messages; chatbot gracefully degrades. |

---

## 15. Future Enhancements

| # | Enhancement | Description |
|---|---|---|
| 1 | **Password Hashing (BCrypt)** | Replace plain-text password storage with BCrypt hashing for production security |
| 2 | **Custom Domain Support** | Allow users to map custom domains to their portfolio URLs |
| 3 | **PDF Resume Export** | Generate downloadable PDF resumes alongside HTML portfolios |
| 4 | **Template Builder** | Drag-and-drop template designer for admins to create templates visually |
| 5 | **Multi-language Support** | Internationalization for Hindi, Gujarati, and other languages |
| 6 | **Portfolio Analytics** | View count, visitor location, and engagement metrics per portfolio |
| 7 | **Version History** | Allow users to save and revert portfolio versions |
| 8 | **Collaboration Mode** | Share portfolio editing access with mentors or teammates |
| 9 | **Dark Mode** | System-wide dark/light theme toggle |
| 10 | **Progressive Web App** | Service worker + manifest for offline access and mobile installation |
| 11 | **OAuth 2.0 + JWT Tokens** | Replace session-based auth with token-based authentication for API scalability |
| 12 | **Image Upload** | Allow profile picture and project screenshot uploads |
| 13 | **SEO Optimization** | Add meta tags, sitemap, and Open Graph tags to generated portfolios |
| 14 | **Real-time Collaboration** | SignalR-based real-time editing and notifications |
| 15 | **Unit Tests** | Comprehensive test suite for controllers, services, and data access |

---

## License

This project is developed as an academic project for educational purposes.

## Team

Developed by Vaghela Purvarajsinh as part of My academic curriculum using ASP.NET MVC and SQL Server.

## Support

For questions or issues, contact: **portify.support@gmail.com**, Developer: **vaghelapurvarajsinh8442@gmail.com**

---

<p align="center">
  <b>Built with ❤️ using ASP.NET MVC 5, SQL Server Management Studio, and Google Gemini AI</b>
</p>
