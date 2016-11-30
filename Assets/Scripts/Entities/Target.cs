using System;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public interface ITarget
    {
        GameObject DisplayPrefab { get; set; }
        GameObject ExplosionPrefab { get; set; }
        Loot Loot { get; set; }

        string DisplayName { get; set; }
        float Durability { get; set; }

        event Target.TargetClicked OnTargetClicked;
        event Target.TargetDied OnTargetDeath;

        float GetDurabilityPercentage();
        Transform GetTransform();
        Vector3 GetPosition();
        void DecreaseDurability(float amount);
        GameObject GetGameObject();
    }

    public abstract class Target : MonoBehaviour, ITarget
    {
        public delegate void TargetClicked(ITarget e);

        public GameObject DisplayPrefab { get; set; }
        public GameObject ExplosionPrefab { get; set; }

        public Loot Loot { get; set; }
        public string DisplayName { get; set; }
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
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public abstract void DecreaseDurability(float amount);
        public GameObject GetGameObject()
        {
            return gameObject;
        }

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
