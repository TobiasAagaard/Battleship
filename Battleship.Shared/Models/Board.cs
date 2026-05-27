namespace Battleship.Shared.Models;

public class Board
{
    private readonly Dictionary<(string, int), GridSpot> _shotGrid = new();
    private readonly List<GridSpot> _shipLocations = new();
    public IReadOnlyList<GridSpot> ShotGrid => _shotGrid.Values.ToList();
    public IReadOnlyList<GridSpot> ShipLocations => _shipLocations;
    public int ShotCount => _shotGrid.Values.Count(s => !s.IsAvailable);
   
    public Board()
    {
        string[] letters = { "A", "B", "C", "D", "E" };
        int[]    numbers = { 1, 2, 3, 4, 5 };

        foreach (var letter in letters)
        foreach (var number in numbers)
        {
            var spot = new GridSpot(letter, number);
            _shotGrid[(letter, number)] = spot;
        }
    }

    public bool IsHitOnFleet(string letter, int number)
    {
        var ship = _shipLocations.FirstOrDefault(s => s.Matches(letter, number) && s.IsShip);
        if (ship is null)
            return false;

        ship.MarkHit();
        return true;
    }
    
    public bool TryPlaceShip(string location)
    {
        if (!TryParseLocation(location, out string letter, out int number))
            return false;

        
        if (!_shotGrid.ContainsKey((letter, number)))
            return false;

        
        bool alreadyOccupied = _shipLocations.Any(s => s.Matches(letter, number));
        if (alreadyOccupied)
            return false;

        var spot = new GridSpot(letter, number);
        spot.PlaceShip();
        _shipLocations.Add(spot);
        return true;
    }

    public bool IsShotValid(string letter, int number)
    {
        if (!_shotGrid.TryGetValue((letter, number), out GridSpot? spot))
            return false;

        return spot.IsAvailable;
    }

    public void RecordShot(string letter, int number, bool isHit)
    {
        if (!_shotGrid.TryGetValue((letter, number), out GridSpot? spot))
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