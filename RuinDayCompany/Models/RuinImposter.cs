using RuinDayCompany.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Models
{
    public class RuinImposter : RuinEmployee
    {
        public RuinImposter(IRuinCrewmate employee) : base(employee)
        {
        }
    }
}
