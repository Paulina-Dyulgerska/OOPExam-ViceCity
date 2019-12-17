namespace ViceCity.Models.Guns
{
    public class Pistol : Gun
    {
        private const int bulletsPerBarrel = 10; // The initial BulletsPerBarrel count is the actual capacity of the barrel!
        private const int totalBullets = 100;
        public Pistol(string name) : base(name, bulletsPerBarrel, totalBullets)
        {
            this.BulletsPerShoot = 1;
        }
    }
}
