<script lang="ts">
    import { onMount } from "svelte";
    import DashTop from "../../../../Components/DashboardComponents/DashTop.svelte";
    import { createAccountCrumbs } from "$lib/BreadCrumbData";
    import CreateUserForm from "../../../../Components/Forms/DashboadForms/CreateUserForm.svelte";
    import { setCrumbArray } from "../../../../stores/BreadCrumbStore";

    onMount(()=> {
        setCrumbArray(createAccountCrumbs)
    })

</script>


<DashTop dashHeading={'Create User'} useButton={false} dashButtonClicked={() => {}}></DashTop>

<br>

<CreateUserForm></CreateUserForm>