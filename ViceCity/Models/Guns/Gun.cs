using System;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Utilities.Messages;

namespace ViceCity.Models.Guns
{
    public abstract class Gun : IGun
    {
        private string name;
        private int bulletsPerBarrel; // The initial BulletsPerBarrel count is the actual capacity of the barrel!
        private int totalBullets;

        protected Gun(string name, int bulletsPerBarrel, int totalBullets)
        {
            this.Name = name;
            this.BulletsPerBarrel = bulletsPerBarrel;
            this.TotalBullets = totalBullets;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGunName);
                }
                this.name = value;
            }

        }

        public int BulletsPerBarrel
        {
            get => this.bulletsPerBarrel;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBulletsPerBarrelCount);
                }
                this.bulletsPerBarrel = value;
            }
        }

        public int TotalBullets
        {
            get => this.totalBullets;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTotalBulletsCount);
                }
                this.totalBullets = value;
            }
        }

        public bool CanFire => this.TotalBullets + this.BulletsInBarrel >= this.BulletsPerShoot;

        protected int BulletsPerShoot { get; set; }

        private int BulletsInBarrel { get; set; }

        public int Fire()
        {
            if (this.BulletsInBarrel < this.BulletsPerShoot)
            {
                var bulletsNeeded = this.BulletsPerBarrel - this.BulletsInBarrel;

                if (this.TotalBullets >= bulletsNeeded)
                {
                    this.BulletsInBarrel += bulletsNeeded;
                    this.TotalBullets -= bulletsNeeded;
                }
                else
                {
                    this.BulletsInBarrel += this.TotalBullets;
                    this.TotalBullets = 0;
                }
            }

            this.BulletsInBarrel -= this.BulletsPerShoot;

            return this.BulletsPerShoot;
        }
    }
}
