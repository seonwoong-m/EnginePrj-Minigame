using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAvoidPlayerMove : MonoBehaviour
{
    [Header("�̵�")]
    public float moveSpeed;
    private float currentMoveSpeed = 2f;

    [Header("�뽬")]
    private bool isDash = false;
    private float dashPower = 10f;
    public float dashTime = 0.2f;

    [Header("ü��")]
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
        playerPos = Camera.main.WorldToViewportPoint(transform.position);

        playerPos.x = Mathf.Clamp(playerPos.x, 0f, 1f);
        playerPos.y = Mathf.Clamp(playerPos.y, 0f, 1f);

        if (Input.GetAxis("Jump") > 0 ? true : false && !isDash)
        {
            isDash = true;
            StartCoroutine(Dash());
        }

        transform.position = Camera.main.ViewportToWorldPoint(playerPos);
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
        if (isDash) return;//Dash���̸� return 


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
            Destroy(this.gameObject);
            OnGameOver();
        }


    }
}
