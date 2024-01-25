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
            IEnumerable<RuinImpostor> Impostors = _ChooseImpostors();

            var crew = new InfestedCrew(Impostors);
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
