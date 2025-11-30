// Force everything into America/New_York (handles EST/EDT automatically)
const EASTERN_TZ = "America/New_York";

// Convert SQL timestamps (which don't include timezone info) into REAL UTC
function parseUtcDate(dateStr: string | null | undefined): Date | null {
    if (!dateStr) return null;

    const trimmed = dateStr.trim();
    if (!trimmed) return null;

    // Detect if the string already has timezone info like Z or +05:00
    const hasZone = /[zZ]|[+\-]\d\d:?\d\d$/.test(trimmed);

    // SQL returns values like: 2025-11-30T00:22:51.1970435
    // These *are* UTC but have no Z suffix — so we treat them as UTC manually
    const iso = hasZone ? trimmed : trimmed + "Z";

    const d = new Date(iso);
    if (isNaN(d.getTime())) return null;

    return d;
}

export function formatReadableDate(dateStr: string | null | undefined): string {
    const d = parseUtcDate(dateStr);
    if (!d) return "-";

    return d.toLocaleDateString("en-US", {
        month: "short",
        day: "numeric",
        year: "numeric",
        timeZone: EASTERN_TZ
    });
}

export function formatDateTime(dateStr: string | null | undefined): string {
    const d = parseUtcDate(dateStr);
    if (!d) return "-";

    const date = d.toLocaleDateString("en-US", {
        month: "short",
        day: "numeric",
        year: "numeric",
        timeZone: EASTERN_TZ
    });

    const time = d.toLocaleTimeString("en-US", {
        hour: "numeric",
        minute: "2-digit",
        hour12: true,
        timeZone: EASTERN_TZ
    });

    return `${date} · ${time}`;
}
