using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsToggles
{

    public class States
    {
        public Dictionary<string, bool> has_Bools=new()
        {

            { nameof(PlayerData.hasWalljump), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasWalljump)) },
            { nameof(PlayerData.hasAcidArmour), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasAcidArmour)) },
            { nameof(PlayerData.hasLantern), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasLantern)) },
            { nameof(PlayerData.hasSuperDash), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasSuperDash)) },
            { nameof(PlayerData.hasDoubleJump), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasDoubleJump)) },
            { nameof(PlayerData.hasDreamNail), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasDreamNail)) },
            { nameof(PlayerData.dreamNailUpgraded), PlayerData.instance.GetBoolInternal(nameof(PlayerData.dreamNailUpgraded)) },
            { nameof(PlayerData.hasDreamGate), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasDreamGate)) },
            { nameof(PlayerData.hasDash), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasDash))},
            { nameof(PlayerData.hasShadowDash), PlayerData.instance.GetBoolInternal(nameof(PlayerData.hasShadowDash))}


        };



        public Dictionary<string, int> has_Ints = new() {
            { nameof(PlayerData.nailSmithUpgrades), PlayerData.instance.GetIntInternal(nameof(PlayerData.nailSmithUpgrades)) },
            { nameof(PlayerData.nailDamage), PlayerData.instance.GetIntInternal(nameof(PlayerData.nailDamage)) },
            { nameof(PlayerData.fireballLevel), PlayerData.instance.GetIntInternal(nameof(PlayerData.fireballLevel)) },
            { nameof(PlayerData.screamLevel), PlayerData.instance.GetIntInternal(nameof(PlayerData.screamLevel)) },
            { nameof(PlayerData.quakeLevel), PlayerData.instance.GetIntInternal(nameof(PlayerData.quakeLevel)) },
        };

    }
}
