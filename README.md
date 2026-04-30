# Stock Watchlist Tracker

A stock watchlist app built with .NET 8 and React. Users can search for a stock symbol from the Finnhub API and add it to their watchlist. Data is stored in SQL Server via Entity Framework Core.

## Tech Stack

- Backend: .NET 8 Web API
- Database: SQL Server
- External API: Finnhub
- Frontend: React + TypeScript + Vite
- Containerization: Docker + Docker Compose
- Testing: xUnit

## Design Patterns Used

- **Repository Pattern** — IStockRepository / StockRepository
  Data access logic is fully abstracted behind an interface. The controller never touches the DbContext directly, which makes the code testable and the data layer swappable.

## Decisions & Trade-offs

**Why Finnhub?**
Finnhub offers a free tier with real-time stock quotes and a simple REST API. No credit card required, which makes it easy for anyone cloning the project to get started quickly.

**What I would add with more time**
Auto-refreshing prices via a background service, pagination on the watchlist endpoint, and user authentication so each user can maintain their own watchlist.

## Setup & Run

1. Clone the repository
2. Open `docker-compose.yml` and replace `YOUR_API_KEY` with your Finnhub API key (https://finnhub.io)
3. Run: `docker-compose up --build`
4. Open your browser:
   - Frontend: http://localhost:5173
   - Backend Swagger: http://localhost:7227/swagger

> Database is created and migrations are applied automatically on first run.
