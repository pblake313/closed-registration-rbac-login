export interface User {
    userId?: string,
    Email: string | null,
    FirstName: string | null,
    LastName: string | null,
    DateCreated: string | null,
    UpdatedAt: string | null,
    permissions: {
        JobPostings: boolean,
        AccountManagement: boolean,
        ViewCandidates: boolean
    }
}