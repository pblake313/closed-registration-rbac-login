<script>
	import Footer from '../Components/Footer.svelte';
    import Navigation from '../Components/Navigation.svelte';
    import Notifications from '../Components/Notifications.svelte';
    import { authenticatedUser } from '../stores/UserStore';

	import './styles.css';


    $: console.log('AUTHENTICATED USER: ', $authenticatedUser)

</script>


<Notifications></Notifications>
<slot />