export function formatReadableDate(dateStr: string | null | undefined): string {
    if (!dateStr) return "-";

    const d = new Date(dateStr);
    if (isNaN(d.getTime())) return "-";

    return d.toLocaleDateString("en-US", {
        month: "short",   // "Nov"
        day: "numeric",   // "18"
        year: "numeric"   // "2025"
    });
}