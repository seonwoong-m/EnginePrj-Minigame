using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Target : MonoBehaviour
{
    //private bool isEnable = false;  
    private Transform appleScale;
    private Vector3 targetScale;
    private TargetSpawner targetSpawner;

    //public float smallerSpeed { get; set; } = 5f;
    public float defaultSize  { get; set; }
    public int   ownScore = 0;
    public int   ownRand;
    
    public int HP { get; set; }
    public int hp;

    void Awake()
    {
        appleScale = GetComponent<Transform>();
        targetSpawner = FindObjectOfType<TargetSpawner>();
    }

    void Start()
    {
        defaultSize = Random.Range(7f, 9f);
        gameObject.transform.localScale = new Vector3(defaultSize, defaultSize, 1);
        targetScale = appleScale.localScale;
        HP = hp;
    }

    void FixedUpdate()
    {
            // targetScale = new Vector3
            // (
            //     targetScale.x - smallerSpeed * Time.fixedDeltaTime,
            //     targetScale.y - smallerSpeed * Time.fixedDeltaTime,
            //     targetScale.z
            // );

            //transform.localScale = targetScale;

            // if(targetScale.x <= 0)
            // {
            //     Destroy(gameObject);
            // }
    }

    public void HitTarget()
    {
        targetSpawner.aimScore += ownScore;
        Destroy(gameObject); // 풀링하기 전
    }
}
