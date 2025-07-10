# Corelane SDK for .NET

**Corelane** accelerates your development by providing essential plug-and-play services like **User Management**, **Notifications**, **Templates**, **SchedulerApi** and **Payments**, so you can focus on what matters most—your **core logic**.

This SDK helps you easily integrate with Corelane services in your .NET applications.

---

## 🚀 Features

- ✅ User API (Authentication, Authorization, Role Management)
- 📩 Notification API (Email, SMS, Push)
- 🧩 Template Management (Dynamic content templates)
- 💳 Payment API (Subscriptions, Billing)
- 🛠️ Simple configuration and plug-and-play usage
- 🔐 Built-in JWT and token handling

---

## 📦 Installation

```bash
dotnet add package Corelane.SDK


Or via NuGet Package Manager:
Install-Package Corelane.SDK

Add your API settings to appsettings.json:
{
  "Corelane": {
    "BaseUrl": "https://api.corelane.io",
    "ApiKey": "your-api-key-here"
  }
}

Register the SDK in Startup.cs or your Program.cs:
builder.Services.AddCorelane(options =>
{
    options.BaseUrl = configuration["Corelane:BaseUrl"];
    options.ApiKey = configuration["Corelane:ApiKey"];
});

