namespace ViceCity.Models.Guns
{
    public class Rifle : Gun
    {
        private const int bulletsPerBarrel = 50; // The initial BulletsPerBarrel count is the actual capacity of the barrel!
        private const int totalBullets = 500;
        public Rifle(string name) : base(name, bulletsPerBarrel, totalBullets)
        {
            this.BulletsPerShoot = 5;
        }
    }
}

