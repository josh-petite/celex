using System;
using Assets.Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
    private ITarget _currentTarget;

    public Slider LifebarSlider;
    
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
        Ship.OnTargetSet += HandleTargetSetEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTarget == null || LifebarSlider == null) return;
        LifebarSlider.value = _currentTarget.GetDurabilityPercentage();
    }

    void OnEnable()
    {
        Ship.OnTargetSet += HandleTargetSetEvent;
    }

    void OnDisable()
    {
        Ship.OnTargetSet -= HandleTargetSetEvent;
    }

    private void HandleTargetSetEvent(ITarget e)
    {
        _currentTarget = e;
        gameObject.SetActive(e != null);
    }
}
