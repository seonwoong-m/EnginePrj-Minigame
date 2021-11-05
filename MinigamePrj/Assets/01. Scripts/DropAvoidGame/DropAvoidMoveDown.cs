using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAvoidMoveDown : MonoBehaviour
{
    private float downSpeed;

    private void OnEnable()
    {
        downSpeed = Random.Range(1f, 4f);
        float scale = Random.Range(0.5f, 2f);
        this.gameObject.transform.localScale = new Vector2(scale, scale);

        float spawnPointX = Random.Range(0f, 1f);
        float spawnPointY = 1.1f;
        Vector2 tr = Camera.main.ViewportToWorldPoint(new Vector3(spawnPointX, spawnPointY));
        gameObject.transform.position = tr;
    }

    private void Update()
    {
        gameObject.transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            gameObject.SetActive(false);
        }
    }
}
