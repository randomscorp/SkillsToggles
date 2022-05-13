using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsToggles.BaseClasses
{
    public interface Toggle
    {
        public abstract void Change(string name, PlayMakerFSM fsm);

    }
}
