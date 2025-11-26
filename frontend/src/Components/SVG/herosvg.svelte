<script lang="ts">
  export let uniqueId; // This uniqueId should be passed to each component instance

  // Helper function to make IDs unique
  const makeIdUnique = (id: any) => `${id}-${uniqueId}`;

  import { onMount } from 'svelte';

  onMount(() => {
    const svg = document.querySelector(`svg[data-unique="${uniqueId}"]`);
    if (!svg) return;

    svg.querySelectorAll('[id]').forEach(el => {
      const originalId = el.id;
      const newId = makeIdUnique(originalId);
      el.id = newId;

      // Update references in 'fill', 'clip-path', 'stroke', etc.
      ['fill', 'clip-path', 'stroke', 'style'].forEach(attr => {
        svg.querySelectorAll(`[${attr}]`).forEach(refEl => {
          const attrVal = refEl.getAttribute(attr);
          if (attrVal && attrVal.includes(`url(#${originalId})`)) {
            refEl.setAttribute(attr, attrVal.replace(`url(#${originalId})`, `url(#${newId})`));
          }
        });
      });
    });
  });
</script>


<svg id="Layer_2" data-name="Layer 2" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 1343.3 736.83" data-unique="{uniqueId}">
    <defs>
      <style>
          .cls-1 {stroke: #4d4d4d; fill: none; stroke-miterlimit: 10; opacity: .41;}
          .cls-2 {stroke-width: 0px;}
          .cls-8 { fill: #1b1b1b; stroke: #000; }
          .cls-3, .cls-4, .cls-5, .cls-6, .cls-7 {fill: none; stroke-miterlimit: 10; opacity: .41;}
          .cls-3 {stroke: #ccc;}
          .cls-4 {stroke: #e6e6e6;}
          .cls-5 {stroke: #333;}
          .cls-9 {clip-path: url('#{makeIdUnique('clippath')}');}
          .cls-6 {stroke: #999;}
          .cls-7 {stroke: #f2f2f2;}
      </style>
      <clipPath id="{makeIdUnique('clippath')}">
          <rect class="cls-2" x="303.23" y="-303.23" width="736.83" height="1343.3" transform="translate(303.23 1040.07) rotate(-90)"/>
      </clipPath>
      <radialGradient id="{makeIdUnique('radial-gradient')}" cx="670.99" cy="336.56" fx="1716.66" fy="1035.29" r="1257.64" gradientTransform="translate(.19 29.07)" gradientUnits="userSpaceOnUse">
          <stop offset=".04" stop-color="#1a1a1a"/>
          <stop offset=".59" stop-color="#1a1a1a"/>
      </radialGradient>
  </defs>
    <g id="Layer_1-2" data-name="Layer 1">
      <g class="cls-9">
        <rect class="cls-8" x="-.32" y="-2.66" width="1348.57" height="742.74"/>
        <line class="cls-1" x1="573.36" y1="815.64" x2="1198.12" y2="454.94"/>
        <line class="cls-1" x1="1149.36" y1="228.64" x2="1774.12" y2="-132.06"/>
        <line class="cls-5" x1="92.36" y1="952.64" x2="717.12" y2="591.94"/>
        <line class="cls-1" x1="838.36" y1="548.64" x2="1463.12" y2="187.94"/>
        <line class="cls-1" x1="262.66" y1="739.85" x2="938.34" y2="349.74"/>
        <line class="cls-1" x1="-109.21" y1="672.76" x2="553.93" y2="289.89"/>
        <line class="cls-1" x1="-30.61" y1="768.32" x2="764.98" y2="308.99"/>
        <line class="cls-1" x1="1058.36" y1="646.64" x2="1683.12" y2="285.94"/>
        <line class="cls-1" x1="1247.36" y1="629.64" x2="1872.12" y2="268.94"/>
        <line class="cls-1" x1="1058.36" y1="92.64" x2="1683.12" y2="-268.06"/>
        <line class="cls-1" x1="937.12" y1="208.64" x2="312.36" y2="-152.06"/>
        <line class="cls-1" x1="938.12" y1="350.64" x2="313.36" y2="-10.06"/>
        <line class="cls-1" x1="1683.12" y1="1006.64" x2="1058.36" y2="645.94"/>
        <line class="cls-1" x1="1793.12" y1="944.64" x2="1168.36" y2="583.94"/>
        <line class="cls-1" x1="1059.16" y1="421.4" x2="936.91" y2="350.1"/>
        <line class="cls-1" x1="839.06" y1="548.62" x2="716.57" y2="478.47"/>
        <line class="cls-1" x1="568.06" y1="564.62" x2="445.57" y2="494.47"/>
        <line class="cls-1" x1="642.06" y1="378.62" x2="519.57" y2="308.47"/>
        <line class="cls-1" x1="1059.16" y1="279.4" x2="937.3" y2="209.05"/>
        <line class="cls-1" x1="838.16" y1="662.4" x2="716.3" y2="592.05"/>
        <line class="cls-1" x1="1271.16" y1="298.4" x2="1149.3" y2="228.05"/>
        <line class="cls-1" x1="1058.56" y1="92.81" x2="1058.56" y2="736.45"/>
        <line class="cls-1" x1="838.56" y1="547.81" x2="838.56" y2="1191.45"/>
        <line class="cls-1" x1="763.56" y1="-6.19" x2="763.56" y2="108.71"/>
        <line class="cls-1" x1="1270.56" y1="-120.19" x2="1270.51" y2="615.91"/>
        <line class="cls-1" x1="1149.56" y1="-415.19" x2="1149.56" y2="228.45"/>
        <line class="cls-1" x1="937.56" y1="-293.19" x2="937.56" y2="350.45"/>
        <line class="cls-3" x1="869.71" y1="530.36" x2="1058.53" y2="420.76"/>
        <line class="cls-6" x1="1149.71" y1="228.36" x2="1338.53" y2="118.76"/>
        <line class="cls-3" x1="471.07" y1="619.26" x2="690.44" y2="492.61"/>
        <line class="cls-3" x1="1058.71" y1="646.36" x2="1247.53" y2="536.76"/>
        <line class="cls-3" x1="1058.71" y1="421.62" x2="1058.71" y2="264.91"/>
        <line class="cls-7" x1="1270.71" y1="368.62" x2="1270.71" y2="10.62"/>
        <line class="cls-4" x1="937.71" y1="247.62" x2="937.71" y2="-110.38"/>
      </g>
    </g>
  </svg>