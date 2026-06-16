namespace Battleship.Shared.Models;

public class Board
{
    private readonly Dictionary<(string, int), GridSpot> shotGrid = new();
    public IReadOnlyDictionary<(string, int), GridSpot> ShotGrid => shotGrid;
    private readonly List<Ships> shipLocations = new();
    public IReadOnlyList<Ships> ShipLocations => shipLocations;
    public int ShotCount => shotGrid.Values.Count(s => !s.IsAvailable);

    public Board()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        int[]    numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        foreach (var letter in letters)
        {
            foreach (var number in numbers)
            {
                shotGrid[(letter, number)] = new GridSpot(letter, number);
            }
        }
    }

    public void AddShip(Ships ship) => shipLocations.Add(ship);
   
    public static bool TryParseLocation(string input, out string letter, out int number)
    {
        letter = "";
        number = 0;

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        input = input.Trim();
        if (input.Length < 2 || input.Length > 3)
        {
            return false;
        }
        letter = input[0].ToString().ToUpper();
        return int.TryParse(input[1..], out number);
    }
}