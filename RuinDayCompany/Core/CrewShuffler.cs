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
        public byte MinPlayers { get; set; }

        public CrewShuffler(IEnumerable<IRuinCrewmate> crew)
        {
            _lethalCrew = crew.Shuffle();
            MinPlayers = 3;
        }

        public InfestedCrew Shuffle()
        {
            IEnumerable<RuinImposter> imposters = _ChooseImposters();

            var crew = new InfestedCrew(imposters);
            foreach(var crewmate in _lethalCrew)
            {
                crew.Add(crewmate);
            }

            return crew;
        }

        private IEnumerable<RuinImposter> _ChooseImposters()
        {
            if (_lethalCrew.Count() < MinPlayers)
                yield break;

            var indexer = 0;
            foreach(var crewmate in _lethalCrew)
            {
                if ((indexer % MinPlayers) == 0)
                    yield return new RuinImposter(crewmate);
                indexer++;
            }
        }
    }
}
