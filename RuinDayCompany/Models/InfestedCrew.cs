using GameNetcodeStuff;
using Newtonsoft.Json;
using RuinDayCompany.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Models
{
    public class InfestedCrew
    {
        [JsonProperty("crew")]
        public IList<RuinImpostor> Impostors { get; private set; }
        public RuinImpostor MainImpostor => Impostors.First();
        [JsonProperty("currentLocalCrewmate")]
        public IRuinCrewmate CurrentLocalCrewmate { get; private set; }
        private Collection<IRuinCrewmate> _crewmates = new Collection<IRuinCrewmate>();

        public InfestedCrew(IEnumerable<RuinImpostor> impostors, IRuinCrewmate currentCrewmate)
        {
            Impostors = impostors.ToList();
            CurrentLocalCrewmate = currentCrewmate;
        }

        // Crack.
        public bool IsImpostor(IRuinCrewmate crewmate) => Impostors.Any(x => x.Name == crewmate.Name); 
        public bool IsImpostor(PlayerControllerB crewmate) => Impostors.Any(x => x.Name == crewmate.playerUsername);
        public bool IsLocalPlayerImpostor() => IsImpostor(CurrentLocalCrewmate);

        public void Add(IRuinCrewmate crewmate)
        {
            if(_crewmates == null) _crewmates = new Collection<IRuinCrewmate>();
            _crewmates.Add(crewmate);
            if (crewmate.IsLocal) CurrentLocalCrewmate = crewmate;
        }
    }
}
