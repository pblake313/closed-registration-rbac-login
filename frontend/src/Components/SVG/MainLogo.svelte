<script lang="ts">
    import { onMount } from "svelte";

    export let height = '50px';
    
    let uniqueId = '';
    onMount(()=>{
      uniqueId = `logo-${Math.random().toString(36).substring(2, 9)}`;

    })
  </script>

<svg id="{uniqueId}" {height} data-name="Layer 2" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 303 298.64">
  <defs>
    <style>
      .lll1 {
        fill: #ff7b00;
        stroke: #ff7b00;
      }

      .lll1, .lcccc2 {
        stroke-miterlimit: 10;
      }

      .lcccc2 {
        stroke: #000;
      }
    </style>
  </defs>
  <g id="Layer_2-2" data-name="Layer 2">
    <g id="Layer_6" data-name="Layer 6">
      <path class="lll1" d="M68.5,263.65l1-50-36.44,26.44.2,58.04h234.63l-136.61-142.52c1.17-8.4,9.93-20.04,9.93-20.04l11.17-8.44c19.95-15.07,61.99-4.22,61.99-4.22-63.58-50.97-116.63-8.38-116.63-8.38l-35.1,26.53c3.01,3.99-2.52,23.21-12.09,30.44-9.57,7.23-26.79,2.7-26.79,2.7,0,0-8.78,6.63-14.36,10.85-5.58,4.22,1.67,18.79,1.67,18.79l9.04,11.97c9.04,11.97,21.03,7.92,21.03,7.92l15.96-12.06c-.27-18.6,11.12-23.44,11.12-23.44,18.35-13.87,36.98-9.89,36.98-9.89l81.31,84.31-118,1Z"/>
    </g>
    <g id="Layer_7" data-name="Layer 7">
      <polygon class="lcccc2" points="302.5 170.65 267.5 144.65 267.5 268.65 233.5 233.65 233.5 111.65 151.5 42.65 .5 172.65 .5 128.65 151.5 .65 302.5 127.65 302.5 170.65"/>
    </g>
  </g>
</svg>