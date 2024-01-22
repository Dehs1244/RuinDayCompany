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
        public IEnumerable<RuinImposter> Imposters { get; }
        public RuinImposter MainImposter => Imposters.First();

        public InfestedCrew(IEnumerable<RuinImposter> imposters)
        {
            Imposters = imposters;
        }
    }
}
