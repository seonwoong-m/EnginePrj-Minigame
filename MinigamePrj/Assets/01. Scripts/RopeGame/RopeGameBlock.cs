using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGameBlock : MonoBehaviour
{
    public bool isStatic = true;
    public float moveSpeed = 0;

    private void OnEnable()//À§Ä¡
    {
        if (isStatic)
        {
            moveSpeed = Mathf.Lerp(0f, 30f, Random.Range(0.5f, 1f)) * 0.001f;
            float x = 13f;
            float y = Random.Range(-4f, 4f);
            gameObject.transform.position = new Vector2(x, y);
        }
    }

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
            RopeGameManager.instance.MoveScreen(gameObject.transform, moveSpeed);
        if (transform.position.x < -13f)
            gameObject.SetActive(false);
    }
}
