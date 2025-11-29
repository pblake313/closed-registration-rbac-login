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

export function formatDateTime(dateStr: string | null | undefined): string {
    if (!dateStr) return "-";

    const d = new Date(dateStr);
    if (isNaN(d.getTime())) return "-";

    const date = d.toLocaleDateString("en-US", {
        month: "short",
        day: "numeric",
        year: "numeric",
    });

    const time = d.toLocaleTimeString("en-US", {
        hour: "numeric",
        minute: "2-digit",
        hour12: true,
    });

    return `${date} · ${time}`;
}
