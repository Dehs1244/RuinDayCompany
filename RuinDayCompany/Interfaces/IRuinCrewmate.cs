using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Interfaces
{
    public interface IRuinCrewmate
    {
        string Name { get; }
        bool IsLocal { get; }
        void GiveItem(GrabbableObject item);
        int FindEmptySlot();
        void SwitchToEmptySlot();
        void SwitchToSlot(int index);
    }
}
