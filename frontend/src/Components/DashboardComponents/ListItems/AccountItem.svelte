<script lang="ts">
    import type { User } from "$lib/types/User";
    import { formatReadableDate } from "../../../utils/dateFormatter";
    import './AccountItem.css'

    export let user: User
</script>

<a href="/dashboard/accounts/{user.UserId}" class="userAccountListItem">
    <div class="col">
        <p>{user.FirstName}</p>
    </div>
    <div class="col">
        <p>{user.LastName}</p>
    </div>
    <div class="col">
        <p>{user.Email}</p>
    </div>
    <div class="col">
        <p>
            {[
                user.permissions.AccountManagement && "Account Management",
                user.permissions.JobPostings && "Job Postings",
                user.permissions.ViewCandidates && "View Candidates"
            ].filter(Boolean).join(", ") || "-"}
        </p>
    </div>
    <div class="col rev">
        <p>{formatReadableDate(user.DateCreated)}</p>
    </div>
</a>