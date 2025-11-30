<script lang="ts">
    import type { User } from "$lib/types/User";
    import { formatDateTime, formatReadableDate } from "../../../utils/dateFormatter";
    import './AccountItem.css'

    export let user: User
</script>

<a href="/dashboard/accounts/{user.UserId}" class="userAccountListItem">
    <div class="col">
        <p>{user.FirstName} {user.LastName}</p>
    </div>

    <div class="col">
        <p>{user.Email}</p>
    </div>
    <div class="col">
        <p>
            {[
                user.permissions.AccountManagement && "Accounts",
                user.permissions.JobPostings && "Jobs",
                user.permissions.ViewCandidates && "Candidates"
            ].filter(Boolean).join(", ") || "-"}
        </p>
    </div>
    <div class="col">
        <p>{formatDateTime(user.LastLogin)}</p>
    </div>
    <div class="col rev">
        <p>{formatDateTime(user.LastAutoLogin)}</p>
    </div>
</a>