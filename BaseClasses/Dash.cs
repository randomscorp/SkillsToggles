using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modding;
using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using ItemChanger;
using ItemChanger.FsmStateActions;
using ItemChanger.Extensions;
using TMPro;
using SkillsToggles.BaseClasses;
using Logger = Modding.Logger;

namespace SkillsToggles.Toggles
{
    class Dash : BaseToggle
    {
        public override string fsmStateName => "Equip Item 1";

        public override Dictionary<string, string> ChoicesOptions => null;

        public override void Change(PlayMakerFSM fsm)
        {
            fsm.gameObject.transform.Find("Equipment").gameObject.LocateMyFSM("Build Equipment List").GetState("Dash").RemoveAction(0);

            base.Change(fsm);
        }

        public override void Update(PlayMakerFSM fsm)
        {
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            string shadow = SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasShadowDash)] ? "SHADOW" : "";
            updateText.FsmVariables.GetFsmString("Convo Name").Value = $"INV_NAME_{shadow}DASH";
            updateText.FsmVariables.GetFsmString("Convo Desc").Value = $"INV_DESC_{shadow}DASH";
            updateText.SendEvent("UPDATE TEXT");

            GameObject text = updateText.FsmVariables.GetFsmGameObject("Text Name").Value;// ;
            if (!PlayerData.instance.canDash)
            {
                text.GetComponent<TextMeshPro>().SetText("Disabled");
            }
    
        }

        public override void Upgrade(PlayMakerFSM fsm)
        {
            bool hasDash = SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasDash)];
            bool hasShadow = SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasShadowDash)];
            if (!hasDash && !hasShadow)
            {
                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasDash)]= true;
                PlayerData.instance.canDash= true;
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.white;

            }

            else if (hasDash && !hasShadow)
            {

                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasShadowDash)]= true;
                PlayerData.instance.canShadowDash= true;
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.white;

            }
            else
            {

                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasDash)] = false;
                PlayerData.instance.canDash = false;

                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasShadowDash)] = false;
                PlayerData.instance.canShadowDash = false;

                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.gray;



            }


        }
    }
}
