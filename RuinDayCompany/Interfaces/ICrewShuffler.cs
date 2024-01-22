using RuinDayCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Interfaces
{
    public interface ICrewShuffler
    {
        InfestedCrew Shuffle();
        byte MinPlayers { get; set; }
    }
}
