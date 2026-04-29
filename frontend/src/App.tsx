import { useState, useEffect } from "react";
import type { Stock, StockSummary } from "./types/stock";
import {
  getAllStocks,
  getSummary,
  getTopGainers,
  getStockById,
} from "./api/stockApi";
import SearchBar from "./components/SearchBar";
import StockDetail from "./components/StockDetail";
import WatchlistPanel from "./components/WatchlistPanel";
import AggregationCard from "./components/AggregationCard";

function App() {
  const [watchlist, setWatchlist] = useState<Stock[]>([]);
  const [selectedStock, setSelectedStock] = useState<Stock | null>(null);
  const [summary, setSummary] = useState<StockSummary | null>(null);
  const [topGainers, setTopGainers] = useState<Stock[]>([]);

  const loadWatchlist = async () => {
    const data = await getAllStocks();
    setWatchlist(data);
  };

  const loadAnalytics = async () => {
    const summaryData = await getSummary();
    const gainersData = await getTopGainers(3);
    setSummary(summaryData);
    setTopGainers(gainersData);
  };

  useEffect(() => {
    const init = async () => {
      await loadWatchlist();
      await loadAnalytics();
    };
    init();
  }, []);

  const handleStockAdded = async (stock: Stock) => {
    setSelectedStock(stock);
    await loadWatchlist();
    await loadAnalytics();
  };

  const handleSelectStock = async (id: number) => {
    const data = await getStockById(id);
    setSelectedStock(data);
  };
  const handleRefreshOrDelete = async () => {
    await loadWatchlist();
    await loadAnalytics();
  };

  return (
    <div className="min-h-screen bg-gray-950 text-white p-6">
      <h1 className="text-2xl font-bold mb-6 text-green-400">
        Stock Watchlist
      </h1>

      <div className="flex justify-center mt-4 mb-6 w-full max-w-2xl mx-auto">
        <SearchBar onStockAdded={handleStockAdded} />
      </div>

      <div className="flex gap-4 mt-6">
        {/* Sol — Aggregation */}
        <div className="flex flex-col gap-4 w-64">
          <AggregationCard title="Summary" data={summary} />
          <AggregationCard title="Top Gainers" stocks={topGainers} />
        </div>

        {/* Orta — Detay */}
        <div className="flex-1">
          <StockDetail stock={selectedStock} />
        </div>

        {/* Sağ — Watchlist */}
        <div className="w-72">
          <WatchlistPanel
            watchlist={watchlist}
            onSelect={handleSelectStock}
            onRefresh={handleRefreshOrDelete}
            onDelete={handleRefreshOrDelete}
          />
        </div>
      </div>
    </div>
  );
}

export default App;
