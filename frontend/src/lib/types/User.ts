export interface User {
    UserId: number;
    Email: string | null;
    FirstName: string | null;
    LastName: string | null;
    DateCreated: string | null;
    UpdatedAt: string | null;
    LastPasswordResetEmailSentAt: string | null,
    LastLogin: string | null,
    LastAutoLogin: string | null,
    permissions: {
        JobPostings: boolean;
        AccountManagement: boolean;
        ViewCandidates: boolean;
    };
}
