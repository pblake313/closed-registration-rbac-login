<script lang="ts">
	import './PhoneInput.css';
	import { createEventDispatcher } from 'svelte';

	export let label: string | null = null;
	export let value: string | null = null;
	export let id: string | null = null;
	export let placeholder: string | null = null;
	export let isInvalid: boolean = false;

	const dispatch = createEventDispatcher();

	function formatPhoneNumber(input: string): string {
		const digits = input.replace(/\D/g, '').slice(0, 10); // max 10 digits
		const parts = [];

		if (digits.length > 0) parts.push('(' + digits.slice(0, 3));
		if (digits.length >= 4) parts[0] += ')';
		if (digits.length >= 4) parts.push(' ' + digits.slice(3, 6));
		if (digits.length >= 7) parts.push('-' + digits.slice(6, 10));

		return parts.join('');
	}

    function handleTextInput(event: Event) {
        const target = event.target as HTMLInputElement;
        const formatted = formatPhoneNumber(target.value);
        target.value = formatted;

        dispatch('inputUpdated', formatted); // always dispatch formatted string, even if it's empty
    }

</script>


<div class="inputWrapper">
    <label for={id}>{label}</label>
    <div class="shadowWrapper">
        <input class:invalid={isInvalid} id={id} value={value} type="text" on:input={handleTextInput} placeholder={!placeholder ? label : placeholder} autocomplete="off">
    </div>
</div>