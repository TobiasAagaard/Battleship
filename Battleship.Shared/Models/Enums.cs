namespace Battleship_Shared;

 public enum GridSpotStatus
    {
        Empty,
        Ship,
        Miss,
        Hit,
        Sunk
    }

public enum ShipType
{
    Battleship,
    Submarine,
    Destroyer,
}

public enum Orientation
{
    Horizontal,
    Vertical
}

public enum GameStatus
{
    WaitingForPLayer,
    InProgress,
    Player1Won,
    Player2Won
}