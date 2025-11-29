<script lang="ts">
    import type { User } from "$lib/types/User";
    import { formatDateTime, formatReadableDate } from "../../../utils/dateFormatter";
    import DashboardBox from "../../DashboardComponents/DashboardBox.svelte";
    import Button from "../../UI/Button.svelte";
    import EmailInput from "../../UI/EmailInput.svelte";
    import TextInput from "../../UI/TextInput.svelte";
    import Toggler from "../../UI/Toggler.svelte";

    import './EditUserForm.css'

    export let userToEdit: User | null = null
    let formTouched: boolean = false

    async function udpateUser() {
        try {

        } catch {

        } finally {

        }
    }
</script>

{#if !userToEdit}
    <p>No User</p>
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
                value={userToEdit.FirstName}
            />
            <br>
            <TextInput 
                label={'Last Name'} 
                onTextChange={(v) => {
                    userToEdit.LastName = v
                    formTouched = true
                }} 
                value={userToEdit.LastName}
            />
            <br>

            <Button fullWidth={true} buttonClass={'stayDark'} disabled={!formTouched}>Update</Button>

        
        </div>


        <form on:submit={udpateUser} class="editFormSide">

            <div>
                <h3 style="font-size: 20pt;">General</h3>
                <DashboardBox boxStyle={'altBox'}>
                    <div class="singlePermission">
                            <h4>Email</h4>
                        <p>{userToEdit.Email}</p>
                    </div>
                    <div style="height: 5px;"></div>
                    <div class="singlePermission">
                        <h4>Created At</h4>
                        <p>{formatDateTime(userToEdit.DateCreated)}</p>
                    </div>
                    <div style="height: 5px;"></div>
                    <div class="singlePermission">
                        <h4>Last Updated</h4>
                        <p>{formatDateTime(userToEdit.UpdatedAt)}</p>
                    </div>
                    <div style="height: 5px;"></div>
                    <div class="singlePermission">
                        <h4>Last Password Reset Email Sent At - Cleared After Login</h4>
                        <p>{formatDateTime(userToEdit.LastPasswordResetEmailSentAt)}</p>
                    </div>
                    <div style="height: 5px;"></div>
                    <div class="singlePermission">
                        <h4>Last Manual Login</h4>
                        <p>{formatDateTime(userToEdit.LastLogin)}</p>
                    </div>
                </DashboardBox>

                <div class="dangerZone">
                    <div></div>
                    <Button icon={'trash'} usePadding={false} buttonClass={'danger'}>Delete Account</Button>

                </div>
      
            </div>


        

        </form>




    </div>

{/if}