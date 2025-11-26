export function isValidPhoneNumber(input: string | null): boolean {
	if (!input) return false;
	const digits = input.replace(/\D/g, '');
	return /^\d{10}$/.test(digits);
}

export function isValidEmail(input: string | null): boolean {
	if (!input) return false;
	const trimmed = input.trim();
	const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
	return emailRegex.test(trimmed);
}