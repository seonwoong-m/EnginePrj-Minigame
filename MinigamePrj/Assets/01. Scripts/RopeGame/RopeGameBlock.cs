using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGameBlock : MonoBehaviour
{

    private void OnEnable()//À§Ä¡
    {
        float x = 13f;
        float y = Random.Range(-4f, 4f);
        gameObject.transform.position = new Vector2(x, y);
    }

    private void FixedUpdate()
    {
        RopeGameManager.instance.MoveScreen(transform);
        if (transform.position.x < -13f)
            gameObject.SetActive(false);
    }
}
