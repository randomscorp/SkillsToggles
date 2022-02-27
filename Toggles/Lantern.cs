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
    class Lantern : BaseToggle
    {
        public override string fsmStateName => "Equip Item 6";

        public override Dictionary<string, string> ChoicesOptions => null;

        public override void Change(PlayMakerFSM fsm)
        {

            fsm.gameObject.transform.Find("Equipment").gameObject.LocateMyFSM("Build Equipment List").GetState("Lantern").RemoveAction(0);
            base.Change(fsm);
        }

        public override void Update(PlayMakerFSM fsm)
        {
        }

        public override void Config(PlayMakerFSM fsm)
        {
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            GameObject text = updateText.FsmVariables.GetFsmGameObject("Text Name").Value;// ;
            //updateText.SendEvent("UPDATE TEXT");
            if (!PlayerData.instance.hasLantern)
            {
                text.GetComponent<TextMeshPro>().SetText("Disabled");
            }

            else { text.GetComponent<TextMeshPro>().SetText("Lantern"); }
            base.Config(fsm);

        }
        public override void Upgrade(PlayMakerFSM fsm)
        {
            if (!PlayerData.instance.hasLantern)
            {
                PlayerData.instance.SetBool(nameof(PlayerData.hasLantern), true);
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.white;

            }

            else
            {

                PlayerData.instance.SetBool(nameof(PlayerData.hasLantern), false);
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.gray;


            }
        }
    }
}
