import { get } from "svelte/store";
import { accessToken as accessTokenStore } from "../stores/TokenStore";

const backendLink = import.meta.env.VITE_BACKEND_URL;

export async function accountFetch<T = any>(
    url: string,
    options: RequestInit = {}
): Promise<T> {
    const method = options.method || "GET";

    // get the current token from the store
    const token = get(accessTokenStore);

    const res = await fetch(`${backendLink}${url}`, {
        method,
        headers: {
            "Content-Type": "application/json",
            ...(token ? { Authorization: `Bearer ${token}` } : {}),
            ...(options.headers || {})
        },
        credentials: "include", // sends refresh token cookie automatically
        ...(method === "GET" ? {} : { body: options.body })
    });

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
        throw err;
    }

    return data as T;
}
