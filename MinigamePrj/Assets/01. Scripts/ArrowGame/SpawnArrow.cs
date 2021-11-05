using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpawnArrow : MonoBehaviour
{
    [HideInInspector]
    public List<int> arrs;
    [HideInInspector]
    public List<Image> arrImgs;
    private GridLayoutGroup grid;
    private ArrowManager arrManager;

    public List<Image> arrows;
    private Image arr;

    private int randArr{ get; set; }

    void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
        arrManager = FindObjectOfType<ArrowManager>();
    }

    void Start()
    {
        arrManager.wave = 1;
        SpawnArrs();

        for (int i = 0; i < arrs.Count; i++)
        {
            print(arrs[i]);
            print(arrImgs[i]);
        }
    }

    public void SpawnArrs()
    {
        arrManager.copyInt.Clear();
        arrManager.copyImg.Clear();

        for (int i = 0; i < arrManager.wave; i++)
        {
            randArr = Random.Range(0, 3);
            arr = Instantiate(arrows[randArr], transform);
            arrs.Add(randArr);
            arrImgs.Add(arr);
        }

        arrManager.copyInt = arrs.ToList();
        arrManager.copyImg = arrImgs.ToList();
    }

    public void RespawnArrs(List<int> copyInts, List<Image> copyImgs)
    {
        for(int i = 0; i < arrImgs.Count; i++)
        {
            Destroy(arrImgs[i].gameObject);
        }

        arrs.Clear();
        arrImgs.Clear();

        for(int i = 0; i < copyInts.Count; i++)
        {
            arr = Instantiate(arrows[copyInts[i]], transform);
            arrs.Add(copyInts[i]);
            arrImgs.Add(arr);
        }
    }
}
