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
using SkillsToggles.Toggles;

namespace SkillsToggles
{
    public class SkillsToggles : Mod, IGlobalSettings<GlobalSettings>
    {

        internal static SkillsToggles Instance ;

        public static GlobalSettings GS = new();
        public void OnLoadGlobal(GlobalSettings gs) => GS = gs;
        public GlobalSettings OnSaveGlobal() => GS;


        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");


            Hooks();
            Instance = this;

            Log("Initialized");
        }


        private void Hooks()
        {
            ModHooks.GetPlayerBoolHook += getBools;
            ModHooks.GetPlayerIntHook += getInt;
            ModHooks.SetPlayerBoolHook += setBool;
            ModHooks.SetPlayerIntHook += setInt;

            On.HeroController.Start += HeroController_Start;
        }

        private int setInt(string name, int orig)
        {
            if (GS.has_Ints.ContainsKey(name))
            {
                GS.has_Ints[name] = orig;
            }
            return orig;
        }

        private bool setBool(string name, bool orig)
        {
            if (GS.has_Bools.ContainsKey(name))
            {
                GS.has_Bools[name] = orig;
            }
            return orig;
        }

        private void HeroController_Start(On.HeroController.orig_Start orig, HeroController self)
        {

            //Patch wraiths menu
            Events.AddFsmEdit(new("Inv", "UI Inventory"), PatchWraiths);
            void PatchWraiths(PlayMakerFSM fsm)
            {

                fsm.GetState("Choice 15").RemoveAction(-1);
                fsm.GetState("Choice 15").AddLastAction(new Lambda(() => fsm.SendEvent("OPT D")));
            }


            Dictionary<string, Toggle> items = new()
            {
                { "Claw", new InvItem("2", "Walljump", nameof(PlayerData.hasWalljump)) },
                { "Ismas", new InvItem("5", "Acid Armour", nameof(PlayerData.hasAcidArmour)) },
                { "Lantern", new InvItem("6", "Lantern", nameof(PlayerData.hasLantern)) },
                { "Cdash", new InvItem("3", "Super Dash", nameof(PlayerData.hasSuperDash)) },
                { "Wings", new InvItem("4", "Double Jump", nameof(PlayerData.hasDoubleJump)) },

                { "Nail", new Nail() },

                { "Fireball", new Spells("Fireball", nameof(PlayerData.fireballLevel), new() { "Choice 2", "Choice 3" }) },
                { "Dive", new Spells("Quake", nameof(PlayerData.quakeLevel), new() { "Choice 4", "Choice 11" }) },
                { "Wraiths", new Spells("Scream", nameof(PlayerData.screamLevel), new() { "Choice 13", "Choice 12" }) },

                { "Dream Nail", new DreamNail() },

                { "Dream Gate", new DreamGate() },

                { "Dash", new Dash() }


            };

            PlayMakerFSM fsm = GameObject.Find("_GameCameras").FindChild("HudCamera").FindChild("Inventory").FindChild("Inv").LocateFSM("UI Inventory");
            foreach (Toggle item in items.Values)
            {
                item.Change(fsm);
            }
        }

        private int getInt(string name, int orig)
        {
            return (GS.has_Ints.ContainsKey(name) ? GS.has_Ints[name] : orig);
        }

        private bool getBools(string name, bool orig)
        {

            return (GS.has_Bools.ContainsKey(name) ? GS.has_Bools[name] && orig  : orig) ;
        }

    }
    }

