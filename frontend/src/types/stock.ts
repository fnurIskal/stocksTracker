export interface Stock {
  id: number;
  symbol: string;
  companyName: string;
  currentPrice: number;
  change: number;
  changePercent: number;
  highPrice: number;
  lowPrice: number;
  lastUpdated: string;
}
export interface StockSummary {
  totalStocks: number;
  averagePrice: number;
  totalValue: number;
  bestPerformer: string;
  worstPerformer: string;
}
