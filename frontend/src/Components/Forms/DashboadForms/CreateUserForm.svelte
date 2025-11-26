<script lang="ts">
    import { goto } from "$app/navigation";
    import type { User } from "$lib/types/User";
    import { adminFetch } from "../../../fetchers/AdminFetch";
    import { upsertUserIntoAllUserAccounts } from "../../../stores/DashboardStores/AllAccountsStore";
    import { pushNotification } from "../../../stores/DashboardStores/NotificationStore";
    import { isValidEmail } from "../../../utils/validators";
    import DashboardBox from "../../DashboardComponents/DashboardBox.svelte";
    import DashComponentHead from "../../DashboardComponents/DashComponentHead.svelte";
    import Button from "../../UI/Button.svelte";
    import EmailInput from "../../UI/EmailInput.svelte";
    import FormError from "../../UI/FormError.svelte";
    import Loader from "../../UI/Loaders/Loader.svelte";
    import TextInput from "../../UI/TextInput.svelte";
    import Toggler from "../../UI/Toggler.svelte";
    import './CreateUserForm.css';

    // permissions
    let viewCandidates: boolean = true;
    let accountManagement: boolean = false;
    let JobPostings: boolean = true;

    // user info
    let firstName: string | null = null
    let lastName: string | null = null
    let email: string | null = null


    let formSubmitted: boolean = false
    let isLoading = false

    // clear the error message if something changes.

    let errorMessage : string | null = null

    let shaking = false; // 🔥 control prop for button

    async function createUser(event: SubmitEvent) {
        event.preventDefault(); // don't reload page

        formSubmitted = true
        if (isLoading){
            // means just sent form.. dont send dupes.
            return
        }

        if (!firstName || !lastName || !isValidEmail(email) ){
            shake();
            return
        }

        const postObject = {
            ViewCandidates: viewCandidates,
            AccountManagement: accountManagement,
            JobPostings,
            FirstName: firstName,
            LastName: lastName,
            Email: email
        }

        try {
            isLoading = true
            console.log('hittting here...')

            const response = await adminFetch('/Admin/Create-User', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(postObject)
            });

            console.log(response)

           let userObj: User = {
                userId: response.userId,
                Email: response.email,
                FirstName: response.firstName,
                LastName: response.lastName,
                UpdatedAt: response.updatedAt,
                DateCreated: response.dateCreated,
                permissions: {
                    JobPostings: response.jobPostings,
                    AccountManagement: response.accountManagement,
                    ViewCandidates: response.viewCandidates
                }
            }
            upsertUserIntoAllUserAccounts(userObj)
            console.log('push notification here...')
            pushNotification('Success!', 'The account was created successfully.', 'success', 1.5, false)
            goto('/dashboard/accounts')

        } catch (err: any) {
            errorMessage = err.errorMessage || err.message || 'An unknown error has occurred.'

        } finally {
            isLoading = false
        }

    }

    function shake() {
        shaking = false;
        // force a new tick so animation restarts even if already true
        requestAnimationFrame(() => {
            shaking = true;
            setTimeout(() => {
                shaking = false;
            }, 500); // match CSS animation duration
        });
    }
</script>

{#if isLoading}
    <Loader></Loader>
{:else}
    <form on:submit={createUser}>
        <div class="wrapCreateUserFormFlex">
            <!-- user details -->
            <div class="userInfoSide">
                <div style="margin-top: 2px;">
                    <div>
                        <div class="doubleFlex">
                            <TextInput value={firstName} onTextChange={(v) => {
                                firstName = v
                                errorMessage = null
                            }} inputErrorText={formSubmitted && !firstName ? "Missing first name." : null}  label={'First name'}></TextInput>
                            <TextInput value={lastName}  onTextChange={(v) => {
                                lastName = v,
                                errorMessage = null
                            }} inputErrorText={formSubmitted && !lastName ? "Missing last name." : null} label={'Last Name'}></TextInput>
                        </div>
    
                        <EmailInput value={email} inputErrorText={formSubmitted && !isValidEmail(email) ? "Enter a valid email address." : null} onEmailChange={(v) => {
                            email = v
                            errorMessage = null
                        }} label={'Email'}></EmailInput>

                        <div style="height: 20px;"></div>
                        <Button buttonClass={'stayDark'} {shaking}>
                            Create User
                        </Button> 

                        {#if errorMessage}
                            <br><br>
                            <FormError errorTitle={'Create User Error'} errorMessage={errorMessage}></FormError>
                        {/if}
                    </div>
                </div>
            </div>

            <!-- user permissions -->
            <div class="userPermissionsSide">
                <DashComponentHead headingText={'User Permissions'}></DashComponentHead>
                <div style="height: 8px;"></div>
                <DashboardBox boxStyle={"altBox"}>
                    <div class="singlePermission" style="margin-bottom: 5px;">
                        <div class="permissionToggleHead">
                            <h4>Account Management</h4>
                            <Toggler value={accountManagement} onChange={(v) => { 
                                accountManagement = v 
                                errorMessage = null

                            }} />
                        </div>
                        <p>User is allowed to create and manage user accounts, and adjust their permissions.</p>
                    </div>

                    <div class="singlePermission" style="margin-bottom: 5px;">
                        <div class="permissionToggleHead">
                            <h4>Candidate Management</h4>
                            <Toggler value={viewCandidates} onChange={(v) => { 
                                viewCandidates = v 
                                errorMessage = null

                            }} />
                        </div>
                        <p>User is allowed to view and edit candidates and candidate information.</p>
                    </div>

                    <div class="singlePermission">
                        <div class="permissionToggleHead">
                            <h4>Job Postings</h4>
                            <Toggler value={JobPostings} onChange={(v) => { 
                                JobPostings = v 
                                errorMessage = null
                            }} />
                        </div>
                        <p>User is allowed to create and manage job postings.</p>
                    </div>
                </DashboardBox>
            </div>
        </div>

        <br>
    </form>
{/if}
