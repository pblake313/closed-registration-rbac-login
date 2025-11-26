<script lang="ts">
    import { fade } from 'svelte/transition';


    import './FormError.css'

    export let errorTitle: string = 'Enter Error Title'
    export let errorMessage: string = 'Enter Error Message'
    export let centerAlign: boolean = false

</script>

<div in:fade={{duration: 500}} out:fade={{duration: 300}} class:centerText={centerAlign}>
    <h4 class="errorTitle">{errorTitle}</h4>
    <p class="errorMessage">{errorMessage}</p>
</div>