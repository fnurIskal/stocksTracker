import type { Stock } from "../types/stock";

interface Props {
  stock: Stock | null;
}

function StockDetail({ stock }: Props) {
  if (!stock) {
    return (
      <div className="bg-gray-900 rounded-xl p-6 h-full flex items-center justify-center">
        <p className="text-gray-500">
          Select a stock from the watchlist to see details.
        </p>
      </div>
    );
  }

  return (
    <div className="bg-gray-900 rounded-xl p-6">
      <div className="flex items-center justify-between mb-4">
        <div>
          <h2 className="text-2xl font-bold text-white">{stock.symbol}</h2>
          <p className="text-gray-400 text-sm">{stock.companyName}</p>
        </div>
        <span
          className={`text-lg font-semibold ${
            stock.changePercent >= 0 ? "text-green-400" : "text-red-400"
          }`}
        >
          {stock.changePercent >= 0 ? "+" : ""}
          {stock.changePercent.toFixed(2)}%
        </span>
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div className="bg-gray-800 rounded-lg p-4">
          <p className="text-gray-400 text-xs mb-1">Current Price</p>
          <p className="text-white text-xl font-bold">
            ${stock.currentPrice.toFixed(2)}
          </p>
        </div>

        <div className="bg-gray-800 rounded-lg p-4">
          <p className="text-gray-400 text-xs mb-1">Change</p>
          <p
            className={`text-xl font-bold ${stock.change >= 0 ? "text-green-400" : "text-red-400"}`}
          >
            {stock.change >= 0 ? "+" : ""}${stock.change.toFixed(2)}
          </p>
        </div>

        <div className="bg-gray-800 rounded-lg p-4">
          <p className="text-gray-400 text-xs mb-1">High</p>
          <p className="text-white text-xl font-bold">
            ${stock.highPrice.toFixed(2)}
          </p>
        </div>

        <div className="bg-gray-800 rounded-lg p-4">
          <p className="text-gray-400 text-xs mb-1">Low</p>
          <p className="text-white text-xl font-bold">
            ${stock.lowPrice.toFixed(2)}
          </p>
        </div>
      </div>

      <p className="text-gray-500 text-xs mt-4">
        Son güncelleme: {new Date(stock.lastUpdated).toLocaleString()}
      </p>
    </div>
  );
}

export default StockDetail;
