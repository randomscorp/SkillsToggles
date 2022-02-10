using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using ItemChanger;
using ItemChanger.FsmStateActions;
using ItemChanger.Extensions;

using HutongGames.PlayMaker;


namespace SkillsToggles.Toggles
{
    public  class Nail : BaseToggle
    {
        public override string fsmStateName => "Nail";

        public override Dictionary<string, string> ChoicesOptions =>null;

        public override void Update(PlayMakerFSM fsm)
        {
            int nailsmithUpgrades = 1 + PlayerData.instance.GetInt(nameof(PlayerData.nailSmithUpgrades));
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            updateText.FsmVariables.GetFsmString("Convo Name").Value = "INV_NAME_NAIL" + nailsmithUpgrades;
            updateText.FsmVariables.GetFsmString("Convo Desc").Value = "INV_DESC_NAIL" + nailsmithUpgrades;
            fsm.gameObject.GetComponentInChildren<InvNailSprite>().SendMessage("OnEnable");

            updateText.SendEvent("UPDATE TEXT");

            GameObject trinkets = fsm.FsmVariables.GetFsmGameObject("Equip Item 5").Value;

            foreach (Component component in trinkets.GetComponents<Component>())
            {
                Modding.Logger.Log(component);
            }
        }

        public override void Upgrade(PlayMakerFSM fsm)
        {
            PlayerData.instance.SetBool(nameof(PlayerData.honedNail), true);

            if (PlayerData.instance.GetInt(nameof(PlayerData.nailSmithUpgrades)) < 4)
            {
                PlayerData.instance.IntAdd(nameof(PlayerData.nailDamage), 4);
                PlayerData.instance.IncrementInt(nameof(PlayerData.nailSmithUpgrades));
            }
            else
            {
                PlayerData.instance.SetInt(nameof(PlayerData.nailDamage), 5);
                PlayerData.instance.SetInt(nameof(PlayerData.nailSmithUpgrades), 0);
            }
            PlayMakerFSM.BroadcastEvent("UPDATE NAIL DAMAGE");
        }
    }
}
