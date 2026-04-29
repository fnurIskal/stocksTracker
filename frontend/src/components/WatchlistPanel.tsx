import type { Stock } from "../types/stock";
import { deleteStock, refreshStock } from "../api/stockApi";

interface Props {
  watchlist: Stock[];
  onSelect: (id: number) => void;
  onRefresh: () => void;
  onDelete: () => void;
}

function WatchlistPanel({ watchlist, onSelect, onRefresh, onDelete }: Props) {
  const handleRefresh = async (e: React.MouseEvent, id: number) => {
    e.stopPropagation();
    await refreshStock(id);
    onRefresh();
  };

  const handleDelete = async (e: React.MouseEvent, id: number) => {
    e.stopPropagation();
    await deleteStock(id);
    onDelete();
  };

  return (
    <div className="bg-gray-900 rounded-xl p-4">
      <h2 className="text-lg font-semibold text-green-400 mb-4">
        My Watchlist
      </h2>

      {watchlist.length === 0 ? (
        <p className="text-gray-500 text-sm">Henüz hisse eklenmedi.</p>
      ) : (
        <ul className="flex flex-col gap-2">
          {watchlist.map((stock) => (
            <li
              key={stock.id}
              onClick={() => onSelect(stock.id)}
              className="flex items-center justify-between bg-gray-800 hover:bg-gray-700 cursor-pointer px-3 py-2 rounded-lg"
            >
              <div>
                <p className="font-bold text-white">{stock.symbol}</p>
                <p className="text-xs text-gray-400">
                  ${stock.currentPrice.toFixed(2)}
                </p>
              </div>

              <div className="flex items-center gap-2">
                <span
                  className={`text-xs font-semibold ${
                    stock.changePercent >= 0 ? "text-green-400" : "text-red-400"
                  }`}
                >
                  {stock.changePercent >= 0 ? "+" : ""}
                  {stock.changePercent.toFixed(2)}%
                </span>

                <button
                  onClick={(e) => handleRefresh(e, stock.id)}
                  className="text-blue-400 hover:text-blue-300 text-lg"
                  title="Refresh"
                >
                  ↻
                </button>

                <button
                  onClick={(e) => handleDelete(e, stock.id)}
                  className="text-red-400 hover:text-red-300 text-lg"
                  title="Delete"
                >
                  ✕
                </button>
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default WatchlistPanel;
