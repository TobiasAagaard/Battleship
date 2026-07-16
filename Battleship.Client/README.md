# Battleship.Client

Console client for Battleship. Run it with:

```bash
dotnet run --project Battleship.Client
```

## Game modes

| # | Mode | What it does |
|---|------|--------------|
| 1 | Play Game Offline | Two players share one console. Each places a fleet, then they take turns firing. |
| 2 | Test TCP Connection | Connects to the server on port 50000 and echoes messages. Not a playable match yet. |
| 0 | Exit | Quits. |

## What a match looks like

Both players enter a name and place all five ships (Carrier 5, Battleship 4, Cruiser 3, Submarine 3, Destroyer 2) by giving a starting cell like `B3` and an orientation, `H` or `V`.

Then turns alternate. On your turn you see the opponent's board on the left and your own fleet on the right, and you fire at a coordinate:

```
Tobias's Turn

Opponent's Board:        Your Fleet:
A ~ ~ X ~ ~ ~ ~ ~ ~ ~    A S S S S S ~ ~ ~ ~ ~
B ~ ~ ~ ~ ~ ~ ~ ~ ~ ~    B ~ ~ ~ ~ ~ ~ ~ X ~ ~
C ~ # ~ ~ ~ ~ ~ ~ ~ ~    C ~ ~ S ~ ~ ~ ~ ~ ~ ~
  1 2 3 4 5 6 7 8 9 10     1 2 3 4 5 6 7 8 9 10

Please enter your shot selection: C4
```

| Symbol | Meaning |
|--------|---------|
| `~` | Unknown water, or a spot you haven't fired at |
| `X` | Hit |
| `#` | Sunk ship |
| `S` | Your own ship (only shown on your fleet) |
| ` ` | Miss |

The board is 10×10, rows `A`–`J` and columns `1`–`10`. Sink all five enemy ships to win.
