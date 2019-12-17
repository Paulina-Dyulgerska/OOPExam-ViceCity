using SpaceStation.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Core.Contracts;
using ViceCity.Models.Guns;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Neghbourhoods;
using ViceCity.Models.Neghbourhoods.Contracts;
using ViceCity.Models.Players;
using ViceCity.Models.Players.Contracts;
using ViceCity.Repositories;
using ViceCity.Repositories.Contracts;

namespace ViceCity.Core
{
    public class Controller : IController
    {
        private const int mainPlayerInitialLifePoints = 100;
        private const int civilPlayerInitialLifePoints = 50;

        private IPlayer mainPlayer;
        private List<IPlayer> civilPlayers;
        private IRepository<IGun> gunRepository;
        private INeighbourhood gangNeighbourhood;

        public Controller()
        {
            this.mainPlayer = new MainPlayer();
            this.civilPlayers = new List<IPlayer>();
            this.gunRepository = new GunRepository();
            this.gangNeighbourhood = new GangNeighbourhood();
        }
        public string AddGun(string type, string name)
        {
            IGun gun;

            switch (type)
            {
                case nameof(Pistol):
                    gun = new Pistol(name);
                    break;

                case nameof(Rifle):
                    gun = new Rifle(name);
                    break;

                default:
                    return string.Format(OutputMessages.invalidGunType);
            }
            this.gunRepository.Add(gun);

            return string.Format(OutputMessages.GunAdded, name, type);
        }

        public string AddGunToPlayer(string name)
        {
            string result = string.Empty;

            if (this.gunRepository.Models.Count > 0)
            {
                var gunToAdd = this.gunRepository.Models.ElementAt(0);

                if (mainPlayer.Name.EndsWith(name))
                {
                    mainPlayer.GunRepository.Add(gunToAdd);
                    this.gunRepository.Remove(gunToAdd);
                    result = string.Format(OutputMessages.AddedGunToMainPlayerGuns, gunToAdd.Name);
                }
                else
                {
                    var civilPlayerToAddGun = this.civilPlayers.FirstOrDefault(x => x.Name == name);

                    if (civilPlayerToAddGun != null)
                    {
                        civilPlayerToAddGun.GunRepository.Add(gunToAdd);
                        this.gunRepository.Remove(gunToAdd);
                        result = string.Format(OutputMessages.AddedGunToCivilPlayerGuns, gunToAdd.Name, civilPlayerToAddGun.Name);
                    }
                    else
                    {
                        result = string.Format(OutputMessages.NoCivilPlayerWithSuchName);
                    }
                }
            }
            else
            {
                result = string.Format(OutputMessages.NoGunsInTheRepository);
            }

            return result;
        }

        public string AddPlayer(string name)
        {
            IPlayer civilPlayer = new CivilPlayer(name);

            this.civilPlayers.Add(civilPlayer);

            return string.Format(OutputMessages.PlayerAdded, name);
        }

        public string Fight()
        {
            var result = new StringBuilder();

            this.gangNeighbourhood.Action(mainPlayer, civilPlayers);

            int countDeadCivilPlayers = 0;

            for (int i = 0; i < civilPlayers.Count; i++)
            {
                if (!civilPlayers[i].IsAlive)
                {
                    civilPlayers.RemoveAt(i);
                    i--;
                    countDeadCivilPlayers++;
                }
            }

            ////mojeh taka da si vzema civilnite jivi igrachi:
            ////int countDeadCivilPlayers = this.civilPlayers.Count(x => x.IsAlive);
            ////mojeh da napravq proverka dali sumata na igrachite v civilplayers e ravna na sumata im sled bitkata
            //int sumBeforeFight = this.civilPlayers.Sum(x => x.LifePoints);
            ////i da pusna tuk bitkata
            ////this.gangNeighbourhood.Action(mainPlayer, civilPlayers);
            ////i posle da proverq sumata sled bitkata:
            //int sumAfterFight = this.civilPlayers.Sum(x => x.LifePoints);
            ////i ako razlikata im e 0, znachi vsichki sa jivi i zdravi.


            bool mainPlayerIsWithMaxPoints = this.mainPlayer.LifePoints == mainPlayerInitialLifePoints;
            bool allOtherPlayersAreWithMaxPoints = (countDeadCivilPlayers == 0 &&
                this.civilPlayers.All(x => x.LifePoints == civilPlayerInitialLifePoints));

            if (mainPlayerIsWithMaxPoints && allOtherPlayersAreWithMaxPoints)
            {
                result.AppendFormat(OutputMessages.NoONeIsHearth);
            }
            else
            {
                result.AppendLine("A fight happened:");
                result.AppendLine($"Tommy live points: {mainPlayer.LifePoints}!");
                result.AppendLine($"Tommy has killed: {countDeadCivilPlayers} players!");
                result.AppendLine($"Left Civil Players: {this.civilPlayers.Count}!");
            }

            return result.ToString().TrimEnd();
        }
    }
}
