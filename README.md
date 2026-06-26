# NOMAD
### Intelligent Travel Command Platform

> **NOMAD** is a full-stack web application built for the modern traveller — combining real-time weather intelligence, AI-powered route planning, cultural survival guides, expense tracking, emergency response tools, and offline data storage into a single unified tactical dashboard.

---

## Table of Contents

- [Project Overview](#project-overview)
- [Key Features](#key-features)
- [Screenshots](#screenshots)
- [System Architecture](#system-architecture)
- [Application Flow](#application-flow)
- [Technology Stack](#technology-stack)
- [Database Schema](#database-schema)
- [Setup & Installation](#setup--installation)
- [Configuration](#configuration)
- [Project Structure](#project-structure)

---

## Project Overview

### Objectives

NOMAD aims to replace the fragmented ecosystem of travel apps with a single intelligent platform. Whether you are navigating an unfamiliar city, tracking a travel budget in a foreign currency, or preparing for a medical emergency abroad, NOMAD provides the tools you need — even when you are offline.

- Provide a centralised **command dashboard** for all travel data
- Deliver **real-time weather intelligence** with meteorological alerts and 7-day forecasts
- Enable **AI-powered route planning** with safety heatmaps, secure extraction paths, and checkpoint tracking
- Offer a **cultural survival guide** tailored to your destination's etiquette, customs, dress codes, and scam alerts
- Supply a **universal translator** with emergency phrases and text-to-speech output
- Track **travel expenses** by category with budget forecasting and spending charts
- Provide an **emergency SOS override system** with live threat intelligence and emergency contact management
- Store critical information in an **offline-accessible vault** for use when internet connectivity is lost
- Manage a full **explorer profile** with travel history, preferences, and credential settings

---

## Key Features

### 🛡️ User Authentication
Secure account management powered by ASP.NET Core Identity with role-based access control.

- Secure registration with validated email, password policy enforcement (min. 6 chars, digit required), and unique email constraint
- Cookie-based login with a 7-day sliding expiration window
- Forgot password flow with token-based reset
- Authenticated route guards — all core pages require login; unauthenticated users are redirected to `/login`
- Session-aware cascading authentication state throughout the Blazor component tree

---

### 🌐 Command Centre Dashboard
The application's home screen aggregates live data across all NOMAD modules into a single intelligence briefing.

- **Stat cards** displaying key travel metrics at a glance
- **Live weather widget** showing current conditions for the active destination
- **Exchange rate panel** with currency shift indicators for the destination's local currency
- **AI recommendations** tailored to current weather, location, and safety conditions
- **Spending summary** previewing current trip expenditure against budget
- **Danger alert feed** surfacing active regional threats for the selected destination
- **Nearby safe havens** listing embassies, hospitals, and secure facilities
- Destination selector supporting multiple pre-loaded city/country profiles (Istanbul, Tokyo, Paris, Islamabad, and more)

---

### 🌦️ Weather Intelligence
Full meteorological module with city search and structured atmospheric analysis.

- Live weather data lookup by city name with real-time async fetch
- Displays: current temperature, feels-like index, humidity, wind speed, UV index, sunrise/sunset times, and overall travel condition rating
- **7-day forecast** showing daily high/low temperatures, precipitation probability, and condition icons
- **Active meteorological alerts** (storm warnings, flash flood advisories, etc.) highlighted with severity badges
- **AI travel recommendations** generated from current weather conditions (e.g., pack waterproofs, avoid coastal zones)
- Dynamic weather background theming based on current conditions (clear, cloudy, stormy)

---

### 🗺️ AI Route Intelligence & Live Maps
A tactical mapping module for navigation, route planning, and real-time situational awareness.

- **GPS coordinate display** with live latitude/longitude uplink
- **Intelligence layer toggles** — activate or deactivate Nearby Safe Havens, Secure Extraction Routes, and Danger Heatmaps independently
- **AI journey planner** — input a destination to receive an AI-generated travel plan including:
  - Recommended transport mode and estimated journey time
  - Estimated cost in local currency
  - Point-of-interest recommendations
  - Ordered route checkpoints with safety ratings
  - Local danger zones overlaid on the map
- **Route checkpoint tracker** displaying checkpoint name, status, and safety rating
- **Danger zone heatmap** rendered via JavaScript interop for dynamic map overlays
- **Saved routes** persisted to the database for future reference
- Leaflet.js map integration via `mapInterop.js` for interactive rendering

---

### 💱 Financial Hub
Comprehensive travel expense management with analytics and currency awareness.

- **Total expenditure tracker** with a visual budget progress bar against a configurable budget ceiling
- **Expense log** showing each transaction with title, category, amount, date, and destination currency
- **Category breakdown** — expenses grouped into Hotels, Transport, Food, Shopping, Entertainment, and Emergencies
- **Daily spending pattern chart** rendered via Chart.js interop (`chartInterop.js`)
- **Add expense form** for manual entry with category selector and currency field
- Currency contextually set to the active destination's local currency on dashboard load
- Spending data seeded per destination to reflect realistic travel cost profiles

---

### 🌍 Cultural Survival Guide
Destination-specific cultural and social intelligence for respectful and safe travel.

- Select from multiple destination sectors (Turkey, Japan, France, Pakistan)
- **Regional protocol card** covering local greeting customs, dress code requirements, and tipping culture
- **Dos and Don'ts list** — practical behavioural guidance specific to each destination
- **Scam alert panel** — known tourist scams and confidence trick patterns for the selected region
- **Emergency information** — local emergency numbers and critical contacts
- **Emergency phrase reference** — key phrases in the local language with pronunciation guides
- Visual country indicator with a contextual hero image per region

---

### 🔤 Universal Translator
Real-time linguistic decryption tool with speech output capability.

- Translates user-entered English text into the local language of the active destination
- Language is automatically synchronised to the user's `ActiveDestination` and `ActiveCountry` profile fields
- **Emergency phrase quick-access panel** — one-tap access to pre-translated critical phrases (Help, Police, Hospital, etc.)
- **Text-to-speech output** via `speechInterop.js` — translated text can be spoken aloud using the Web Speech API
- Support for Turkish, Japanese, French, and Urdu language targets
- Translation engine handles common travel vocabulary: greetings, gratitude, price inquiries, directions, and emergency language

---

### 🚨 Emergency SOS Override System
A dedicated safety module for high-threat situations.

- **Live threat intelligence feed** — displays active political, environmental, and security danger alerts for the current region with severity classifications (Low, Medium, High, Critical)
- **SOS Override button** — a single prominent trigger that activates the emergency broadcast sequence, displaying a countdown timer and simulating alert dispatch
- **Emergency contact management** — add, view, and manage trusted contacts with name, phone number, and relationship; contacts are persisted per user in the database
- **Nearby safety locations** — lists embassies, hospitals, and police stations with addresses, phone numbers, and distance estimates
- **Extraction route** — displays a pre-computed safe route out of the current region
- Alert levels are colour-coded: green (safe), amber (caution), red (critical)

---

### 📋 Offline Intel Vault
An encrypted local cache for critical travel data accessible without internet connectivity.

- **Vault card system** — create custom information cards saved to local browser storage via `storageInterop.js`
- Card types include: emergency contacts, booking references, safety phrases, local notes, and custom entries
- **Vault status indicator** showing encrypted/secure state
- Pre-populated cards per destination covering offline essentials (embassy address, emergency numbers, critical phrases)
- **CRUD operations** — inject new card entries, view existing cards, and delete stale records
- Data persists across browser sessions using the browser's local storage sandbox
- Works fully off-grid once loaded — no active connection required to access saved intel

---

### 👤 Explorer Profile
Full user identity and preference management centre.

- **Profile card** displaying full name, username, email, nationality, preferred language, and preferred currency
- **Avatar URL** field for custom profile image
- **Bio field** for personal travel description
- **Active destination settings** — set and update the current destination city and country, which propagates across all modules (weather, translator, map, finance)
- **Visited countries log** — serialised list of destinations recorded to the user's travel history
- **Travel preferences panel** — configure travel style (Budget / Mid-range / Luxury), preferred activities, dietary restrictions, accessibility needs, and notification opt-ins (weather alerts, safety alerts, news alerts)
- **Password change** with current password verification
- **Emergency contacts** — manage up to multiple trusted contacts with full CRUD
- **Travel history** — paginated log of past trips with destination, dates, and total expenses recorded
- Secure credential update with validation feedback and success/error state messaging

---

### 🔔 Real-Time Notification System
A persistent background notification engine that broadcasts contextual alerts throughout the session.

- `NotificationService` runs as a singleton background service using a timer-based simulation
- Broadcasts weather updates, commerce telemetry, and security alerts at random intervals
- **Toast notification component** (`NotificationToast.razor`) renders alerts as animated overlays in the main layout
- Notification types: Info, Warning, Danger, Success — each with distinct icon and colour treatment
- Dismissible toasts with auto-expiry; notification history accessible from the sidebar

---

## Screenshots

> _Screenshots will be added here. Suggested views to capture:_

| Screen | Description |
|---|---|
| Login | Authentication screens |
<img width="1907" height="915" alt="Screenshot 2026-06-26 140107" src="https://github.com/user-attachments/assets/10c6d49b-c2b8-4e2e-baf0-82ddab38f1af" />

| Register |
<img width="1890" height="912" alt="Screenshot 2026-06-26 140139" src="https://github.com/user-attachments/assets/ca383679-52fc-40aa-8d78-346187595c8f" />

| Command Centre Dashboard | Main hub with stats, weather, and alerts |
<img width="1911" height="895" alt="Screenshot 2026-06-26 140323" src="https://github.com/user-attachments/assets/d1649b77-2f0a-461b-b90e-3134095c5f44" />
<img width="1493" height="911" alt="Screenshot 2026-06-26 140648" src="https://github.com/user-attachments/assets/97d2c00b-e802-487f-9417-e18590503bb9" />
<img width="1358" height="792" alt="Screenshot 2026-06-26 140702" src="https://github.com/user-attachments/assets/ba2640a4-897d-4163-89cf-5dd0df570221" />
<img width="1393" height="875" alt="Screenshot 2026-06-26 140723" src="https://github.com/user-attachments/assets/30e5ede9-734c-4681-8d47-a055884bc51e" />
<img width="1535" height="348" alt="Screenshot 2026-06-26 140732" src="https://github.com/user-attachments/assets/d2c32281-88af-478a-bf73-131702edcc59" />

| Weather Intelligence | Full meteorological module |
<img width="1918" height="905" alt="Screenshot 2026-06-26 140806" src="https://github.com/user-attachments/assets/765cf0f6-1bd3-40d8-a8b6-d8c771860373" />
<img width="1877" height="886" alt="Screenshot 2026-06-26 140817" src="https://github.com/user-attachments/assets/0242e4e6-6be6-4c59-849c-763bbf95aa0a" />

| AI Route Map | Live map with heatmap and route layers |
<img width="1892" height="917" alt="Screenshot 2026-06-26 140934" src="https://github.com/user-attachments/assets/103b7133-4c0d-493a-99f0-a4f68f030b09" />

| Financial Hub | Expense tracker with chart |
<img width="1901" height="912" alt="Screenshot 2026-06-26 141013" src="https://github.com/user-attachments/assets/498f8444-de40-4d01-a7b4-0ff3a21c085d" />
<img width="1875" height="902" alt="Screenshot 2026-06-26 141034" src="https://github.com/user-attachments/assets/e4283e45-cd78-422e-8262-dda7326aef2f" />
<img width="1531" height="527" alt="Screenshot 2026-06-26 141050" src="https://github.com/user-attachments/assets/e8514b14-4a83-4cd6-b90f-eaec683a2f98" />

| Cultural Survival Guide | Regional etiquette and scam alerts |
<img width="1902" height="905" alt="Screenshot 2026-06-26 141523" src="https://github.com/user-attachments/assets/8498efeb-5ccb-458e-bea1-2f4a24bc0521" />

| Universal Translator | Translation engine with phrase panel |
<img width="1907" height="900" alt="Screenshot 2026-06-26 141604" src="https://github.com/user-attachments/assets/38b82067-3fe4-4636-a77b-115ec14f28d1" />

| Emergency SOS | Threat feed and SOS override button |
<img width="1896" height="915" alt="Screenshot 2026-06-26 141637" src="https://github.com/user-attachments/assets/a5b144be-0c8e-4f7e-adfb-a358c89217f0" />

| Offline Intel Vault | Vault card grid |
<img width="1907" height="893" alt="Screenshot 2026-06-26 141701" src="https://github.com/user-attachments/assets/5d9c84fd-ce06-4ebf-a21d-e1bb107da8ee" />

| Explorer Profile | User identity and preferences |
<img width="1918" height="902" alt="Screenshot 2026-06-26 141736" src="https://github.com/user-attachments/assets/f1349636-52c8-4e4d-9d77-78074653f111" />


---

## System Architecture

### Overview

NOMAD is a **Blazor Server** application built on **ASP.NET Core 8**, using an **Interactive Server render mode** throughout. All pages communicate with services through standard .NET dependency injection. The UI state lives on the server and streams updates to the client over a persistent SignalR connection.

```
┌─────────────────────────────────────────────────────────────┐
│                      Browser (Client)                        │
│         Blazor WebAssembly Interop + SignalR Channel         │
└────────────────────────┬────────────────────────────────────┘
                         │
┌────────────────────────▼────────────────────────────────────┐
│                  ASP.NET Core 8 Server                       │
│                                                              │
│  ┌──────────────┐  ┌───────────────┐  ┌──────────────────┐  │
│  │  Razor Pages │  │  Middleware   │  │  Authentication  │  │
│  │  (Blazor)    │  │  Pipeline     │  │  (Identity +     │  │
│  │              │  │               │  │   Cookie Auth)   │  │
│  └──────┬───────┘  └───────────────┘  └──────────────────┘  │
│         │                                                     │
│  ┌──────▼──────────────────────────────────────────────┐     │
│  │                   Service Layer                      │     │
│  │  DashboardService │ WeatherService │ MapService      │     │
│  │  FinanceService   │ CultureService │ SafetyService   │     │
│  │  NotificationService (Singleton)                     │     │
│  └──────┬───────────────────────────────────────────────┘     │
│         │                                                     │
│  ┌──────▼──────────────────────────────────────────────┐     │
│  │             Data Access Layer (EF Core)              │     │
│  │         ApplicationDbContext (IdentityDbContext)     │     │
│  └──────┬───────────────────────────────────────────────┘     │
│         │                                                     │
└─────────┼───────────────────────────────────────────────────┘
          │
┌─────────▼──────────┐
│   SQLite Database  │
│    (nomad.db)      │
└────────────────────┘
```

### Core Services

| Service | Lifetime | Responsibility |
|---|---|---|
| `DashboardService` | Scoped | Central data resolver — maps destination city/country to full `DestinationMetadata` including weather, dangers, safety locations, exchange rates, offline cards, and emergency phrases |
| `WeatherService` | Scoped | Generates weather data models per destination including 7-day forecasts, active alerts, UV index, and AI travel recommendations |
| `MapService` | Scoped | Produces GPS coordinates, nearby POIs, AI journey plans with checkpoints, route coordinates, and danger zone data per destination |
| `FinanceService` | Scoped | Manages in-session expense records, seeds destination-appropriate starting expenses, and computes category summaries and spending patterns |
| `CultureService` | Scoped | Provides emergency phrase lists and text translation for the active destination's language |
| `SafetyService` | Scoped | Handles safety data retrieval (currently a lightweight façade over DashboardService metadata) |
| `NotificationService` | Singleton | Background alert broadcaster using a timer; fires contextual travel notifications across weather, security, and finance domains |

### JavaScript Interop Modules

| File | Purpose |
|---|---|
| `mapInterop.js` | Initialises and controls the Leaflet.js map — renders markers, routes, heatmap overlays, and layer toggles |
| `chartInterop.js` | Renders Chart.js bar and line charts for the Financial Hub spending visualisations |
| `speechInterop.js` | Interfaces with the Web Speech API for text-to-speech output in the Translator |
| `storageInterop.js` | Reads and writes to browser `localStorage` for the Offline Intel Vault |

### UI Component Structure

```
App.razor
├── Routes.razor
│   ├── AuthLayout.razor          ← Unauthenticated wrapper
│   │   ├── LoginScreen           (/login)
│   │   ├── RegisterScreen        (/register)
│   │   └── ForgotPasswordScreen  (/forgot-password)
│   └── MainLayout.razor          ← Authenticated wrapper with sidebar nav
│       ├── Home.razor            (/dashboard)   — Command Centre
│       ├── WeatherPage.razor     (/weather)     — Atmospheric Intel
│       ├── Map.razor             (/map)         — AI Route Intelligence
│       ├── Finance.razor         (/finance)     — Financial Hub
│       ├── Culture.razor         (/culture)     — Cultural Survival Guide
│       ├── Translator.razor      (/translate)   — Universal Translator
│       ├── Emergency.razor       (/emergency)   — SOS Override System
│       ├── OfflineCards.razor    (/offline)     — Offline Intel Vault
│       ├── Profile.razor         (/profile)     — Explorer Profile
│       └── Logout.razor          (/logout)
└── Shared/
    └── NotificationToast.razor   — Global toast notification overlay
```

---

## Application Flow

### Authentication Flow

```
App Start
    │
    ▼
RedirectToLogin.razor checks AuthenticationState
    │
    ├── Not Authenticated ──► LoginScreen (/login)
    │                              │
    │                         [Enter credentials]
    │                              │
    │                    ┌─────────┴──────────┐
    │                    │                    │
    │               Login success        Login fail
    │                    │                    │
    │                    ▼                    ▼
    │           Redirect to /dashboard   Show error
    │                                        │
    │                              [Forgot password?]
    │                                        │
    │                              ForgotPassword.razor
    │                              Token-based reset flow
    │
    └── Authenticated ──► MainLayout.razor (/dashboard)
                               │
                          [All pages behind
                           @attribute [Authorize]]
```

### Main Application Flow

```
MainLayout.razor (Sidebar Navigation)
    │
    ├──► /dashboard    Command Centre
    │         │
    │         ├── DashboardService.Resolve(city, country)
    │         ├── WeatherService.GetLiveWeatherAsync()
    │         ├── FinanceService.SeedForDestination()
    │         └── Renders: stats, weather, exchange rates,
    │                      AI tips, danger alerts, nearby havens
    │
    ├──► /weather      Weather Intelligence
    │         │
    │         ├── WeatherService.GetLiveWeatherAsync(city)
    │         └── Renders: current conditions, 7-day forecast,
    │                      alerts, AI recommendations
    │
    ├──► /map          AI Route Intelligence
    │         │
    │         ├── MapService.GetCurrentLocation()
    │         ├── MapService.GetNearbyPlaces()
    │         ├── MapService.PlanAIJourney(destination)
    │         ├── JS Interop: mapInterop.js (Leaflet)
    │         └── Renders: GPS HUD, layer toggles,
    │                      AI journey plan, checkpoint list
    │
    ├──► /finance      Financial Hub
    │         │
    │         ├── FinanceService.GetExpenses()
    │         ├── FinanceService.GetSpendingByCategory()
    │         ├── JS Interop: chartInterop.js (Chart.js)
    │         └── Renders: total spend, budget bar,
    │                      expense log, category chart
    │
    ├──► /culture      Cultural Survival Guide
    │         │
    │         ├── DashboardService.Resolve(country)
    │         └── Renders: greeting, dress code, tipping,
    │                      dos/don'ts, scam alerts
    │
    ├──► /translate    Universal Translator
    │         │
    │         ├── CultureService.MockTranslate(text, city, country)
    │         ├── CultureService.GetEmergencyPhrases()
    │         ├── JS Interop: speechInterop.js (Web Speech API)
    │         └── Renders: translation engine, phrase panel,
    │                      TTS output button
    │
    ├──► /emergency    Emergency SOS Override
    │         │
    │         ├── DashboardService.Resolve() → DangerAlerts
    │         ├── UserManager → EmergencyContacts
    │         └── Renders: threat feed, SOS button,
    │                      emergency contacts, extraction route
    │
    ├──► /offline      Offline Intel Vault
    │         │
    │         ├── JS Interop: storageInterop.js (localStorage)
    │         └── Renders: vault cards grid, add/delete forms
    │
    └──► /profile      Explorer Profile
              │
              ├── UserManager.GetUserAsync()
              ├── UserManager.UpdateAsync()
              ├── DbContext → TravelPreferences, TravelHistory
              └── Renders: identity card, preferences,
                           travel history, emergency contacts
```

### Notification Flow

```
NotificationService (Singleton)
    │
    ├── Timer fires every ~45 seconds
    ├── Selects random alert from pool
    ├── Adds ToastNotification to internal list
    └── Fires OnNotify event
              │
              ▼
    NotificationToast.razor (in MainLayout)
    ├── Subscribes to OnNotify
    ├── Re-renders toast overlay
    └── Auto-dismisses after timeout
```

---

## Technology Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 — Blazor Server (Interactive Server Mode) |
| Language | C# 12 / .NET 8 |
| UI | Blazor Razor Components + Bootstrap 5 + custom `nomad.css` |
| Authentication | ASP.NET Core Identity (cookie-based, 7-day sliding expiration) |
| ORM | Entity Framework Core 8 |
| Database | SQLite (`nomad.db`) |
| Mapping | Leaflet.js (via JS Interop) |
| Charts | Chart.js (via JS Interop) |
| Speech | Web Speech API (via JS Interop) |
| Offline Storage | Browser `localStorage` (via JS Interop) |
| Icons | Bootstrap Icons (`bi-*`) |
| Fonts | Cinzel (display), Inter (body), monospace (data readouts) |

### NuGet Packages

| Package | Version | Purpose |
|---|---|---|
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | 8.0.0 | Identity + EF Core integration |
| `Microsoft.EntityFrameworkCore.Sqlite` | 8.0.0 | SQLite database provider |

---

## Database Schema

NOMAD uses a SQLite database managed by Entity Framework Core. The schema extends the ASP.NET Core Identity base tables.

### Core Entities

**`ApplicationUser`** (extends `IdentityUser`)
- `FullName`, `Nationality`, `PreferredLanguage`, `PreferredCurrency`
- `AvatarUrl`, `Bio`
- `ActiveDestination`, `ActiveCountry` — drives context across all modules
- `SerializedVisitedCountries` — JSON-serialised list
- `CreatedAt`, `LastLoginAt`, `IsActive`
- Navigation: `EmergencyContacts`, `TravelPreferences`, `TravelHistory`

**`TravelPreferences`** (one-to-one with `ApplicationUser`)
- `TravelStyle` (Budget / Mid-range / Luxury)
- `PreferredActivities`, `FavoriteDestinations`, `DietaryRestrictions`, `AccessibilityNeeds`
- `ReceiveWeatherAlerts`, `ReceiveSafetyAlerts`, `ReceiveNewsAlerts`

**`EmergencyContact`** (many-to-one with `ApplicationUser`)
- `Name`, `Phone`, `Relationship`
- Cascade delete on user removal

**`TravelRecord`** (many-to-one with `ApplicationUser`)
- `Destination`, `StartDate`, `EndDate`, `TotalExpenses`

**`Trip`** (many-to-one with `ApplicationUser`)
- `TotalBudget`
- Navigation: `Expenses`, `SavedRoutes`

**`Expense`** (many-to-one with `Trip`)
- `Amount` (precision 18,2), `Category`, `Title`, `Date`

**`SavedRoute`** (many-to-one with `Trip`)
- Route coordinate data for map persistence

**`Alert`** (many-to-one with `ApplicationUser`)
- Persisted alert records per user

**`OfflineCardDb`** (many-to-one with `ApplicationUser`)
- `Type`, `Title`, `Content`, `MetaData`, `Icon`
- Persistent offline vault entries per user

---

## Setup & Installation

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- Git

### Clone & Run

```bash
git clone <your-repository-url>
cd NOMAD/NOMAD
dotnet restore
dotnet run
```

The application will be available at `https://localhost:5001` (or the port shown in terminal output).

The SQLite database (`nomad.db`) is created automatically on first run via `db.Database.EnsureCreated()`.

### First-Time Setup

1. Navigate to `/register` to create your first account
2. Log in at `/login`
3. Go to **Explorer Profile** (`/profile`) and set your `Active Destination` and `Active Country`
4. Return to the **Dashboard** — all modules will load data contextualised to your destination

---

## Configuration

Application settings are managed in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=nomad.db"
  },
  "AppSettings": {
    "AppName": "NOMAD",
    "Version": "1.0.0",
    "SupportEmail": "support@nomad.travel"
  }
}
```

### Password Policy (configured in `Program.cs`)

| Rule | Setting |
|---|---|
| Minimum length | 6 characters |
| Require digit | ✅ |
| Require lowercase | ✅ |
| Require uppercase | ❌ |
| Require non-alphanumeric | ❌ |
| Unique email | ✅ |
| Email confirmation | ❌ (disabled for development) |

---

## Project Structure

```
NOMAD/
└── NOMAD/
    ├── Components/
    │   ├── App.razor                    # Root component
    │   ├── Routes.razor                 # Route configuration
    │   ├── _Imports.razor               # Global using statements
    │   ├── Layout/
    │   │   ├── AuthLayout.razor         # Unauthenticated shell
    │   │   └── MainLayout.razor         # Authenticated shell with sidebar
    │   ├── Pages/
    │   │   ├── Home.razor               # /dashboard — Command Centre
    │   │   ├── WeatherPage.razor        # /weather
    │   │   ├── Map.razor                # /map
    │   │   ├── Finance.razor            # /finance
    │   │   ├── Culture.razor            # /culture
    │   │   ├── Translator.razor         # /translate
    │   │   ├── Emergency.razor          # /emergency
    │   │   ├── OfflineCards.razor       # /offline
    │   │   ├── Profile.razor            # /profile
    │   │   ├── Login.razor              # /login
    │   │   ├── Register.razor           # /register
    │   │   ├── ForgotPassword.razor     # /forgot-password
    │   │   ├── Logout.razor             # /logout
    │   │   └── Error.razor              # Error page
    │   └── Shared/
    │       └── NotificationToast.razor  # Global toast overlay
    ├── Data/
    │   └── ApplicationDbContext.cs      # EF Core DbContext
    ├── DTOs/
    │   ├── LoginDto.cs
    │   ├── RegisterDto.cs
    │   ├── ProfileUpdateDto.cs
    │   └── ForgotPasswordDto.cs
    ├── Models/
    │   ├── ApplicationUser.cs           # Extended Identity user
    │   ├── TravelPreferences.cs
    │   ├── TravelRecord.cs
    │   ├── Trip.cs
    │   ├── Expense.cs
    │   ├── EmergencyContact.cs
    │   ├── Alert.cs
    │   ├── SavedRoute.cs
    │   └── OfflineCardDb.cs
    ├── Services/
    │   ├── DashboardService.cs          # Central destination resolver
    │   ├── WeatherService.cs            # Weather data service
    │   ├── MapService.cs                # Map + route intelligence
    │   ├── FinanceService.cs            # Expense management
    │   ├── CultureService.cs            # Translation + cultural data
    │   ├── SafetyService.cs             # Safety data façade
    │   └── NotificationService.cs       # Background alert broadcaster
    ├── wwwroot/
    │   ├── css/
    │   │   └── nomad.css                # Custom design system
    │   ├── js/
    │   │   ├── mapInterop.js            # Leaflet.js bridge
    │   │   ├── chartInterop.js          # Chart.js bridge
    │   │   ├── speechInterop.js         # Web Speech API bridge
    │   │   └── storageInterop.js        # localStorage bridge
    │   └── bootstrap/
    │       └── bootstrap.min.css
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── Program.cs                       # App bootstrap + DI config
    └── NOMAD.csproj
```

---

*NOMAD — Navigate the world with intelligence.*
