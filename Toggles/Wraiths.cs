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
namespace SkillsToggles.Toggles
{
    class Wraiths : BaseToggle
    {
        public override string fsmStateName => "Scream";

        public override Dictionary<string, string> ChoicesOptions => new()
        {
            { "Choice 13", "OPT A" },
            { "Choice 12", "OPT A" }
        };

        public override void Change(PlayMakerFSM fsm)
        {
            fsm.GetState("Choice 15").RemoveAction(-1);
            fsm.GetState("Choice 15").AddLastAction(new Lambda(() => fsm.SendEvent("OPT D")));

            FsmStateAction[] str = fsm.GetState(fsmStateName).Actions;

            fsm.GetState(fsmStateName).Actions = new FsmStateAction[]
            {
                str[0],
                str[1],
                str[9],
            };

            PlayMakerFSM active = fsm.gameObject.transform.Find("Inv_Items").Find($"Spell {fsmStateName}").gameObject.LocateMyFSM("Check Active");
            active.GetState("Check Spell?").AddFirstAction(new Lambda(() => active.SendEvent("CHECK")));
            active.GetState("Inactive").Actions = new FsmStateAction[]
            {
                active.GetState("Inactive").Actions[2]
            };

            base.Change(fsm);
        }

        public override void Config(PlayMakerFSM fsm)
        {
            int spell = PlayerData.instance.screamLevel;
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            updateText.FsmVariables.GetFsmString("Convo Name").Value = $"INV_NAME_SPELL_{fsmStateName.ToUpper()}" + (spell == 2 ? spell : 1);
            updateText.FsmVariables.GetFsmString("Convo Desc").Value = $"INV_DESC_SPELL_{fsmStateName.ToUpper()}" + (spell == 2 ? spell : 1);
            updateText.SendEvent("UPDATE TEXT");
        }

        public override void Update(PlayMakerFSM fsm)
        {
        }

        public override void Upgrade(PlayMakerFSM fsm)
        {
            int spell = PlayerData.instance.screamLevel;

            PlayMakerFSM active = fsm.gameObject.transform.Find("Inv_Items").Find($"Spell {fsmStateName}").gameObject.LocateMyFSM("Check Active");

            switch (spell)
            {
                case 0:
                    PlayerData.instance.SetInt(nameof(PlayerData.screamLevel), 1);
                    active.SetState("Lv 1");
                    break;
                case 1:
                    PlayerData.instance.SetInt(nameof(PlayerData.screamLevel), 2);
                    active.SetState("Lv 2");
                    break;
                case 2:
                    PlayerData.instance.SetInt(nameof(PlayerData.screamLevel), 0);
                    active.SetState("Inactive");
                    break;
                default:
                    PlayerData.instance.SetInt(nameof(PlayerData.screamLevel), 0);
                    active.SetState("Inactive");
                    break;
            }
        }

    }
}
