using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class RopeGameManager : MonoBehaviour
{
    public static RopeGameManager instance;

    public bool isMove = false;
    private float moveTime = 30f;

    public List<GameObject> spawnList = new List<GameObject>();
    private List<GameObject> blockList = new List<GameObject>();

    public Transform player;
    public Transform hook;
    public GameObject[] systemPanel;

    private Sequence seq;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < systemPanel.Length; i++)
        {
            systemPanel[i].SetActive(false);
        }

        foreach (var item in spawnList)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject gameObject = Instantiate(item, this.transform);
                gameObject.SetActive(false);
                blockList.Add(gameObject);
            }
        }
        player.GetComponent<RopePlayer>().OnGameOver += GameOver;
        StartCoroutine(StageLoad());

    }

    private void FixedUpdate()
    {
        MoveScreen(this.gameObject.transform);
        MoveScreen(hook.transform);
        foreach (var item in blockList)
        {
            if (item.activeSelf)
            {
                MoveScreen(item.transform);
            }
        }
    }

    IEnumerator StageLoad()
    {
        int random1, random2;
        GameObject temp;
        for (int i = 0; i < blockList.Count(); i++)
        {
            random1 = Random.Range(0, blockList.Count);
            random2 = Random.Range(0, blockList.Count);

            temp = blockList[random1];
            blockList[random1] = blockList[random2];
            blockList[random2] = temp;
        }

        int count = 0;
        float ws = 0;

        while (true)
        {
            count = Random.Range(1,2);
            ws = Random.Range(0.7f, 5f);
            for (int i = 0; i < count; i++)
            {
                GameObject gameObject = blockList.Find(x => !x.activeSelf);
                gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(ws);
        }
    }

    public RopePlayer ReturnPlayer()
    {
        return player.GetComponent<RopePlayer>();
    }

    public void MoveScreen(Transform transform)
    {
        if (isMove)
        {
            float speed = -Mathf.Lerp(0, 20, 1f / moveTime * Time.deltaTime);
            transform.position = new Vector2(speed + transform.position.x, transform.position.y);
        }
    }

    public void GameOver()
    {
        systemPanel[0].SetActive(true);
    }


}
