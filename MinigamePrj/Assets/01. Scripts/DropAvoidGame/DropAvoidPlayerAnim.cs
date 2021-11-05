using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAvoidPlayerAnim : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetMove(float dir)
    {
        anim.SetFloat("Move", dir);
    }

    public void SetDash()
    {
        anim.SetTrigger("Dash");
    }
}
