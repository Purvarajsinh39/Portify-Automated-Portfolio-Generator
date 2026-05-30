# One-Click Portfolio Deployment Feature

This plan outlines the steps to implement the new feature where a user's portfolio is automatically deployed and hosted upon saving, allowing the user to copy a direct link to it.

## Goal Description

When the user enters their details and clicks "Save Portfolio", the system will not only save the data in the database but also generate a static `.html` file containing their complete portfolio. This file will be hosted at the application's root level (e.g., `/myportfolio.html`).

The user will be able to:
1. See "Download" and "Copy Link" options on their dashboard.
2. Click "Copy Link" to copy the live URL of their portfolio.
3. Access their hosted portfolio anytime.
4. Have the hosted file automatically deleted if they delete their portfolio from the dashboard.

## Open Questions

> [!IMPORTANT]
> Please review and clarify the following before we proceed:
> 
> 1. **Filename Clashes**: If two users create a portfolio with the same title (e.g., both use "My Portfolio"), the system will try to save both as `myportfolio.html` and one will overwrite the other. Do you want me to append a unique ID (like the Portfolio ID) to the filename to guarantee it's unique (e.g., `myportfolio_12.html`), or do you want strictly the exact title as requested?
> 2. **UI Location**: The current dashboard already has a "Download" button on the portfolio card. I will add the "Copy Link" button right next to it. Is this acceptable, or do you want a pop-up modal to appear *immediately* after saving the portfolio?
> 3. **Static File Storage location**: I plan to save these `.html` files in the root folder of the project so they can be accessed via `http://localhost:8000/myportfolio.html`. Are you okay with this, or would you prefer a specific public folder like `/PublicPortfolios/`?

## Proposed Changes

---

### Portfolio Controller

We need to generate and save the static HTML file at the time of portfolio creation.

#### [MODIFY] [PortfolioController.cs](file:///d:/Portify/Controllers/PortfolioController.cs)
- In the `Save()` method, after inserting the portfolio data into the database:
  - Fetch the selected template's HTML structure.
  - Call the `RenderTemplate()` method using the saved user data to generate the final HTML.
  - Determine the filename based on the portfolio title (e.g., `myportfolio.html`).
  - Use `System.IO.File.WriteAllText` to save this `.html` file directly to the server's root folder (`Server.MapPath("~/" + fileName)`).

---

### Dashboard Controller

We need to ensure that the static file is deleted when the user deletes their portfolio.

#### [MODIFY] [DashboardController.cs](file:///d:/Portify/Controllers/DashboardController.cs)
- In the `DeletePortfolio()` method:
  - Before deleting the database record, reconstruct the filename based on the portfolio's title.
  - Delete the corresponding `.html` file from the server's disk using `System.IO.File.Delete()`.

---

### User Dashboard View

We need to add the "Copy Link" functionality to the user's dashboard.

#### [MODIFY] [UserDashboard.cshtml](file:///d:/Portify/Views/User/UserDashboard.cshtml)
- In the Portfolio Cards loop:
  - Reconstruct the file name (e.g., `myportfolio.html`).
  - Add a "Copy Link" button next to the existing "Download" button.
  - Implement a small JavaScript function that copies the absolute URL (`window.location.origin + "/" + fileName`) to the user's clipboard and shows a toast notification or feedback (e.g., "Link copied!").

## Verification Plan

### Manual Verification
1. I will log in as a user and create a new portfolio.
2. I will verify that the `.html` file is generated in the root directory.
3. I will navigate to the dashboard, click "Copy Link", and open the copied URL in a new tab to ensure the portfolio displays correctly.
4. I will delete the portfolio and verify that the file is successfully removed from the disk.
