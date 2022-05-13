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

        public override void Change(string name,PlayMakerFSM fsm)
        {
            fsm.gameObject.transform.Find("Equipment").gameObject.LocateMyFSM("Build Equipment List").GetState("Dash").RemoveAction(0);

            base.Change(name,fsm);
        }

        public override void Update(PlayMakerFSM fsm)
        {
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            string shadow = SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasShadowDash)] ? "SHADOW" : "";
            updateText.FsmVariables.GetFsmString("Convo Name").Value = $"INV_NAME_{shadow}DASH";
            updateText.FsmVariables.GetFsmString("Convo Desc").Value = $"INV_DESC_{shadow}DASH";

            if (!PlayerData.instance.hasDash)
            {
                GameObject text = updateText.FsmVariables.GetFsmGameObject("Text Name").Value;// ;
                text.GetComponent<TextMeshPro>().SetText("Disabled");
            }
            updateText.SendEvent("UPDATE TEXT");
    
        }

        public override void Upgrade(PlayMakerFSM fsm)
        {
            bool hasDash = SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasDash)];
            bool hasShadow = SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasShadowDash)];

            if (hasDash == PlayerData.instance.GetBoolInternal(nameof(PlayerData.instance.hasDash)) &&
                hasShadow == PlayerData.instance.GetBoolInternal(nameof(PlayerData.instance.hasShadowDash)))
            {
                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasDash)] = false;
                PlayerData.instance.canDash = false;

                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasShadowDash)] = false;
                PlayerData.instance.canShadowDash = false;

                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.gray;
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().gameObject.SetActive(false);



            }

            else if (!hasDash && !hasShadow)
            {
                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasDash)]= true;
                PlayerData.instance.canDash= true;
                //fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.white;
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().gameObject.SetActive(true);
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().gameObject.SendMessage("OnEnable");

            }

            else if (hasDash && !hasShadow)
            {

                SkillsToggles.GS.has_Bools[nameof(PlayerData.hasShadowDash)]= true;
                PlayerData.instance.canShadowDash= true;
                //fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.white;
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().gameObject.SetActive(true);
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().gameObject.SendMessage("OnEnable");

            }



        }
    }
}
