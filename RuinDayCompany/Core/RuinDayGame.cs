using GameNetcodeStuff;
using RuinDayCompany.Interfaces;
using RuinDayCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Core
{
    internal class RuinDayGame : IImpostorGameMode
    {
        public InfestedCrew Crew { get; }

        public RuinDayGame(InfestedCrew crew)
        {
            Crew = crew;
        }

        public RuinDayGame(IEnumerable<PlayerControllerB> gameStuffPlayers)
        {
            var shuffle = new CrewShuffler(RuinEmployee.CreateFromGamePlayers(gameStuffPlayers));

            Crew = shuffle.Shuffle();
        }
    }
}
