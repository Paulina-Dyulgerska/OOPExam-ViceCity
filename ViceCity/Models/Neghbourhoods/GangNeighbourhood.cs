using System.Collections.Generic;
using System.Linq;
using ViceCity.Models.Neghbourhoods.Contracts;
using ViceCity.Models.Players.Contracts;

namespace ViceCity.Models.Neghbourhoods
{
    public class GangNeighbourhood : INeighbourhood
    {
        public void Action(IPlayer mainPlayer, ICollection<IPlayer> civilPlayers)
        {
            foreach (var civilPlayer in civilPlayers)
            {
                Attack(mainPlayer, civilPlayer);

                if (mainPlayer.GunRepository.Models.Count == 0)
                {
                    break;
                }
            }

            var aliveCivilPlayers = civilPlayers.Where(x => x.IsAlive == true);

            foreach (var civilPlayer in aliveCivilPlayers)
            {
                Attack(civilPlayer, mainPlayer);

                if (!mainPlayer.IsAlive)
                {
                    break;
                }
            }
        }

        private static void Attack(IPlayer attacker, IPlayer target)
        {
            while (target.IsAlive && attacker.GunRepository.Models.Count > 0)
            {
                var currentGun = attacker.GunRepository.Models.ElementAt(0);

                if (currentGun.CanFire)
                {
                    target.TakeLifePoints(currentGun.Fire());
                }
                else
                {
                    attacker.GunRepository.Remove(currentGun);
                }
            }
        }
    }
}
