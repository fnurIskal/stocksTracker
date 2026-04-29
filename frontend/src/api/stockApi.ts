const BASE_URL = "https://localhost:7227/api/stock";

export const getAllStocks = async () => {
  const res = await fetch(`${BASE_URL}`);
  return res.json();
};

export const getStockById = async (id: number) => {
  const res = await fetch(`${BASE_URL}/${id}`);
  return res.json();
};

export const createStock = async (symbol: string) => {
  const res = await fetch(`${BASE_URL}`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ symbol }),
  });

  if (!res.ok) {
    const error = await res.text();
    throw new Error(error);
  }

  return res.json();
};

export const refreshStock = async (id: number) => {
  const res = await fetch(`${BASE_URL}/${id}/refresh`, { method: "PUT" });
  return res.json();
};

export const deleteStock = async (id: number) => {
  await fetch(`${BASE_URL}/${id}`, { method: "DELETE" });
};

export const getTopGainers = async (count = 5) => {
  const res = await fetch(`${BASE_URL}/analytics/top-gainers?count=${count}`);
  return res.json();
};

export const getSummary = async () => {
  const res = await fetch(`${BASE_URL}/analytics/summary`);
  return res.json();
};
