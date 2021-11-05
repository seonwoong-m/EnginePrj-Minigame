using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed = 0.4f;

    private Material backMaterial;

    void Start()
    {
        backMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector2 newOffset = backMaterial.mainTextureOffset;
        newOffset.Set(newOffset.x + (scrollSpeed * Time.deltaTime), 0);
        backMaterial.mainTextureOffset = newOffset;
    }
}
