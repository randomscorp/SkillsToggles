using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsToggles.BaseClasses
{
    public abstract class Toggle
    {
        public abstract void Change(PlayMakerFSM fsm);

    }
}
