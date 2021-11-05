using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float charSpeed { get; set; } = 4.0f;

    void FixedUpdate()
    {
        gameObject.transform.position += Vector3.right * charSpeed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter2D(Collision2D others)
    {
        if(others.transform.CompareTag("Block") || others.transform.CompareTag("Player"))
        this.enabled = false;
    }
}
