import type { Stock, StockSummary } from "../types/stock";

interface Props {
  title: string;
  data?: StockSummary | null;
  stocks?: Stock[];
}

function AggregationCard({ title, data, stocks }: Props) {
  return (
    <div className="bg-gray-900 rounded-xl p-4">
      <h2 className="text-md font-semibold text-green-400 mb-3">{title}</h2>

      {/* Summary modu */}
      {data && (
        <div className="flex flex-col gap-2">
          <div className="bg-gray-800 rounded-lg p-3">
            <p className="text-gray-400 text-xs">Total Stocks</p>
            <p className="text-white font-bold">{data.totalStocks}</p>
          </div>
          <div className="bg-gray-800 rounded-lg p-3">
            <p className="text-gray-400 text-xs">Average Price</p>
            <p className="text-white font-bold">
              ${data.averagePrice.toFixed(2)}
            </p>
          </div>
          <div className="bg-gray-800 rounded-lg p-3">
            <p className="text-gray-400 text-xs">Total Value</p>
            <p className="text-white font-bold">
              ${data.totalValue.toFixed(2)}
            </p>
          </div>
          <div className="bg-gray-800 rounded-lg p-3">
            <p className="text-gray-400 text-xs">Best Performer</p>
            <p className="text-green-400 font-bold">
              {data.bestPerformer ?? "-"}
            </p>
          </div>
          <div className="bg-gray-800 rounded-lg p-3">
            <p className="text-gray-400 text-xs">Worst Performer</p>
            <p className="text-red-400 font-bold">
              {data.worstPerformer ?? "-"}
            </p>
          </div>
        </div>
      )}

      {/* Top Gainers modu */}
      {stocks && (
        <ul className="flex flex-col gap-2">
          {stocks.length === 0 ? (
            <p className="text-gray-500 text-sm">Veri yok.</p>
          ) : (
            stocks.map((stock) => (
              <li
                key={stock.id}
                className="bg-gray-800 rounded-lg p-3 flex justify-between"
              >
                <span className="text-white font-bold">{stock.symbol}</span>
                <span className="text-green-400 font-semibold">
                  +{stock.changePercent.toFixed(2)}%
                </span>
              </li>
            ))
          )}
        </ul>
      )}
    </div>
  );
}

export default AggregationCard;
