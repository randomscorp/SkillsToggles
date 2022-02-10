using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using ItemChanger;
using ItemChanger.FsmStateActions;
using ItemChanger.Extensions;

using Modding;


namespace SkillsToggles.Toggles
{
    public  abstract class BaseToggle
    {
        //public string state => this.GetType().Name;

        public abstract string fsmStateName { get;}
        public abstract Dictionary<string,string> ChoicesOptions { get; } 


        public virtual void Change(PlayMakerFSM fsm)
        {

            if (ChoicesOptions != null)
            {

                foreach (KeyValuePair<string, string> choice in ChoicesOptions)
                {
                    fsm.GetState(choice.Key).AddFirstAction(new Lambda(() => fsm.SendEvent(choice.Value)));
                }
            }

            fsm.GetState(fsmStateName).AddLastAction(new LambdaEveryFrame(ListenForNailPress));
            void ListenForNailPress()
            {
                Config(fsm);
                if (InputHandler.Instance.inputActions.attack.WasPressed)
                {
                    Upgrade(fsm);
                    Update(fsm);
                }

            }





        }


        public abstract void Upgrade(PlayMakerFSM fsm);
        public abstract void Update(PlayMakerFSM fsm);
        public virtual void Config(PlayMakerFSM fsm) {
        }

    }
}
