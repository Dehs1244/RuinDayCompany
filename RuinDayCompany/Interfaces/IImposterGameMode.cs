using RuinDayCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Interfaces
{
    internal interface IImpostorGameMode
    {
        InfestedCrew Crew { get; }
    }
}
