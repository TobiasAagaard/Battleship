using Battleship.Shared.Models;

namespace Battleship.Shared.Logic;

public class BoardLogic
{

    public BoardLogic()
    {
    }

    public bool IsHitOnFleet(Board board,string letter, int number)
    {
        foreach (Ships ship in board.ShipLocations)
        {
            if (ship.OccupiesSpot(new GridSpot(letter, number)))
            {
                ship.Hit(new GridSpot(letter, number));
                if (ship.IsSunk)
                {
                    ship.MarkSunk();
                }
                return true;
            }
        }
            return false;
    }
    
    public bool TryPlaceShip(Board board, ShipType type, string startLocation, Orientation orientation)
    {
        int size = Ships.SizeOf(type);
        if (!Board.TryParseLocation(startLocation, out string letter, out int number))
        {
            return false;
        }
        List<GridSpot> spots = new();
        char startLetter = letter[0];

        for (int i = 0; i < size; i++)
        {
            string l = orientation == Orientation.Vertical ? ((char)(startLetter + i)).ToString() : letter;
            int n = orientation == Orientation.Horizontal ? number + i : number;

            if (!board.ShotGrid.TryGetValue((l, n), out GridSpot? spot) || !spot.IsAvailable)
            {
                return false;
            }
            spots.Add(spot);

        }
        foreach (GridSpot spot in spots)
        {
            spot.PlaceShip();
        }
        board.AddShip(new Ships(type, spots));
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
        {
            return;
        }

        if (isHit)
        {
            spot.MarkHit();
        }
        else
        {
            spot.MarkMiss();
        }
    }
}