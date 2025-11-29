import { get } from "svelte/store";
import { logout } from "../stores/AuthStore";
import { pushNotification } from "../stores/DashboardStores/NotificationStore";
import { accessToken } from "../stores/TokenStore";

const backendLink = import.meta.env.VITE_BACKEND_URL;

export async function protectedFetch<T = any>(
    url: string,
    options: RequestInit = {}
): Promise<T> {
    const method = options.method || "GET";

    const token = get(accessToken);

    const res = await fetch(`${backendLink}${url}`, {
        method,
        headers: {
            "Content-Type": "application/json",
            ...(token ? { Authorization: `Bearer ${token}` } : {}),
            ...(options.headers || {})
        },
        credentials: "include",
        ...(method === "GET" ? {} : { body: options.body })
    });

    // 🔄 check for a new access token from middleware
    const newAccessToken = res.headers.get("x-new-access-token");
    if (newAccessToken) {
        accessToken.set(newAccessToken);
    }

    const text = await res.text();
    let data: any = null;

    try {
        data = text ? JSON.parse(text) : null;
    } catch {
        data = null;
    }

    if (!res.ok) {
        const err: any = new Error(
            data?.errorMessage ||
            data?.message ||
            `Request failed with status ${res.status}`
        );
        err.status = res.status;
        err.data = data;


        // 🔥 AUTO-LOGOUT HANDLING
        if (data?.forceLogout) {
            console.log("⛔ FORCE LOGOUT triggered");

            await logout();

            pushNotification("Logout Forced", data?.message || "You have been logged out. Please login and try again.", "neutral", 6, false);

            return Promise.reject(err);
        }

        throw err;
    }

    return data as T;
}
