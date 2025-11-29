export function toOneLetter(str: string | null | undefined): string {
    if (!str) return "";
    return str.trim()[0]?.toUpperCase() ?? "";
}