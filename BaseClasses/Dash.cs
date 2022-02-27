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
            if(!SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasDash)] && !SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasShadowDash)])
            {
                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasDash)]= true;
                SkillsToggles.GS.has_Bools[nameof(PlayerData.canDash)]= true;
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.white;

            }

            else if (SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasDash)] && !SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasShadowDash)])
            {

                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasShadowDash)]= true;
                SkillsToggles.GS.has_Bools[nameof(PlayerData.canShadowDash)]= true;
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.white;

            }
            else
            {
                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasDash)] = false;
                SkillsToggles.GS.has_Bools[nameof(PlayerData.canDash)] = false;

                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasShadowDash)] = false;
                SkillsToggles.GS.has_Bools[nameof(PlayerData.canShadowDash)] = false;

                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.gray;



            }


        }
    }
}
