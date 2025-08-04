# EventBus — Lightweight Type-Safe Event System for Unity

**EventBus** is a minimal, zero-allocation, and type-safe event system tailored for Unity.  
It allows publishing and subscribing to strongly-typed events — including parameterless ones — without relying on strings, enums, or magic identifiers.

---

## ✨ Features

- ✅ **Type-safe**: Events are represented by custom types (usually structs), ensuring compile-time safety.
- 🧼 **Zero GC allocations**: Struct-based events avoid boxing and unnecessary memory pressure.
- 🔄 **Parameterless support**: Easily create empty events without arguments using simple marker structs.
- 📦 **No dependencies**: Pure C#, drop-in ready — no third-party libraries required.
- ⚡ **Simple API**: Subscribe, Unsubscribe, and Publish — that's it.

---

## 📦 Installation

Just copy the `EventBus.cs` file into your Unity project.  
No additional setup required.

---

## 🚀 Usage

### 1. Define your event type
Each event is just a struct — it can carry data, or be empty for parameterless signals.

```csharp
public struct GameStarted { }

public struct PlayerDamaged
{
    public int Amount;
    public string Source;
}
```

### 2. Subscribe to the event

```csharp
EventBus.Subscribe<GameStarted>(() =>
{
    Debug.Log("Game has started!");
});

EventBus.Subscribe<PlayerDamaged>(e =>
{
    Debug.Log($"Player took {e.Amount} damage from {e.Source}");
});
```

3. Publish the event

```csharp
EventBus.Publish(new GameStarted());

EventBus.Publish(new PlayerDamaged
{
    Amount = 15,
    Source = "Enemy Archer"
});
```
Or for parameterless events (no allocations):
```csharp
EventBus.Publish<GameStarted>();
```
