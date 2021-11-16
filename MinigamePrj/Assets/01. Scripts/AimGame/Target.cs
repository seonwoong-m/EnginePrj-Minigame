using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Target : MonoBehaviour
{
    private TargetSpawner targetSpawner;
    private ParticleSystem bombEffect;

    public GameObject hpBar;

    public float defaultSize  { get; set; }
    public int   ownScore = 0;
    public int   ownRand;
    
    public int HP { get; set; }
    public int hp;

    public bool bKill = false;

    void Awake()
    {
        targetSpawner = FindObjectOfType<TargetSpawner>();
        bombEffect = GetComponentInChildren<ParticleSystem>();
        if(bombEffect != null)
        {
            Debug.Log("bomb");
        }
    }

    void Start()
    {
        defaultSize = Random.Range(7f, 9f);
        gameObject.transform.localScale = new Vector3(defaultSize, defaultSize, 1);
        HP = hp;

        if(ownScore < 0)
        {
            StartCoroutine(KillMinus());
        }
    }

    public void HitTarget()
    {
        if(ownScore > 0)
        {
            targetSpawner.aimScore += ownScore;
        }

        Destroy(gameObject); // 풀링하기 전
    }

    IEnumerator KillMinus()
    {
        yield return new WaitForSeconds(1f);
        HitTarget();
    }
}
