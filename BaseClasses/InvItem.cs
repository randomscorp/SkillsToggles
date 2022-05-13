using UnityEngine;
using ItemChanger.FsmStateActions;
using ItemChanger.Extensions;
using SkillsToggles;
using TMPro;
namespace SkillsToggles.BaseClasses
{
    public class InvItem:Toggle
    {
        private string equipNamePosition;
        private string equipmentStateName;
        private string playerDataName;
        private string itemName=null;
        public InvItem(string equipNamePosition, string equipmentStateName, string playerDataName)
        {
            this.equipNamePosition = equipNamePosition;
            this.equipmentStateName = equipmentStateName;
            this.playerDataName = playerDataName;
        }


        public void Change(string name, PlayMakerFSM  fsm)
        {


            string fsmStateName = $"Equip Item {equipNamePosition}";
            //Remove the Bool check on the Inventory equip items init
            // Items are always in the inventory 
            fsm.gameObject.transform.Find("Equipment").gameObject.LocateMyFSM("Build Equipment List")
            .GetState(equipmentStateName).RemoveAction(0);

            fsm.GetState("Determine Initial").AddFirstAction(new Lambda(()=>
               fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = PlayerData.instance.GetBool(playerDataName) ? Color.white : Color.gray
            
            ));

            //Add the change action in the Inv fsm
            fsm.GetState(fsmStateName).AddLastAction(new LambdaEveryFrame(ListenForNailPress));

            this.itemName = name;


            void ListenForNailPress()
            {

                #region Update Text

                PlayMakerFSM updateText = fsm.gameObject.LocateMyFSM("Update Text");
                GameObject text = updateText.FsmVariables.GetFsmGameObject("Text Name").Value;
                
                /*if (this.itemName == null)
                {
                    this.itemName = text.GetComponent<TextMeshPro>().text;
                }*/

                text.GetComponent<TextMeshPro>().SetText(this.itemName +
                    (PlayerData.instance.GetBool(playerDataName) ? "": " (Disabled)")
                    );
                updateText.SendEvent("UPDATE TEXT");

                #endregion

                if (InputHandler.Instance.inputActions.attack.WasPressed)
                {
                    SkillsToggles.GS.has_Bools[playerDataName]=!SkillsToggles.GS.has_Bools[playerDataName];
                    fsm.FsmVariables.GetFsmGameObject(fsmStateName).Value.GetComponent<SpriteRenderer>().color = PlayerData.instance.GetBool(playerDataName) ? Color.white : Color.gray;
                    
                }

            }

        }

    }

}