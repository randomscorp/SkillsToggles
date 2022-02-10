﻿using System;
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
    class FireBall : BaseToggle
    {
        public override string fsmStateName => "Fireball";
        public override Dictionary<string, string> ChoicesOptions => new()
        {
            {"Choice 2", "OPT A" },
            {"Choice 3","OPT A" }
        };

        public override void Update(PlayMakerFSM fsm)
        {
        }

        public override void Change(PlayMakerFSM fsm)
        {
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

        public override void Upgrade(PlayMakerFSM fsm)
        {
            int spell= PlayerData.instance.fireballLevel;

            PlayMakerFSM active = fsm.gameObject.transform.Find("Inv_Items").Find($"Spell {fsmStateName}").gameObject.LocateMyFSM("Check Active");

            switch (spell)
            {
                case 0:
                    PlayerData.instance.SetInt(nameof(PlayerData.fireballLevel), 1);
                    active.SetState("Lv 1");
                    break;
                case 1:
                    PlayerData.instance.SetInt(nameof(PlayerData.fireballLevel), 2);
                    active.SetState("Lv 2");
                    break;
                case 2:
                    PlayerData.instance.SetInt(nameof(PlayerData.fireballLevel), 0);
                    active.SetState("Inactive");
                    break;
                default:
                    PlayerData.instance.SetInt(nameof(PlayerData.fireballLevel), 0);
                    active.SetState("Inactive");
                    break;
            }

        }

        public override void Config(PlayMakerFSM fsm)
        {
            int spell = PlayerData.instance.fireballLevel;
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            updateText.FsmVariables.GetFsmString("Convo Name").Value = $"INV_DESC_SPELL_{fsmStateName.ToUpper()}" + (spell == 2 ? spell : 1);
            updateText.FsmVariables.GetFsmString("Convo Desc").Value = $"INV_NAME_SPELL_{fsmStateName.ToUpper()}" + (spell == 2 ? spell : 1);
            updateText.SendEvent("UPDATE TEXT");

        }
    }
}


