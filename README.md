# CafeSphere — Enterprise Cafe Management Platform

CafeSphere is an award-winning Enterprise Cafe Management SaaS user interface built with client-side **ASP.NET Core Blazor WebAssembly** and styled completely with custom **Vanilla CSS**.

The interface is handcrafted to follow high-end design aesthetics inspired by **Apple, Stripe, Linear, and Square POS**, implementing premium glassmorphism, depth elevations, blurred lighting backgrounds, custom SVG noise grain overlays, and micro-interactions.

---

## ✨ Features & Interactive Modules

The application integrates real-time **ASP.NET Core SignalR WebSockets** and fallback memory state brokers, syncing all actions instantly to a persistent MongoDB database:

*   **📊 Hub Dashboard (`Home.razor`)**: Displays key metrics (Today's Revenue, Active Tickets, Table Map status, safety stock warnings) and custom-rendered SVG hourly sales line charts, updating live via `DashboardHub`.
*   **🛒 Point of Sale (`Pos.razor`)**: Square-like category selector tabs, product search indexes, live cart item lists, tax/discount calculators, and checkout checkout success receipt animations, synced with `PosHub`.
*   **🧑‍🍳 Kitchen Display System (`Kitchen.razor`)**: Back-of-house ticket grid equipped with elapsed recipe cooking timers ticking in real time, custom coffee barista notes, and status bumping triggers, synced with `KitchenHub`.
*   **📅 Table Reservations (`Reservations.razor`)**: Interactive seating maps (12 tables with seating capacities from 2 to 6 covers) showing statuses (Available, Reserved, Seated) and guest booking dialog cards, synced live with `ReservationsHub`.
*   **🔔 Real-Time Floating Toasts (`MainLayout.razor`)**: Displays sliding glassmorphic popups on the viewport, playing synthetic chime sound notes natively via Web Audio API.
*   **📦 Raw Inventory (`Inventory.razor`)**: Stock level list featuring Safety Threshold limits, critical low-stock warnings, SKU codes, and inline restock buttons.
*   **👥 Staff Roster (`Employees.razor`)**: Attendance log displaying check-in times, weekly hours, roles, and attendance clock-in buttons.
*   **💰 Finance Ledger (`Finance.razor`)**: Double-entry ledger list displaying all cashflows, expenses logs, and payout margin details.
*   **📈 Graph Analytics (`Analytics.razor`)**: Product category performance bars and payment split share doughnut charts.
*   **⌨️ AI Command Drawer (`AiAssistant.razor`)**: Raycast Spotlight-style terminal triggered by pressing **Ctrl+K** or **Cmd+K** on the keyboard, allowing natural language navigation and queries (e.g. `/pos`, `whats our revenue?`, `warnings`).
*   **📱 Responsive Loading Transition**: Intercepts Blazor router location changes to display shimmering page skeleton loaders and slides side menus dynamically for mobile.

---

## 🛠️ Technology Stack

-   **Runtime**: .NET 10 WebAssembly Standalone
-   **Structure**: Blazor Razor Components
-   **Styling**: Custom CSS3 variables and resets (Zero Bootstrap, Tailwind, or Material libraries used)
-   **Icons**: Lucide Icons CDN
-   **Fonts**: Google Fonts (`Manrope`, `Inter`, `Plus Jakarta Sans`)

---

## 📂 Codebase Architecture

```bash
UI/
├── Components/
│   └── AiAssistant.razor       # Raycast Spotlight Command Drawer
├── Layout/
│   ├── MainLayout.razor        # Collapsible Sidebar & Glass Header Shell
│   └── NavMenu.razor           # Nav list containing Lucide links
├── Models/
│   └── CafeSphereModels.cs     # Enterprise Entities (Orders, Items, Staff)
├── Pages/
│   ├── Home.razor              # Dashboard View
│   ├── Pos.razor               # Cash Register interface
│   ├── Kitchen.razor           # KDS screen
│   ├── Reservations.razor      # Table Map Allocation
│   └── ...                     # Inventory, Employees, Finance, Settings
├── Services/
│   └── MockDataService.cs      # Core state broker & NLP AI query parser
└── wwwroot/
    ├── css/
    │   ├── variables.css       # HSL Color tokens, Radii, Shadows
    │   └── app.css             # Noise grain vectors, Scrollbars, Animations
    └── index.html              # Custom loading spinner & global script bindings
```

---

## 🚀 Getting Started

To launch and explore the CafeSphere interface locally:

1.  Clone this repository to your machine.
2.  Open your terminal inside the project directory:
    ```powershell
    cd UI
    ```
3.  Boot the ASP.NET Core developer environment:
    ```powershell
    dotnet run
    ```
4.  Open your browser and navigate to:
    [http://localhost:5247](http://localhost:5247)
5.  Try typing **Ctrl+K** or **Cmd+K** anywhere in the viewport to open the command search!
