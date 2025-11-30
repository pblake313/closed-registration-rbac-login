<script lang="ts">
    import type { User } from "$lib/types/User";
    import { formatDateTime } from "../../../utils/dateFormatter";
    import DashboardBox from "../../DashboardComponents/DashboardBox.svelte";
    import Button from "../../UI/Button.svelte";
    import TextInput from "../../UI/TextInput.svelte";
    import Toggler from "../../UI/Toggler.svelte";
    import './EditUserForm.css'
    import { protectedFetch } from "../../../fetchers/protectedFetch";
    import Loader from "../../UI/Loaders/Loader.svelte";
    import { removeUserFromAccountsArray, upsertUserIntoAllUserAccounts } from "../../../stores/DashboardStores/AllAccountsStore";
    import { pushNotification } from "../../../stores/DashboardStores/NotificationStore";
    import { goto } from "$app/navigation";
    import { authenticatedUser } from "../../../stores/UserStore";
    import { logout } from "../../../stores/AuthStore";

    export let userToEdit: User 
    let formTouched: boolean = false
    let isLoading: boolean = false
    let formSubmitted: boolean = false

    async function updateUser() {

        formSubmitted = true
        // a
        const userId = userToEdit?.UserId

        if (!userToEdit.FirstName || !userToEdit.LastName){
            return
        }

        const postObj = {
            FirstName: userToEdit.FirstName,
            LastName: userToEdit.LastName,
            accountManagement: userToEdit.permissions.AccountManagement,
            JobPostings: userToEdit.permissions.JobPostings,
            ViewCandidates: userToEdit.permissions.ViewCandidates,
        }

        try {
            isLoading = true

            const response = await protectedFetch(`/Admin/Edit-User/${userId}`, {
                method: "PATCH",
                body: JSON.stringify(postObj)
            });

            const updatedUser = response.updatedUser

            if (updatedUser){

                const upsertUser: User = {
                    UserId: updatedUser.userId,
                    Email: updatedUser.email,
                    FirstName: updatedUser.firstName,
                    LastName: updatedUser.lastName,
                    UpdatedAt: updatedUser.updatedAt,
                    DateCreated: updatedUser.dateCreated,
                    LastPasswordResetEmailSentAt: updatedUser.lastPasswordResetEmailSentAt,
                    LastLogin: updatedUser.lastLogin,
                    LastAutoLogin: updatedUser.lastAutoLogin,
                    PasswordAttempts: updatedUser.passwordAttempts,
                    AccountLockedUntil: updatedUser.accountLockedUntil,
                    permissions: {
                        JobPostings: updatedUser.jobPostings,
                        AccountManagement: updatedUser.accountManagement,
                        ViewCandidates: updatedUser.viewCandidates
                    }
                };

                upsertUserIntoAllUserAccounts(upsertUser)

                userToEdit = upsertUser

                formTouched = false
                pushNotification('Updated', 'User updated successfully!', 'success', 1, false)


                if (upsertUser.UserId === $authenticatedUser?.UserId){
                    logout()
                    pushNotification('Logout Forced', 'We have logged you out because your account was updated. Please login again.', 'neutral', 8, false)

                }
            }



        } catch (err: any) {

            pushNotification('Edit User Error', `${err?.data?.message || err.message || 'An unknown error has occurred.'}`, 'error', 6, false)

        } finally {
            isLoading = false
        }
    }

    async function deleteUser() {
        try {

            const userId = userToEdit?.UserId

            const response = await protectedFetch(`/Admin/Delete-User/${userId|| 'no-id'}`, {
                method: "DELETE"
            })

            console.log(response)
            removeUserFromAccountsArray(userId)
            pushNotification('User Removed', 'Users account was deleted successfully.', 'success', 3.5, false)
            goto('/dashboard/accounts')
        } catch (error: any) {
            console.log(error)
            pushNotification('Delete Error', error?.data?.message || error.message || 'An unknown error has occurred', 'error', 5, false)
        } finally {

        }
        
    }
</script>

{#if !userToEdit}
    <p>No User</p>
{:else}


    {#if isLoading}
        <Loader></Loader>
    {:else}
        
        <div class="splitForm">

            <div class="permSide">

                <h3 style="font-size: 20pt;">Edit User</h3>

                <DashboardBox boxStyle={'altBox'} >

                    <div class="singlePermission">
                        <div class="permissionToggleHead">
                            <h4>Account Management</h4>
                            <Toggler 
                                onChange={(v) => {
                                    userToEdit.permissions.AccountManagement = v
                                    formTouched = true
                                }} 
                                value={userToEdit.permissions.AccountManagement}
                            ></Toggler>
                        </div>
                        <p>User is allowed to create and manage user accounts, and adjust their permissions.</p>
                    </div>
                    <div style="height: 5px;"></div>
                    <div class="singlePermission">
                        <div class="permissionToggleHead">
                            <h4>Candidate Management</h4>
                            <Toggler 
                                onChange={(v) => {
                                    userToEdit.permissions.ViewCandidates = v
                                    formTouched = true
                                }} 
                                value={userToEdit.permissions.ViewCandidates}
                            ></Toggler>
                        </div>
                        <p>User is allowed to view and edit candidates and candidate information.</p>
                    </div>
                    <div style="height: 5px;"></div>
                    <div class="singlePermission">
                        <div class="permissionToggleHead">
                            <h4>Job Postings</h4>
                            <Toggler 
                                onChange={(v) => {
                                    userToEdit.permissions.JobPostings = v
                                    formTouched = true

                                }} 
                                value={userToEdit.permissions.JobPostings}
                            ></Toggler>
                        </div>
                        <p>User is allowed to create and manage job postings.</p>
                    </div>

                </DashboardBox>

                <br>


                <TextInput 
                    label={'First Name'} 
                    onTextChange={(v) => {
                        userToEdit.FirstName = v
                        formTouched = true
                    }} 
                    inputErrorText={!userToEdit.FirstName && formSubmitted ? "Please enter a valid first name." : null}
                    value={userToEdit.FirstName}
                />
                <br>
                <TextInput 
                    label={'Last Name'} 
                    onTextChange={(v) => {
                        userToEdit.LastName = v
                        formTouched = true
                    }} 
                    inputErrorText={!userToEdit.LastName && formSubmitted ? "Please enter a valid last name." : null}
                    value={userToEdit.LastName}
                />
                <br>

                <Button on:click={updateUser} fullWidth={true} buttonClass={'stayDark'} disabled={!formTouched}>Update</Button>

            
            </div>


            <div class="editFormSide">

                <div>
                    <h3 style="font-size: 20pt;">General</h3>
                    <DashboardBox boxStyle={'altBox'}>
                        <div class="genGrid">

                            <div class="singlePermission">
                                    <h4>Email</h4>
                                <p>{userToEdit.Email}</p>
                            </div>
                            <div class="singlePermission">
                                <h4>Created At</h4>
                                <p>{formatDateTime(userToEdit.DateCreated)}</p>
                            </div>
                            <div class="singlePermission">
                                <h4>Last Updated</h4>
                                <p>{formatDateTime(userToEdit.UpdatedAt)}</p>
                            </div>
                            <div class="singlePermission">
                                <h4>Last Auto Login</h4>
                                <p>{formatDateTime(userToEdit.LastAutoLogin)}</p>
                            </div>
                            <div class="singlePermission">
                                <h4>Last Manual Login</h4>
                                <p>{formatDateTime(userToEdit.LastLogin)}</p>
                            </div>
                            <div class="singlePermission">
                                <h4>Last Reset Link Sent</h4>
                                <p><em>Resets After Password Change:</em> {formatDateTime(userToEdit.LastPasswordResetEmailSentAt)}</p>
                            </div>
                            <div class="singlePermission">
                                <h4>Password Attempts</h4>
                                <p><em>Resets After Login:</em> {userToEdit.PasswordAttempts || 0}</p>
                            </div>
                            <div class="singlePermission">
                                <h4>Account Locked</h4>
                                <p><em>Resets After Login:</em> {formatDateTime(userToEdit.AccountLockedUntil)}</p>
                            </div>


                        </div>
                   
                    </DashboardBox>

                    <div class="dangerZone">
                        <div></div>
                        <Button on:click={deleteUser} icon={'trash'} usePadding={false} buttonClass={'danger'}>Delete Account</Button>

                    </div>
        
                </div>


            

            </div>




        </div>
    {/if}

{/if}