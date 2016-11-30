﻿using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    public Material LineMaterial;
    public Transform LaserDestination;
    public float Dps;

    // Use this for initialization
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetVertexCount(2);
        _lineRenderer.SetWidth(0.05f, 0.05f);
        _lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        _lineRenderer.SetColors(Color.green, Color.red);

        Dps = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (LaserDestination == null)
        {
            _lineRenderer.enabled = false;
            return;
        }

        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, LaserDestination.position);
    }
}
