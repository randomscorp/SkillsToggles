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
namespace SkillsToggles
{
    public class SkillsToggles : Mod
    {
        internal static SkillsToggles Instance ;

        //public override List<ValueTuple<string, string>> GetPreloadNames()
        //{
        //    return new List<ValueTuple<string, string>>
        //    {
        //        new ValueTuple<string, string>("White_Palace_18", "White Palace Fly")
        //    };
        //}

        //public SkillsToggles() : base("SkillsToggles")
        //{
        //    Instance = this;
        //}

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");


            Hooks();
            Instance = this;

            Log("Initialized");
        }



        private void Hooks()
        {


            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Nail().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new FireBall().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Dive().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Wraiths().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new DreamNail().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Dash().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Claw().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new SuperDash().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Wings().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Ismas().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new Lantern().Change);
            Events.AddFsmEdit(new("Inv", "UI Inventory"), new DreamGate().Change);









            On.HeroController.Start += HeroController_Start;

        }

        private void HeroController_Start(On.HeroController.orig_Start orig, HeroController self)
        {
            PlayerData.instance.SetBool(nameof(PlayerData.visitedDirtmouth), true);
            orig(self);
        }


       

    }

}
