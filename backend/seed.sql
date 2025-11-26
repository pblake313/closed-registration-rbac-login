USE SaConstruction;
GO

-- =============================
-- Declare variables
-- =============================
DECLARE 
    @Email NVARCHAR(255)        = 'myemail@mydomain.com',
    @FirstName NVARCHAR(100)    = 'John',
    @LastName NVARCHAR(100)     = 'Smith',

    -- Permissions --- leave at 1 so you have all permissions
    @JobPostings BIT            = 1,
    @AccountManagement BIT      = 1,
    @ViewCandidates BIT         = 1,
    @UserId INT;

-- =============================
-- Insert Admin User
-- =============================
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

-- =============================
-- Insert Permissions
-- =============================
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
