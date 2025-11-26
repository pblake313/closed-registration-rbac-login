<script lang="ts">
    import './DashboardBox.css'

    export let boxStyle: 'stockBox' | 'altBox' = 'stockBox'
</script>


<div class="dashboardBox {boxStyle}" >
    <slot></slot>
</div>
