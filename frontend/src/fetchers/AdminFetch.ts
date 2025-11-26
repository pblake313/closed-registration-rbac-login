// src/lib/publicFetch.ts
const backendLink = import.meta.env.VITE_BACKEND_URL;

export async function adminFetch<T = any>(
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
    credentials: "include",
    ...(method === 'GET' ? {} : { body: options.body }),
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
      data?.errorMessage || data?.message || `Request failed with status ${res.status}`
    );
    err.status = res.status;
    err.data = data;
    throw err;
  }

  return data as T;
}
