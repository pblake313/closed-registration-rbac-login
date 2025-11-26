<script lang="ts">
    import { onMount } from "svelte";
    import SubHero from "../../Components/PageComponents/SubHero/SubHero.svelte";


    onMount(async ()=> {
        try {
            await getTestData()
        } catch (error){
            console.log(error)
        }


    })

    async function getTestData() {
        try {
            const response = await fetch('http://localhost:5110/test/finna', {
                method: "GET"
            })
            const responseData = await response.json();

            console.log(responseData)
        } catch (err) {
            console.log(err)

        }
     
    }

</script>


<div style="margin: 350px 0px;">
    
    <p style="text-align: center;">Test Page works!</p>
</div>