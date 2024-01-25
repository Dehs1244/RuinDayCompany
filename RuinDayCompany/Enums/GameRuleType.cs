using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Enums
{
    [Flags]
    public enum GameRuleType : byte
    {
        All = 1 << 0,
        KillEveryone = 1 << 1,
        AbortQuota = 1 << 2,

    }
}
