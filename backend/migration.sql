-- 1) Create DB if it doesn't exist
IF DB_ID('SaConstruction') IS NULL
BEGIN
    CREATE DATABASE SaConstruction;
END
GO

USE SaConstruction;
GO

-- 2) Schema + tables
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Users')
BEGIN
    EXEC('CREATE SCHEMA Users');
END
GO

IF OBJECT_ID('Users.AccountData', 'U') IS NULL
BEGIN
    CREATE TABLE Users.AccountData (
        UserId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Email NVARCHAR(255) NOT NULL UNIQUE,
        FirstName NVARCHAR(100) NULL,
        LastName NVARCHAR(100) NULL,
        PasswordHash NVARCHAR(255) NULL,
        AccountLockedUntil DATETIME2 NULL,
        PasswordAttempts INT NULL,
        DateCreated DATETIME2 NOT NULL CONSTRAINT DF_AccountData_DateCreated DEFAULT SYSUTCDATETIME(),
        UpdatedAt  DATETIME2 NOT NULL CONSTRAINT DF_AccountData_UpdatedAt  DEFAULT SYSUTCDATETIME(),
        LastPasswordResetEmailSentAt DATETIME2 NULL,
        LastLogin DATETIME2 NULL,
        LastAutoLogin DATETIME2 NULL,
    );
END
GO

IF OBJECT_ID('Users.UserPermissions', 'U') IS NULL
BEGIN
    CREATE TABLE Users.UserPermissions (
        UserId INT NOT NULL PRIMARY KEY,
        JobPostings       BIT NOT NULL CONSTRAINT DF_UserPermissions_JobPostings       DEFAULT 0,
        AccountManagement BIT NOT NULL CONSTRAINT DF_UserPermissions_AccountManagement DEFAULT 0,
        ViewCandidates    BIT NOT NULL CONSTRAINT DF_UserPermissions_ViewCandidates    DEFAULT 0,
        CONSTRAINT FK_UserPermissions_AccountData_UserId
            FOREIGN KEY (UserId)
            REFERENCES Users.AccountData(UserId)
            ON DELETE CASCADE
    );
END
GO

-- Password reset docs table
IF OBJECT_ID('Users.PasswordResetDocs', 'U') IS NULL
BEGIN
    CREATE TABLE Users.PasswordResetDocs (
        -- Who the reset is for
        UserId INT NOT NULL,

        -- When the reset was requested
        DateCreated DATETIME2 NOT NULL
            CONSTRAINT DF_PasswordResetDocs_DateCreated DEFAULT SYSUTCDATETIME(),

        -- When the reset link expires (set from code)
        ExpiresAt DATETIME2 NOT NULL,

        -- Token used in the reset link
        ReferenceId NVARCHAR(100) NOT NULL,

        CONSTRAINT PK_PasswordResetDocs PRIMARY KEY (ReferenceId),

        CONSTRAINT FK_PasswordResetDocs_AccountData_UserId
            FOREIGN KEY (UserId)
            REFERENCES Users.AccountData(UserId)
            ON DELETE CASCADE
    );

    CREATE NONCLUSTERED INDEX IX_PasswordResetDocs_UserId
        ON Users.PasswordResetDocs (UserId);
END
GO

-- 3) Seed admin user (single batch, no mid-GO)
DECLARE 
    @Email NVARCHAR(255)        = 'myemail@mydomain.com',
    @FirstName NVARCHAR(100)    = 'John',
    @LastName NVARCHAR(100)     = 'Smith',
    @JobPostings BIT            = 1,
    @AccountManagement BIT      = 1,
    @ViewCandidates BIT         = 1,
    @UserId INT;

IF NOT EXISTS (
    SELECT 1 FROM Users.AccountData WHERE Email = @Email
)
BEGIN
    INSERT INTO Users.AccountData (
        Email,
        FirstName,
        LastName,
        DateCreated,
        UpdatedAt
    )
    VALUES (
        @Email,
        @FirstName,
        @LastName,
        SYSUTCDATETIME(),
        SYSUTCDATETIME()
    );

    PRINT '✓ Inserted initial admin user.';
END
ELSE
BEGIN
    PRINT 'Admin user already exists. Skipping insert.';
END

SELECT @UserId = UserId
FROM Users.AccountData
WHERE Email = @Email;

IF NOT EXISTS (
    SELECT 1 FROM Users.UserPermissions WHERE UserId = @UserId
)
BEGIN
    INSERT INTO Users.UserPermissions (
        UserId,
        JobPostings,
        AccountManagement,
        ViewCandidates
    )
    VALUES (
        @UserId,
        @JobPostings,
        @AccountManagement,
        @ViewCandidates
    );

    PRINT '✓ Inserted admin permissions.';
END
ELSE
BEGIN
    PRINT 'Admin permissions already exist. Skipping insert.';
END;
GO
