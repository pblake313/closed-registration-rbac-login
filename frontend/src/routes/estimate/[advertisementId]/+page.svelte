<script lang="ts">
    import { onMount } from "svelte";
    import { navStyle } from "../../../stores/NavStore";

    onMount(()=> {
        navStyle.set({class: 'bright'})
    })
</script>

