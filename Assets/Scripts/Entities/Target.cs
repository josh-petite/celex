using System;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public interface ITarget
    {
        string Name { get; set; }
        float Durability { get; set; }
        event Target.TargetClicked OnTargetClicked;
        event Target.TargetDied OnTargetDeath;
        float GetDurabilityPercentage();
        Transform GetTransform();
        void DecreaseDurability(float amount);
    }

    public abstract class Target : MonoBehaviour, ITarget
    {
        public delegate void TargetClicked(ITarget e);

        public abstract string Name { get; set; }
        public abstract float Durability { get; set; }

        event TargetClicked ITarget.OnTargetClicked
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        event TargetDied ITarget.OnTargetDeath
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        public abstract float GetDurabilityPercentage();
        public abstract Transform GetTransform();
        public abstract void DecreaseDurability(float amount);
        public static event TargetClicked OnTargetClicked;

        public delegate void TargetDied(ITarget e);
        public static event TargetDied OnTargetDeath;

        protected static void BroadcastOnTargetClicked(ITarget e)
        {
            var handler = OnTargetClicked;
            if (handler != null) handler(e);
        }

        protected static void BroadcastOnTargetDeath(ITarget e)
        {
            var handler = OnTargetDeath;
            if (handler != null) handler(e);
        }
    }
}
