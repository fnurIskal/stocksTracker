import { useState } from "react";
import { createStock } from "../api/stockApi";
import type { Stock } from "../types/stock";

interface Props {
  onStockAdded: (stock: Stock) => void;
}

function SearchBar({ onStockAdded }: Props) {
  const [symbol, setSymbol] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [submitted, setSubmitted] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!symbol.trim() || submitted) return;

    setSubmitted(true);
    setLoading(true);
    setError(null);

    try {
      const newStock = await createStock(symbol.toUpperCase());
      setSymbol("");
      onStockAdded(newStock);
    } catch (err: unknown) {
      const message =
        err instanceof Error ? err.message : "Stock could not be added.";
      setError(message);
    } finally {
      setLoading(false);
      setSubmitted(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="flex gap-3 items-center w-full">
      <div className="relative flex-1 max-w-lg">
        <span className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400 text-sm">
          🔍
        </span>
        <input
          type="text"
          placeholder="Search for a stock symbol (e.g. AAPL, TSLA, GOOGL)"
          value={symbol}
          onChange={(e) => setSymbol(e.target.value)}
          className="w-full bg-gray-800 text-white pl-9 pr-4 py-3 rounded-xl focus:outline-none focus:ring-2 focus:ring-green-400 placeholder-gray-500"
        />
      </div>

      <button
        type="submit"
        disabled={loading}
        className="bg-green-500 hover:bg-green-600 text-white px-6 py-3 rounded-xl font-semibold disabled:opacity-50 transition-colors"
      >
        {loading ? "Adding..." : "Add to Watchlist"}
      </button>

      {error && <p className="text-red-400 text-sm">{error}</p>}
    </form>
  );
}

export default SearchBar;
