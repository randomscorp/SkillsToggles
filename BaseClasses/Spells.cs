using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HutongGames.PlayMaker;
using UnityEngine;
using ItemChanger;
using ItemChanger.FsmStateActions;
using ItemChanger.Extensions;
using SkillsToggles.BaseClasses;
using Modding;


namespace SkillsToggles.BaseClasses
{
    public class Spells : Toggle
    {
        private string fsmStateName { get; }
        private string levelName { get; }
        private List<string> ChoicesOptions { get; }

        public Spells(string fsmStateName, string Level, List<string> ChoicesOptions)
        {
            this.fsmStateName = fsmStateName;
            this.levelName = Level;
            this.ChoicesOptions = ChoicesOptions;
        }

        public virtual void Change(PlayMakerFSM fsm)
        {
            FsmStateAction[] str = fsm.GetState(fsmStateName).Actions;
            try
            {
                fsm.GetState(fsmStateName).Actions = new FsmStateAction[]
                {
                str[0],
                str[1],
                str[9],
                };
            }
            catch { }
            PlayMakerFSM active = fsm.gameObject.transform.Find("Inv_Items").Find($"Spell {fsmStateName}").gameObject.LocateMyFSM("Check Active");
            active.GetState("Check Spell?").AddFirstAction(new Lambda(() => active.SendEvent("CHECK")));
            try
            {
                active.GetState("Inactive").Actions = new FsmStateAction[]
            {
                active.GetState("Inactive").Actions[2]
            };
            }
            catch { }

                foreach (string choice in ChoicesOptions)
                {
                    fsm.GetState(choice).AddFirstAction(new Lambda(() => fsm.SendEvent("OPT A")));
                }
            
            try
            {
                fsm.GetState(fsmStateName).AddLastAction(new LambdaEveryFrame(ListenForNailPress));

            }
            catch { }

            void ListenForNailPress()
            {
                Config(fsm);
                if (InputHandler.Instance.inputActions.attack.WasPressed)
                {
                    Upgrade(fsm);
                }
            }

        }
        private void Upgrade(PlayMakerFSM fsm)
        {
            int spell = SkillsToggles.GS.has_Ints[levelName];

            PlayMakerFSM active = fsm.gameObject.transform.Find("Inv_Items").Find($"Spell {fsmStateName}").gameObject.LocateMyFSM("Check Active");

            if (spell >= PlayerData.instance.GetIntInternal(levelName))
            {
                SkillsToggles.GS.has_Ints[levelName] = 0;
                active.SetState("Inactive");
                return;
            }

            switch (spell)
            {
                case 0:
                    SkillsToggles.GS.has_Ints[levelName]= 1;
                    active.SetState("Lv 1");
                    break;
                case 1:
                    SkillsToggles.GS.has_Ints[levelName] = 2;
                    active.SetState("Lv 2");
                    break;
                case 2:
                    SkillsToggles.GS.has_Ints[levelName] = 0;
                    active.SetState("Inactive");
                    break;
                default:
                    SkillsToggles.GS.has_Ints[levelName] = 0;
                    active.SetState("Inactive");
                    break;

            }
        }
        private void Config(PlayMakerFSM fsm)
        {
            int spell = SkillsToggles.GS.has_Ints[levelName];
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            updateText.FsmVariables.GetFsmString("Convo Name").Value = $"INV_NAME_SPELL_{fsmStateName.ToUpper()}" + (spell == 2 ? spell : 1);
            updateText.FsmVariables.GetFsmString("Convo Desc").Value = $"INV_DESC_SPELL_{fsmStateName.ToUpper()}" + (spell == 2 ? spell : 1);
            updateText.SendEvent("UPDATE TEXT");
        }
    }
}
