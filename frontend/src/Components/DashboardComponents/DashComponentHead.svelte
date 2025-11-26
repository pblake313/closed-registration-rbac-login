<script lang="ts">
    export let headingText: string = 'enter `headingText`'
</script>

<style>
    .holdCompHead {
        border-bottom: 1px solid rgba(211, 211, 211, 0.514);
    }
    .holdCompHead h4 {
        margin: 0px;
        padding: 2px 0px 6px 0px;
        opacity: .9;
    }
</style>

<div class="holdCompHead">
    <h4>{headingText}</h4>

</div>