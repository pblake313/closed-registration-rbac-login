<script lang="ts">
    import './TextInput.css'
    import { createEventDispatcher } from 'svelte';

    export let label: string | null = null;
    export let value: string | null = null;
    export let id: string | null = null;
    export let placeholder: string | null = null;
    export let isInvalid: boolean = false;

    const dispatch = createEventDispatcher();

    function handleTextInput(event: Event) {
        const target = event.target as HTMLInputElement;
        const newValue = target.value.trim() === '' ? null : target.value;
        dispatch('inputUpdated', newValue);
    }
</script>


<div class="inputWrapper">
    <label for={id}>{label}</label>
    <div class="shadowWrapper">
        <input class:invalid={isInvalid} id={id} value={value} type="text" on:input={handleTextInput} placeholder={!placeholder ? label : placeholder} autocomplete="off">
    </div>
</div>