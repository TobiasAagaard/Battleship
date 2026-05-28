using Battleship.Shared.Models;

namespace Battleship.Shared.Logic;

public class BoardLogic
{

    public BoardLogic()
    {
    }

    public bool IsHitOnFleet(Board board,string letter, int number)
    {
        var ship = board.ShipLocations.FirstOrDefault(s => s.Matches(letter, number) && s.IsShip);
        if (ship is null)
            return false;

        ship.MarkHit();
        return true;
    }
    
    public bool TryPlaceShip(Board board, string location)
    {
        if (!Board.TryParseLocation(location, out string letter, out int number))
            return false;

        
        if (!board.ShotGrid.ContainsKey((letter, number)))
            return false;

        
        bool alreadyOccupied = board.ShipLocations.Any(s => s.Matches(letter, number));
        if (alreadyOccupied)
            return false;

        var spot = new GridSpot(letter, number);
        spot.PlaceShip();
        board.AddShip(spot);
        return true;
    }

    public bool IsShotValid(Board board, string letter, int number)
    {
        if (!board.ShotGrid.TryGetValue((letter, number), out GridSpot? spot))
            return false;

        return spot.IsAvailable;
    }

    public void RecordShot(Board board, string letter, int number, bool isHit)
    {
        if (!board.ShotGrid.TryGetValue((letter, number), out GridSpot? spot))
            return;

        if (isHit)
            spot.MarkHit();
        else
            spot.MarkMiss();
    }
}