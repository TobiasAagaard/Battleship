# Battleship

A C# implementation of the classic game Battleship, played in the console.

## About the game
Battleship is a turn-based game played on a 5x5 grid. Each player places a fleet of ships on their own grid, hidden from the opponent. Players then take turns guessing coordinates on the opponent's grid to locate and sink the enemy fleet. The first player to sink all of the opponent's ships wins.

The game flow is:
1. Each player places their ships on their own 5x5 board.
2. Players take turns calling out a coordinate (e.g. `B3`) to fire a shot.
3. After each shot the player is told whether it was a *hit* or a *miss*.
4. When all of a player's ships are sunk, the other player wins.

## How to run
The project targets **.NET 10.0** and is built with the standard `dotnet` CLI.

From the repository root:

```bash
dotnet run --project Battleship.Client
```

Or, to build first and then run the produced binary:

```bash
dotnet build
dotnet run --project Battleship.Client
```

When the client starts you are greeted by the main menu:

- `1` — Play Game Offline (hot-seat on the same machine)
- `0` — Exit

## Project structure
- [Battleship.Client/](Battleship.Client/) — Console client, menus, views, and the game runner.
- [Battleship.Shared/](Battleship.Shared/) — Shared models (`Board`, `GridSpot`, `Player`) and board logic, intended to be reused by both client and a future server.

## Online
The goal of this project is to extend the offline game into a console-driven server using **TCP sockets**, capable of handling multiple clients at the same time. The online mode should be able to:

- Accept incoming TCP connections from multiple Battleship clients concurrently.
- Match two waiting clients into a single game session.
- Hold the authoritative game state on the server, so neither client can cheat by inspecting the other's board.
- Forward each player's actions (ship placement, shots fired) to the opponent and broadcast the result (hit/miss/sunk) back to both players in real time.
- Enforce the rules of the game (turn order, valid coordinates, ship placement) on the server side.
- Detect and handle disconnects cleanly, so a dropped client ends the session for both players instead of leaving the game in a stuck state.
