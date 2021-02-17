namespace UnityEngine
{
    public class UnlockableType
    {
        public UnlockableTypeEnum _unlockableType { get; set; }
        public long _value { get; set; }

        public UnlockableType(UnlockableTypeEnum unlockableTypeEnum, long value)
        {
            _unlockableType = unlockableTypeEnum;
            _value = value;
        }
    }
}