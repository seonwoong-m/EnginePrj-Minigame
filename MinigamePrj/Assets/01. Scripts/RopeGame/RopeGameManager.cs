using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RopeGameManager : MonoBehaviour
{
    public static RopeGameManager instance;

    public List<GameObject> spawnList = new List<GameObject>();
    private List<GameObject> blockList = new List<GameObject>();

    public Transform player;
    public Transform hook;
    public GameObject[] systemPanel;

    //UI
    public bool bPause = false;
    private float score = 0;
    public GameObject[] SystemPanel;
    public Text inGameScore;
    public Text panelScore;

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
        inGameScore.text = "Time : " + score.ToString("000000");
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
            count = Random.Range(1, 2);
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

    public void MoveScreen(Transform transform, float moveSpeed)
    {
        if (!bPause)
        {
            transform.position = new Vector2(transform.position.x - moveSpeed, transform.position.y);
        }
    }

    public void GameOver()
    {
        systemPanel[0].SetActive(true);
    }


}
