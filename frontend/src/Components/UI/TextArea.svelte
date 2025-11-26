<script lang="ts">
	import { createEventDispatcher } from 'svelte';

	export let label: string | null = null;
	export let id = null;
	export let maxChar = 1000;
	export let placeholder = '';
	export let value: string | null = null;

	const dispatch = createEventDispatcher();

	function handleInput(event: Event) {
		const target = event.target as HTMLTextAreaElement;
		const newValue = target.value.trim() === '' ? null : target.value;
		dispatch('inputUpdated', newValue);
	}
</script>

<label for={id}>{label}</label>
<div class="shadowWrapper">
	<textarea
		name={id}
		id={id}
		placeholder={placeholder}
		maxlength={maxChar}
		on:input={handleInput}
		bind:value
	></textarea>
</div>
<style>
	.shadowWrapper {
		border-radius: 5px;
		width: 100%;
	}

	textarea {
		display: block;
		padding: 10px;
		outline: none;
		border: 1px solid lightgray;
		width: calc(100% - 22px);
		border-radius: 5px;
		color: rgb(111, 111, 111);
		height: calc(40px - 22px);
		line-height: 1.4;
		overflow: hidden;
		transition: 0.5s;
	}

	textarea:focus {
		border: 1px solid #FF7B00;
	}

	textarea::placeholder {
		color: rgb(174, 174, 174);
	}

	label {
		display: block;
		font-family: 'Plus Jakarta Sans', sans-serif;
		font-weight: 600;
		font-size: 11pt;
		margin-bottom: 8px;
	}

	textarea:not(:placeholder-shown) {
		height: 140px;
	}

	textarea:not(:placeholder-shown):focus {
		height: 140px;
	}
</style>
