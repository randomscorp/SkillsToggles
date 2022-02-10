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

namespace SkillsToggles.Toggles
{
    class Claw : BaseToggle
    {
        public override string fsmStateName => "Equip Item 2";

        public override Dictionary<string, string> ChoicesOptions => null;

        public override void Change(PlayMakerFSM fsm)
        {
            fsm.gameObject.transform.Find("Equipment").gameObject.LocateMyFSM("Build Equipment List").GetState("Walljump").RemoveAction(0);

            base.Change(fsm);
        }


        public override void Config(PlayMakerFSM fsm)
        {

            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            GameObject text = updateText.FsmVariables.GetFsmGameObject("Text Name").Value;// ;
            //updateText.SendEvent("UPDATE TEXT");
            if (!PlayerData.instance.canWallJump)
            {
                text.GetComponent<TextMeshPro>().SetText("Disabled");
            }
                
            else { text.GetComponent<TextMeshPro>().SetText("Mantis Claw"); }
            base.Config(fsm);
        }
        public override void Update(PlayMakerFSM fsm)
        {
            

        }

        public override void Upgrade(PlayMakerFSM fsm)
        {
            if (!PlayerData.instance.hasWalljump)
            {
                PlayerData.instance.SetBool(nameof(PlayerData.hasWalljump), true);
                PlayerData.instance.SetBool(nameof(PlayerData.canWallJump), true);
                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = Color.white;

            }

            else 
            {

                PlayerData.instance.SetBool(nameof(PlayerData.hasWalljump), false);
                PlayerData.instance.SetBool(nameof(PlayerData.canWallJump), false);

                fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color=Color.gray;

            }



        }
    }
}
