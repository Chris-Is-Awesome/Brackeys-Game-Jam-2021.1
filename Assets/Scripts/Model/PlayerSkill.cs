using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class PlayerSkill : Unlockable
    {
        public PlayerSkill(long level) : base(new List<UnlockableType>(){new UnlockableType(UnlockableTypeEnum.Level, level)})
        {
            
        }

        public PlayerSkill()
        {
            
        }
    }
}