using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropAvoidManager : MonoBehaviour
{
    public DropAvoidPlayerMove player;

    public GameObject avoid;
    private List<GameObject> avoidList = new List<GameObject>();

    public static bool bPause = false;
    public GameObject[] systemPanel;

    private float time;
    public Text timeText;
    public Text panelScoreText;
    public Text panelMaxTimeText;


    private void Start()
    {
        for (int i = 0; i < systemPanel.Length; i++)
        {
            systemPanel[i].SetActive(false);
        }

        for (int i = 0; i < 50; i++)
        {
            GameObject a = Instantiate(avoid, this.gameObject.transform);
            a.SetActive(false);
            avoidList.Add(a);
        }
        StartCoroutine(GameStart());
        player.OnGameOver += GameOver;
    }

    IEnumerator GameStart()
    {
        while (true)
        {
            float waitTime = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(waitTime);
            if (!bPause)
            {
                for (int i = 0; i < Random.Range(1, 5); i++)
                {
                    GameObject gameObject = avoidList.Find(x => !x.activeSelf);
                    if (gameObject == null)
                    {
                        GameObject a = Instantiate(avoid, this.gameObject.transform);
                        a.SetActive(false);
                        avoidList.Add(a);
                        gameObject = a;
                    }
                    gameObject.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        if(!bPause && player.gameObject.activeSelf)
        {
            time += Time.deltaTime;
            timeText.text = "TIME : " + time.ToString("000000");
        }
    }

    public void Pause()
    {
        bPause = !bPause;
        systemPanel[1].SetActive(bPause);
    }

    public void ReStart()
    {
        bPause = false;
        StopAllCoroutines();
        foreach (var item in avoidList)
        {
            if (item.activeSelf) item.SetActive(false);
        }

        player.transform.position = new Vector2(0, -3.55f);//player start position
        player.gameObject.SetActive(true);

        timeText.text = "TIME : ";

        systemPanel[0].SetActive(false);
        systemPanel[1].SetActive(false);

        StartCoroutine(GameStart());
    }

    private void GameOver()
    {
        panelScoreText.text = $"Score : {time.ToString("000000")}";
        //panelMaxTimeText;
        systemPanel[0].SetActive(true);

    }
}
