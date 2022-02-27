using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsToggles
{
    public class GlobalSettings
    {
        public Dictionary<string, bool> has_Bools = new()
        {
            { nameof(PlayerData.hasWalljump), false },
            { nameof(PlayerData.hasAcidArmour), false },
            { nameof(PlayerData.hasLantern), false },
            { nameof(PlayerData.hasSuperDash), false },
            { nameof(PlayerData.hasDoubleJump), false },
        };

        public Dictionary<string, int> has_Ints = new() {
            
        
        };

    }
}
