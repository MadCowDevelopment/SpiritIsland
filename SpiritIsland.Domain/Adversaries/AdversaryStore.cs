using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SpiritIsland.Domain.Adversaries
{
    public class AdversaryStore
    {
        public AdversaryStore()
        {
            var adversaries = typeof(AdversaryStore).Assembly.GetTypes().Where(p => typeof(Adversary).IsAssignableFrom(p) && !p.IsAbstract).Select(p => Activator.CreateInstance(p)).OfType<Adversary>().ToList();
            Adversaries = new ReadOnlyCollection<Adversary>(adversaries);
        }

        public ReadOnlyCollection<Adversary> Adversaries { get; }
    }
}