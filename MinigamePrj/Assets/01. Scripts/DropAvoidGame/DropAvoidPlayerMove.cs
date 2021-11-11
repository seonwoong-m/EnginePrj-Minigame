using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAvoidPlayerMove : MonoBehaviour
{
    public bool isMove = false;
    public float moveSpeed;
    private float currentMoveSpeed = 2f;


    private bool isDash = false;
    private float dashPower = 10f;
    public float dashTime = 0.2f;

    private float maxHP = 5;
    public float currentHP;


    private Vector3 playerPos;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private DropAvoidPlayerAnim anim;

    public Action OnGameOver;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<DropAvoidPlayerAnim>();
    }

    private void Start()
    {
        currentHP = maxHP;
        moveSpeed = currentMoveSpeed;
    }

    private void Update()
    {
        gameObject.transform.position = new Vector2(Mathf.Clamp(gameObject.transform.position.x, -8.5f, 8.5f), transform.position.y);
        if (DropAvoidManager.bPause)
        {
            rigid.velocity = Vector2.zero;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.5f, 8.5f), transform.position.y, 0);

        if (Input.GetAxis("Jump") > 0 ? true : false && !isDash)
        {
            isDash = true;
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        anim.SetDash();
        Vector2 dir = sprite.flipX ? Vector2.right * -1 : Vector2.right;
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
        rigid.AddForce(dir * dashPower, ForceMode2D.Impulse);


        float time = 0;
        while (isDash)
        {
            time += Time.deltaTime;
            if (time > dashTime)
            {
                isDash = false;
            }
            yield return null;
        }

        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1;

    }

    private void FixedUpdate()
    {
        if (isDash) return;

        if (Input.GetAxis("Horizontal") > 0)
        {
            sprite.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            sprite.flipX = true;
        }
        rigid.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, 0);

        anim.SetMove(Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Avoid")
        {
            gameObject.SetActive(false);
            OnGameOver();
            isDash = false;
        }
    }
}
