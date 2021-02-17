using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine 
{
    public class Unlockable : MonoBehaviour
    {
        private List<UnlockableType> requirements = new List<UnlockableType>();
        private Dictionary<AttributeTypeEnum, long> requiredAttributes = new Dictionary<AttributeTypeEnum, long>();

        public Unlockable(List<UnlockableType> requirements)
        {
            this.requirements = requirements;
        }

        public Unlockable() {
            
        }

        public void SetRequirements(List<UnlockableType> requirements)
        {
            this.requirements = requirements;
        }

        public void AddOrUpdateRequirement(UnlockableTypeEnum unlockableTypeEnum, long levelOrId)
        {
            var requirement = requirements.FirstOrDefault(x => x._unlockableType == unlockableTypeEnum);
            if (requirement != null)
            {
                requirement._value = levelOrId;
            }
            else
            {
                UnlockableType unlockableType = new UnlockableType(unlockableTypeEnum, levelOrId);
                requirements.Add(unlockableType);
            }
        }

        public void AddOrUpdateRequiredAttribute(AttributeTypeEnum attributeTypeEnum, long level)
        {
            requiredAttributes[attributeTypeEnum] = level;
        }

        public List<UnlockableType> GetRequirementsByUnlockableType(UnlockableTypeEnum unlockableTypeEnum)
        {
            var requirementsByUnlockableType = requirements.Where(x => x._unlockableType == unlockableTypeEnum);
            if (requirementsByUnlockableType != null)
            {
                return requirementsByUnlockableType.ToList();
            }
            return new List<UnlockableType>();
        }
        
        public List<UnlockableType> GetAllRequirements()
        {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
			var allRequirements = requirements.Where(x => x._unlockableType != null);
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
			if (allRequirements != null)
            {
                return allRequirements.ToList();
            }

            return new List<UnlockableType>();
        }

        public Dictionary<AttributeTypeEnum, long> GetAllRequiredAttributes()
        {
            Dictionary<AttributeTypeEnum, long> reqAttrs = new Dictionary<AttributeTypeEnum, long>();
            foreach (var requiredAttribute in requiredAttributes)
            {
                reqAttrs[requiredAttribute.Key] = requiredAttribute.Value;
            }

            return reqAttrs;
        }

        public bool IsUnlocked(List<UnlockableType> current, Dictionary<AttributeTypeEnum, long> currentAttributes)
        {
            bool result = true;
            foreach (var requirement in requirements)
            {
                if (requirement._unlockableType == UnlockableTypeEnum.Skill)
                {
                    var currentReq = current.FirstOrDefault(x =>
                        x._unlockableType == UnlockableTypeEnum.Skill && x._value == requirement._value);
                    if (currentReq == null)
                    {
                        result = false;
                        break;
                    }
                }

                if (requirement._unlockableType == UnlockableTypeEnum.Level)
                {
                    var currentReq = current.FirstOrDefault(x =>
                        x._unlockableType == UnlockableTypeEnum.Level && x._value >= requirement._value);
                    if (currentReq == null)
                    {
                        result = false;
                        break;
                    }
                }
            }

            foreach (var requiredAttribute in requiredAttributes)
            {
                if (!currentAttributes.ContainsKey(requiredAttribute.Key) || currentAttributes[requiredAttribute.Key] < requiredAttribute.Value)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}