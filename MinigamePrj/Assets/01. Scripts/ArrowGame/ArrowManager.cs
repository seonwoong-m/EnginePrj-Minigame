 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class ArrowManager : MonoBehaviour, ISystem
{

    [HideInInspector]
    public List<Image> endArrL;

    [HideInInspector]                   // 리스트 복사용
    public List<Image> copyImg;
    [HideInInspector]
    public List<int> copyInt;

    public List<GameObject> characters; // 공룡 프리팹
    public List<GameObject> liveChars; // 스폰된 공룡
    public GameObject[] systemPanel; // 0:게임오버, 1:일시정지, 
    public Dictionary<KeyCode, int> keyDic = new Dictionary<KeyCode, int>
    {
        { KeyCode.UpArrow, 0 },
        { KeyCode.DownArrow, 1},
        { KeyCode.RightArrow, 2 },
        { KeyCode.LeftArrow, 3 },
    };

    public SpawnArrow spawnArr;
    public GameObject endArrow;
    public GameObject charParent;
    public Text[] allTexts; // timeText, waveText, highWaveText, scoreText
    public Image centerImg;
    public Toggle BGM_Mute;
    private GameObject character;
    private AudioSource arrowSound;

    public float MAX_TIME   { get; set; } = 30f;
    public float timeSec    { get; set; }
    public int   wave       { get; set; } = 1;
    public int   highWave   { get; set; } = 1;
    public int   scoreCount { get; set; } = 0;
    public int   charCount  { get; set; } = -1;
    
    public bool isOver = false;
    public bool bPause = false;

    void Awake()
    {
        arrowSound = GetComponent<AudioSource>();
        arrowSound.clip = GameManager.Instance.effect_Sounds[1];
    }

    void Start()
    {
        timeSec = MAX_TIME;

        for(int i = 0; i < systemPanel.Length; i++)
        {
            systemPanel[i].SetActive(false);
        }

        centerImg.color = Color.grey;
        TextUpdate();
    }

    void Update()
    {
        if (!isOver)
        {
            if (!bPause)
            {
                if (Input.anyKeyDown)
                {
                    foreach (var dic in keyDic)
                    {
                        if (Input.GetKeyDown(dic.Key))
                        {
                            if (dic.Value <= 7)
                            {
                                ArrowCheck(dic.Value);
                            }
                        }
                    }
                }

                if (timeSec > 0)
                {
                    timeSec = Mathf.Clamp(timeSec - Time.deltaTime, 0, MAX_TIME);
                    TextUpdate();
                }
                else
                {
                    GameOver();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.Setting();
                Pause();
            }
        }
    }

    void ArrowCheck(int n)
    {
        if (spawnArr.arrs[0] == n)
        {
            scoreCount++;
            StartCoroutine(SetColor(Color.blue));


            Debug.Log(spawnArr.arrs[0]);

            endArrL.Add(spawnArr.arrImgs[0]);
            spawnArr.arrImgs[0].transform.SetParent(endArrow.transform);

            spawnArr.arrs.RemoveAt(0);
            spawnArr.arrImgs.RemoveAt(0);

            if (endArrL.Count > 4)
            {
                Destroy(endArrL[0].gameObject);
                endArrL.RemoveAt(0);
            }

            if (spawnArr.arrs.Count == 0)
            {
                SpawnUpdate();
            }
        }
        else
        {
            Debug.Log("삐빅");
            scoreCount = Mathf.Clamp(scoreCount--, 0, int.MaxValue);
            StartCoroutine(SetColor(Color.red));
            ClearEndArr();
            spawnArr.RespawnArrs(copyInt, copyImg);
        }
    }

    void SpawnUpdate()
    {
        Debug.Log("없음");

        wave++;
        if (highWave < wave)
        {
            highWave++;
        }

        ClearEndArr();
        spawnArr.SpawnArrs();

        if (wave % 5 == 0 && charCount < 9)
        {
            charCount++;
            character = Instantiate(characters[charCount % 4], new Vector3(-13, 3f, 0), Quaternion.identity, charParent.transform);
            liveChars.Add(character);
        }
    }

    void TextUpdate()
    {
        allTexts[1].text = $"{wave}";
        allTexts[0].text = $"TIME : {timeSec:F2}";
    }

    void ClearEndArr()
    {
        Transform[] childList = endArrow.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
            endArrL.Clear();
        }
    }

    IEnumerator SetColor(Color color)
    {
        centerImg.color = color;
        yield return new WaitForSeconds(0.3f);
        centerImg.color = Color.grey;
    }

// 여기부터 ISystem 구현
    public void GameOver()
    {
        isOver = !isOver;
        systemPanel[0].SetActive(isOver);
        allTexts[2].text = $"High Wave : {highWave}";
        allTexts[3].text = $"Score : {scoreCount}";
    }

    public void Pause()
    {
        bPause = !bPause;
        systemPanel[1].SetActive(bPause);
    }

    public void Restart()
    {
        isOver = false;
        bPause = false;

        systemPanel[0].SetActive(isOver);
        systemPanel[1].SetActive(bPause);

        foreach (var item in liveChars)
        {
            Destroy(item);
        }
        liveChars.Clear();

        wave = 1;
        scoreCount = 0;
        charCount = -1;
        timeSec = 30.0f;

        //TODO 화살표 전체 초기화
        if(spawnArr.arrImgs != null)
        {
            foreach (var item in spawnArr.arrImgs)
            {
                Destroy(item.gameObject);
            }
        }

        ClearEndArr();
        spawnArr.arrs.Clear();
        spawnArr.arrImgs.Clear();
        
        spawnArr.SpawnArrs();
    }

    public void Setting()
    {
        GameManager.Instance.Setting();
    }
}