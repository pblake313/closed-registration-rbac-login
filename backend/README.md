# Backend

The backend is built with **.NET 10** and provides the full authentication, authorization, and RBAC logic for the application.  
It uses **SQL Server** for persistent storage and **Mailjet** for transactional emails (password resets, onboarding, etc.).

All protected endpoints enforce both authentication and permission checks.

---

## Seeding the Database

**IMPORTANT**  
Because this system uses closed registration, you must seed the database with *your* email address.  
This creates the first administrator account.

After running the seed script:

1. Go to the app’s **Forgot Password** page.
2. Enter the same email you used in the seed.
3. Reset your password.

This completes the initial account setup and gives you access to the application.

You can find the seed script in: /backend/migration.sql

## User Secrets

View the file /user-secrets.example.jsonc for an example of user-secrets required to run the application.

## Run Locally

```bash
  dotnet watch run
```


