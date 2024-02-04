using GameNetcodeStuff;
using RuinDayCompany.Extensions;
using RuinDayCompany.Interfaces;
using RuinDayCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Core
{
    public class CrewShuffler : ICrewShuffler
    {
        private readonly IEnumerable<IRuinCrewmate> _lethalCrew;
        public int MinPlayers { get; set; }

        public CrewShuffler(IEnumerable<IRuinCrewmate> crew)
        {
            _lethalCrew = crew.Shuffle();
            MinPlayers = Plugin.Instance.RuinDayConfig.MinPlayers.Value;
        }

        public InfestedCrew Shuffle()
        {
            IEnumerable<RuinImpostor> impostors = _ChooseImpostors();

            var crew = new InfestedCrew(impostors, _lethalCrew.First(x=> x.IsLocal));
            foreach(var crewmate in _lethalCrew)
            {
                crew.Add(crewmate);
            }

            return crew;
        }

        private IEnumerable<RuinImpostor> _ChooseImpostors()
        {
            if (_lethalCrew.Count() < MinPlayers)
                yield break;

            var indexer = 0;
            foreach(var crewmate in _lethalCrew)
            {
                if ((indexer % MinPlayers) == 0)
                    yield return new RuinImpostor(crewmate);
                indexer++;
            }
        }
    }
}
