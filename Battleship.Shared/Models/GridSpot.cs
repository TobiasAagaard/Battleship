
namespace Battleship.Shared.Models
{
    public class GridSpot
    {
        public string Letter { get; set; } = string.Empty;
        public int Number { get; set; }
        public GridSpotStatus Status { get; set; } = GridSpotStatus.Empty;
        public bool IsAvailable => Status == GridSpotStatus.Empty;
        public bool IsShip => Status == GridSpotStatus.Ship;
        public enum GridSpotStatus
        {
            Empty,
            Ship,
            Miss,
            Hit,
            Sunk
        }  

        public GridSpot(string letter, int number)
        {
            Letter = letter.ToUpper();
            Number = number;
            Status = GridSpotStatus.Empty;
        }

        public bool Matches(string letter, int number) => Letter.Equals(letter.ToUpper()) && Number == number;

        public void PlaceShip() => Status = GridSpotStatus.Ship;
        public void MarkHit() => Status = GridSpotStatus.Hit;
        public void MarkSunk() => Status = GridSpotStatus.Sunk;
        public void MarkMiss() => Status = GridSpotStatus.Miss;


    }
}

