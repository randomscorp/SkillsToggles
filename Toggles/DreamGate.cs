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
    class DreamGate : BaseToggle
    {
        public override string fsmStateName => "Dream Gate";

        public override Dictionary<string, string> ChoicesOptions => new()
        {
            {"Choice 17", "OPT A" },
            { "Choice 16", "OPT C" },

        };

        public override void Change(PlayMakerFSM fsm)
        {

            try
            {

                FsmStateAction[] str = fsm.GetState(fsmStateName).Actions;
                fsm.GetState(fsmStateName).Actions = new FsmStateAction[]
                {
                    str[0],
                    str[1],
                    str[5],
                    };


            }
            catch { }
            finally
            {
                base.Change(fsm);
            }
        }

        public override void Config(PlayMakerFSM fsm)
        {
            fsm.gameObject.transform.Find("Inv_Items").Find("Dream Gate").gameObject.SetActive(true);
            base.Config(fsm);
        }

        public override void Update(PlayMakerFSM fsm)
        {
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            updateText.FsmVariables.GetFsmString("Convo Desc").Value = "INV_DESC_DREAMGATE";
            updateText.FsmVariables.GetFsmString("Convo Name").Value = "INV_NAME_DREAMGATE";
            updateText.SendEvent("UPDATE TEXT");


        }

        public override void Upgrade(PlayMakerFSM fsm)
        {

            GameObject dg = fsm.gameObject.transform.Find("Inv_Items").Find("Dream Gate").gameObject;
            if (!PlayerData.instance.hasDreamGate)
            {
                PlayerData.instance.SetBool(nameof(PlayerData.hasDreamGate), true);
                dg.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                if (PlayerData.instance.hasDreamNail)
                {
                    PlayerData.instance.SetBool(nameof(PlayerData.hasDreamNail), true);
                    GameObject dn = fsm.gameObject.transform.Find("Inv_Items").Find("Dream Nail").gameObject;
                    dn.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                }
                fsm.gameObject.GetComponentInChildren<InvItemDisplay>().BroadcastMessage("OnEnable");
            }

            else
            {
                PlayerData.instance.SetBool(nameof(PlayerData.hasDreamGate), false);
                dg.gameObject.GetComponent<SpriteRenderer>().enabled = false;


            }

        }
    }
}
