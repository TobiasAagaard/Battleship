namespace Battleship.Shared.Models;

public class Board
{
    private readonly Dictionary<(string, int), GridSpot> shotGrid = new();
    private readonly List<GridSpot> shipLocations = new();
    public IReadOnlyList<GridSpot> ShotGrid => shotGrid.Values.ToList();
    public IReadOnlyList<GridSpot> ShipLocations => shipLocations;
    public int ShotCount => shotGrid.Values.Count(s => !s.IsAvailable);
   
    public Board()
    {
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        int[]    numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        foreach (var letter in letters)
        foreach (var number in numbers)
        {
            var spot = new GridSpot(letter, number);
            shotGrid[(letter, number)] = spot;
        }
    }

    public bool IsHitOnFleet(string letter, int number)
    {
        var ship = shipLocations.FirstOrDefault(s => s.Matches(letter, number) && s.IsShip);
        if (ship is null)
            return false;

        ship.MarkHit();
        return true;
    }
    
    public bool TryPlaceShip(string location)
    {
        if (!TryParseLocation(location, out string letter, out int number))
            return false;

        
        if (!shotGrid.ContainsKey((letter, number)))
            return false;

        
        bool alreadyOccupied = shipLocations.Any(s => s.Matches(letter, number));
        if (alreadyOccupied)
            return false;

        var spot = new GridSpot(letter, number);
        spot.PlaceShip();
        shipLocations.Add(spot);
        return true;
    }

    public bool IsShotValid(string letter, int number)
    {
        if (!shotGrid.TryGetValue((letter, number), out GridSpot? spot))
            return false;

        return spot.IsAvailable;
    }

    public void RecordShot(string letter, int number, bool isHit)
    {
        if (!shotGrid.TryGetValue((letter, number), out GridSpot? spot))
            return;

        if (isHit)
            spot.MarkHit();
        else
            spot.MarkMiss();
    }
   
    public static bool TryParseLocation(string input, out string letter, out int number)
    {
        letter = "";
        number = 0;

        if (string.IsNullOrWhiteSpace(input) || input.Length != 2)
            return false;

        letter = input[0].ToString().ToUpper();
        return int.TryParse(input[1].ToString(), out number);
    }
}