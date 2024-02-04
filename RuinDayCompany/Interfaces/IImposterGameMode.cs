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
        // Crack because Lethal Company Author is stupid.
        T SpawnObject<T>(string itemName)
            where T : GrabbableObject;
        void DisplayIntro();
    }
}
