namespace ViceCity.Models.Players
{
    public class CivilPlayer : Player
    {
        private const int initialLifePoints = 50;
        public CivilPlayer(string name) : base(name, initialLifePoints)
        {
        }
    }
}
