using UnityEngine;
using System.Linq;
using Assets.Scripts.Entities;

public class Ship : MonoBehaviour
{
    private MiningLaser[] _miningLasers;
    private ITarget _target;

    public delegate void TargetSetAction(ITarget e);
    public static event TargetSetAction OnTargetSet;

    // Use this for initialization
    void Start()
    {
        _miningLasers = GetComponentsInChildren<MiningLaser>();
        Target.OnTargetClicked += Asteroid_OnTargetClicked;
        Target.OnTargetDeath += ClearTarget;
    }

    private void Asteroid_OnTargetClicked(ITarget target)
    {
        if (target == null) return;

        _target = target;
        BroadcastTargetSetEvent();

        _miningLasers.ToList().ForEach(ml =>
        {
            ml.LaserDestination = _target.GetTransform();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null) return;
        _target.DecreaseDurability(_miningLasers.Sum(ml => ml.Dps));
    }
    
    private void ClearTarget(ITarget target)
    {
        _target = null;
        BroadcastTargetSetEvent();
    }

    private void BroadcastTargetSetEvent()
    {
        if (OnTargetSet != null)
        {
            OnTargetSet(_target);
        }
    }
}
