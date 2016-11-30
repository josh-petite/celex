using UnityEngine;
using System.Linq;
using Assets.Scripts.Entities;

public class Ship : MonoBehaviour
{
    private MiningLaser[] _miningLasers;
    private ITarget _target;

    public delegate void TargetSetAction(ITarget e);
    public static event TargetSetAction OnTargetSet;

    public float RotationSpeed = 1.0f;

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
        Debug.Log("Target changed to: " + _target.GetGameObject().name);
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

        var targetPosition = _target.GetPosition();
        transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.LookRotation(Vector3.forward, targetPosition - transform.position),
            Time.fixedDeltaTime * RotationSpeed);

        RaycastHit2D hit = Physics2D.Raycast(targetPosition, -Vector2.up);
        if (hit.collider != null)
        {
            Debug.DrawRay(targetPosition, transform.rotation * transform.forward * 4, Color.red);
            _target.DecreaseDurability(_miningLasers.Sum(ml => ml.Dps));
        }
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
