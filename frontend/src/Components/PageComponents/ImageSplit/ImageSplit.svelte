<script lang="ts">
    export let imageUrl: string = '/Images/Roofing/roofers.jpg'
</script>

<div class="imageSplit">
	<div class="imageWrap">
		<img src="{imageUrl}" alt="construction management">
	</div>
</div>


<style>
    .imageSplit {
        width: 90%;
        max-width: 1600px;
        margin: auto;
        padding: 100px 0px;
    }
    .imageWrap {
        height: 50vh;
        overflow: hidden;
        max-height: 400px;
    }
    .imageWrap img {
        width: 100%; /* Ensure the image spans the full width of the container */
        height: 100%; /* Ensure the image spans the full height of the container */
        object-fit: cover; /* Cover the container without distorting the image */
    }

    @media (max-width: 1165px){
    .imageSplit{
        width: 90%;
    }
    @media (max-width: 750px){
        .imageWrap {
            height: 50vh;
            max-height: 450px;
        }
        .imageSplit {
            padding: 50px 0px;

        }
    @media (max-width: 550px){
        .imageWrap {
            height: 65vh;
            max-height: 550px;
        }
    }
}
}

</style> 