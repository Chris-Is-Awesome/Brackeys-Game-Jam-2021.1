using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Unlockable
    {
        private Dictionary<UnlockableTypeEnum, Int64> requirements = new Dictionary<UnlockableTypeEnum, Int64>();

        public Unlockable(Dictionary<UnlockableTypeEnum, Int64> requirements)
        {
            this.requirements = requirements;
        }
        
        public bool IsUnlocked(Dictionary<UnlockableTypeEnum, Int64> current)
        {
            return requirements.All(x => current.ContainsKey(x.Key) && current[x.Key].CompareTo(x.Value) >= 0);
        }
    }
}