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
using SkillsToggles.BaseClasses;



namespace SkillsToggles.Toggles
{
    class DreamNail : BaseToggle
    {
        public override string fsmStateName => "Dream Nail";

        public override Dictionary<string, string> ChoicesOptions => new()
        {
            { "Choice 1", "OPT A" },
        };

        public override void Change(PlayMakerFSM fsm)
        {

            try { 
            
                FsmStateAction[] str = fsm.GetState(fsmStateName).Actions;
                fsm.GetState(fsmStateName).Actions = new FsmStateAction[]
                {
                    str[0],
                    str[1],
                    str[5],
                    };

                GameObject dn = fsm.gameObject.transform.Find("Inv_Items").Find("Dream Nail").gameObject;
                //GameObject.Destroy(dn.GetComponent<DeactivateIfPlayerdataFalse>());
                dn.GetComponent<SetPosIfPlayerdataBool>().playerDataBool = nameof(PlayerData.visitedDirtmouth);
                }
            catch
            {

            }
            finally
            {
                base.Change(fsm);
            }
        }

        public override void Config(PlayMakerFSM fsm)
        {
            fsm.gameObject.transform.Find("Inv_Items").Find("Dream Nail").gameObject.SetActive(true);

        }

        public override void Update(PlayMakerFSM fsm)
        {
            PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
            updateText.FsmVariables.GetFsmString("Convo Desc").Value = "INV_DESC_DREAMNAIL_" + (SkillsToggles.GS.has_Bools[nameof(PlayerData.dreamNailUpgraded)] ? "B" : "A");
            updateText.FsmVariables.GetFsmString("Convo Name").Value = "INV_NAME_DREAMNAIL_" + (SkillsToggles.GS.has_Bools[nameof(PlayerData.dreamNailUpgraded)] ? "B" : "A");
            updateText.SendEvent("UPDATE TEXT");

            //fsm.SetState(fsmStateName);

        }

        public override void Upgrade(PlayMakerFSM fsm)
        {

            GameObject dn = fsm.gameObject.transform.Find("Inv_Items").Find("Dream Nail").gameObject;


            bool hasDn = SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasDreamNail)];
            bool adn = SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.dreamNailUpgraded)];


            if (!hasDn && !adn)
            {
                SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasDreamNail)] = true;
                dn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                fsm.gameObject.GetComponentInChildren<InvItemDisplay>().BroadcastMessage("OnEnable");
                //fsm.GetState("Dream Nail").OnEnter();
                return;
            }
            else if (hasDn&& !adn)
            {
                SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.dreamNailUpgraded)] = true;
                dn.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                fsm.gameObject.GetComponentInChildren<InvItemDisplay>().BroadcastMessage("OnEnable");
                return;
            }
            else
            {
                SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.hasDreamNail)] = false;
                SkillsToggles.GS.has_Bools[nameof(PlayerData.instance.dreamNailUpgraded)] = false;

                dn.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                return;
            }

        }
    }
}