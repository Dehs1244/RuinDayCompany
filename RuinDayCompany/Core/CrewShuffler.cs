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

        public InfestedCrew Shuffle() => _CreateCrew();

        private InfestedCrew _CreateCrew()
        {
            InfestedCrew crew = new InfestedCrew();

            foreach(var crewmate in _GiveCrewmatesRoles())
            {
                crew.Add(crewmate);
            }

            return crew;
        }

        private IEnumerable<IRuinCrewmate> _GiveCrewmatesRoles()
        {
            if (_lethalCrew.Count() < MinPlayers)
                yield break;

            for (var i = 0; i < _lethalCrew.Count(); i++)
            {
                if ((i % MinPlayers) == 0)
                    yield return new RuinImpostor(_lethalCrew.ElementAt(i));

                if (MinPlayers > 1 && (i % (MinPlayers + 1)) == 0)
                    yield return new AntiRuinSecurity(_lethalCrew.ElementAt(i));

                yield return _lethalCrew.ElementAt(i);
            }
        }
    }
}
