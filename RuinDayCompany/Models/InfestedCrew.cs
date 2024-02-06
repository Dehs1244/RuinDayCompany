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
    public class InfestedCrew : Collection<IRuinCrewmate>
    {
        [JsonProperty("crew")]
        public IEnumerable<RuinImpostor> Impostors => Items.OfType<RuinImpostor>();
        public IEnumerable<AntiRuinSecurity> AntiRuins => Items.OfType<AntiRuinSecurity>();

        public RuinImpostor MainImpostor => Impostors.First();

        [JsonProperty("currentLocalCrewmate")]
        public IRuinCrewmate CurrentLocalCrewmate => Items.FirstOrDefault(x => x.IsLocal);

        // Crack.
        public bool IsImpostor(IRuinCrewmate crewmate) => Impostors.Any(x => x.Name == crewmate.Name); 
        public bool IsImpostor(PlayerControllerB crewmate) => Impostors.Any(x => x.Name == crewmate.playerUsername);
        public bool IsLocalPlayerImpostor() => IsImpostor(CurrentLocalCrewmate);
    }
}
