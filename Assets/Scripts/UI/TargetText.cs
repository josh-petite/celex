using Assets.Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;

public class TargetText : MonoBehaviour
{
    public Text Text;
    
    private void HandleTargetSetEvent(ITarget target)
    {
        if (target == null)
        {
            Text.text = string.Empty;
            return;
        }

        Text.text = target.DisplayName;
    }
    
    // Use this for initialization
    void Start()
    {
        Ship.OnTargetSet += HandleTargetSetEvent;
    }

    void OnEnable()
    {
        Ship.OnTargetSet += HandleTargetSetEvent;
    }

    void OnDisable()
    {
        Ship.OnTargetSet -= HandleTargetSetEvent;
    }
}
