using UnityEngine;
using Assets.Scripts.Entities;

public class Asteroid : Target
{
    private float _durability;
    private float _maxDurability;
    private GameObject _display;
    
    public override float Durability
    {
        get { return _durability; }
        set
        {
            _maxDurability = value;
            _durability = value;
        }
    }

    public override float GetDurabilityPercentage()
    {
        return _durability / _maxDurability;
    }

    public override Transform GetTransform()
    {
        return transform;
    }
    
    public string ResourcePath { get; set; }
    

    // Use this for initialization
    void Start()
    {
        PlayDisplay();
        gameObject.AddComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Durability >= 0.0f) return;
        
        BroadcastOnTargetDeath(this);
        Destroy(_display);
        PlayExplosion();
        Destroy(gameObject);
    }

    void PlayDisplay()
    {
        _display = Instantiate(DisplayPrefab);
        _display.transform.position = transform.position;
    }

    void PlayExplosion()
    {
        var explosion = Instantiate(ExplosionPrefab);
        explosion.transform.position = transform.position;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BroadcastOnTargetClicked(this);
        }
    }

    public override void DecreaseDurability(float amountToDecreaseDurabilityBy)
    {
        _durability -= amountToDecreaseDurabilityBy;
    }
}
