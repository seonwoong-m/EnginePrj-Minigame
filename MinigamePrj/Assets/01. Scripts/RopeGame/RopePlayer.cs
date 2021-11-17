using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePlayer : MonoBehaviour
{
    private Rigidbody2D rigid;

    public float moveSpeed = 0;
    public GameObject hook;
    public LayerMask layer;
    private Vector2 mouseinput;
    private LineRenderer ropeLine;


    public int boostCount = 3;
    private float boostPower = 5f;
    private Vector2 beforePosition;


    public event Action OnGameOver;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ropeLine = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        beforePosition = transform.position;
        ropeLine.SetPosition(1, hook.transform.position);
    }

    private void Update()
    {
        

        if (Input.GetButtonDown("Jump"))
        {
            Boost();
        }


        if (Input.GetMouseButtonDown(0))
        {
            ShootRope();
        }
        ropeLine.SetPosition(0, transform.position);
        ropeLine.SetPosition(1, hook.transform.position);


        mouseinput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (hook.transform.position.x < -10) 
        {
            GameOver();
        }
    }

    private void FixedUpdate()
    {
        beforePosition = transform.position;
        RopeGameManager.instance.MoveScreen(gameObject.transform, moveSpeed);
        RopeGameManager.instance.MoveScreen(hook.transform, moveSpeed);
    }

    private void ShootRope()
    {
        Vector2 dir = mouseinput - (Vector2)transform.position;
        RaycastHit2D hookPoint = Physics2D.Raycast(transform.position, dir.normalized, float.MaxValue, layer);

        if (hookPoint.collider != null)
        {
            hook.transform.position = hookPoint.point;
            ropeLine.SetPosition(1, hookPoint.point);

            RopeGameBlock rp = hookPoint.collider.gameObject.GetComponent<RopeGameBlock>();
            moveSpeed = rp.moveSpeed;
        }
    }

    private void Boost()
    {
        if (boostCount < 1) return;
        boostCount--;
        Vector2 dir = (Vector2)transform.position - beforePosition;
        rigid.AddForce(dir.normalized * boostPower, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            boostCount++;
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            gameObject.SetActive(false);
            RopeGameManager.instance.GameOver();
        }

    }

    public void bPause(bool pause)
    {
        if(pause)
        {

        }
        else
        {

        }
    }

    private void GameOver()
    {
        OnGameOver();
    }
}
