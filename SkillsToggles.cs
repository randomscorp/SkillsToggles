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
using SkillsToggles.Toggles;
using SkillsToggles.BaseClasses;


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
            On.PlayerData.SetBool += SetGS;

             Dictionary<string, InvItem> items = new()
            {
                { "Claw", new("2", "Walljump", nameof(PlayerData.hasWalljump)) },
                { "Ismas", new("5", "Acid Armour", nameof(PlayerData.hasAcidArmour)) },
                { "Lantern", new("6", "Lantern", nameof(PlayerData.hasLantern)) },
                { "Cdash", new("3", "Super Dash", nameof(PlayerData.hasSuperDash)) },
                { "Wings", new("4", "Double Jump", nameof(PlayerData.hasDoubleJump)) },
            };


            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Nail().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new FireBall().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Dive().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Wraiths().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new DreamGate().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new DreamNail().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Dash().Change);

            foreach(InvItem item in items.Values)
            {
                Events.AddFsmEdit(new("Inv", "UI Inventory"), item.Change);

            }



        }

        private void SetGS(On.PlayerData.orig_SetBool orig, PlayerData self, string boolName, bool value)
        {

            foreach (var control in typeof(GlobalSettings).GetProperties())
            {
                Modding.Logger.Log("name: "+boolName +"  " + control.Name);
                Modding.Logger.Log("isasdasdasdas: " + nameof(boolName) == nameof(control.Name));
               // Modding.Logger.Log("value ="+value);

                if ((boolName.ToString() == control.Name.ToString()) && value==true)
                {
                    Modding.Logger.Log("oi");
                    control.SetValue(GS,value);
                }
            }
            orig(self, boolName, value);

        }
    }

}
