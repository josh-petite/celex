using UnityEngine;
using Assets.Scripts.Entities;

public class Asteroid : Target
{
    private float _durability;
    private float _maxDurability;

    public GameObject ExplosionPrefab;

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

    public override string Name { get; set; }
    public string ResourcePath { get; set; }

    // Use this for initialization
    void Start()
    {
        var texture = Resources.Load<Texture2D>(ResourcePath);
        var spriteLocation = new Rect(0, 0, texture.width, texture.height);
        var rawSprite = Sprite.Create(texture, spriteLocation, new Vector2(0.5f, 0.5f));

        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = rawSprite;
        spriteRenderer.transform.SetParent(transform, false);

        gameObject.AddComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Durability >= 0.0f) return;
        
        BroadcastOnTargetDeath(this);
        PlayExplosion();
        Destroy(gameObject);
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
