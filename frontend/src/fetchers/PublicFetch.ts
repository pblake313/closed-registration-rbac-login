// src/lib/publicFetch.ts
const backendLink = import.meta.env.VITE_BACKEND_URL;

export async function publicFetch<T = any>(
  url: string,
  options: RequestInit = {}
): Promise<T> {
  const method = options.method || 'GET';

  const res = await fetch(`${backendLink}${url}`, {
    method,
    headers: {
      'Content-Type': 'application/json',
      ...(options.headers || {}),
    },
    credentials: 'include',
    ...(method === 'GET' ? {} : { body: options.body }),
  });

  // Read the body once
  const text = await res.text();
  let data: any = null;

  if (text) {
    try {
      data = JSON.parse(text);
    } catch {
      data = text; // not JSON, just raw text
    }
  }

  // 🔥 Throw on non-2xx responses
  if (!res.ok) {
    const message =
      (data && typeof data === 'object' && 'message' in data && data.message) ||
      `Request failed with status ${res.status}`;

    const error: any = new Error(message);
    error.status = res.status;
    error.data = data;
    throw error;
  }

  // If 204 / no content
  if (!text) {
    return {} as T;
  }

  return data as T;
}
