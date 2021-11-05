using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAvoidManager : MonoBehaviour
{
    public DropAvoidPlayerMove player;

    [Header("Avoid")]
    public GameObject avoid;
    private List<GameObject> avoidList = new List<GameObject>();

    [Header("UI")]
    public GameObject[] systemPanel; // 0:���ӿ���, 1:�Ͻ�����, 

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

            for (int i = 0; i < Random.Range(1, 5); i++)
            {
                GameObject gameObject = avoidList.Find(x => !x.activeSelf);//�������� ��ü ����
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

    private void GameOver()
    {
        systemPanel[0].SetActive(true);
    }
}
