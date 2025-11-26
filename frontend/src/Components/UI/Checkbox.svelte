<script lang="ts">
	import './Checkbox.css';
	import { createEventDispatcher } from 'svelte';

	export let label: string = 'Checkbox Label';
	export let checkboxId: string = '';
	export let value: boolean = false; // true = checked, false = unchecked

	const dispatch = createEventDispatcher();

	function handleChange(event: Event) {
		const target = event.target as HTMLInputElement;
		dispatch('radioChanged', target.checked);
	}
</script>

<input
	class="checkboxIn"
    
	type="checkbox"
	id={checkboxId}
	checked={value}
	on:change={handleChange}
/>

<label class="checkLabe" for={checkboxId} class:showBorder={value}>
	<div class="labelFlex">
		<div class="radioSide">
			<div class="radioOuter">
				<div class="radioInner" class:isChecked={value}></div>
			</div>
			<h4>{label}</h4>
		</div>
	</div>
</label>
