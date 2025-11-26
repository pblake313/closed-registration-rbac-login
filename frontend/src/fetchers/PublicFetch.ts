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
    credentials: "include",
    // ✅ Don’t stringify again — just pass the body as is
    ...(method === 'GET' ? {} : { body: options.body }),
  });

  return res.json();
}