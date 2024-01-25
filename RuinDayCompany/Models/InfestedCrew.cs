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
        public IEnumerable<RuinImpostor> Impostors { get; }
        public RuinImpostor MainImpostor => Impostors.First();

        public InfestedCrew(IEnumerable<RuinImpostor> impostors)
        {
            Impostors = impostors;
        }
    }
}
