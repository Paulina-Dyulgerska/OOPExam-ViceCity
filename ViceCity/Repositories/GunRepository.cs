using System.Collections.Generic;
using System.Linq;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Repositories.Contracts;

namespace ViceCity.Repositories
{
    internal class GunRepository : IRepository<IGun>
    {
        private ICollection<IGun> models;

        public GunRepository()
        {
            this.models = new List<IGun>();
        }
        public IReadOnlyCollection<IGun> Models => this.models.ToList().AsReadOnly();

        public void Add(IGun model)
        {
            if (!this.Models.Any(x => x.Name == model.Name))
            {
                this.models.Add(model);
            }
        }

        public IGun Find(string name)
        {
            return this.models.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IGun model)
        {
            bool hasRemoved = this.models.Remove(model); 

            return hasRemoved;
        }
    }
}
