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

export function isValidPassword(input: string | null): boolean {
	if (!input) return false;

	// Reject spaces outright
	if (/\s/.test(input)) return false;

	// Checks
	const hasCapital = /[A-Z]/.test(input);
	const hasNumber = /\d/.test(input);
	const longEnough = input.length >= 9;
	const hasSpecial = /[^A-Za-z0-9]/.test(input);

	return hasCapital && hasNumber && longEnough && hasSpecial;
}