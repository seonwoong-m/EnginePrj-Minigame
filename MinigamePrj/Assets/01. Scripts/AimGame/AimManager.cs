using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;
using DG.Tweening;

public class AimManager : MonoBehaviour, ISystem
{
    public  GameObject[] systemPanel;
    public  AudioSource gunShot;

    private Target  hitTarget;
    private TargetSpawner targetSpawner;
    private Vector3 mousePos;
    private Camera  cam;

    public  Text       waveText;
    public  Text       scoreText;
    public  Text       timeText;
    public  Text       highWaveText;
    public  Text       lastScoreText;
    public  Text       readyText;
    public  GameObject readyPanel;
    
    private float maxDis      { get; set; } = 15f;
    private float defaultSec  { get; set; } = 30f;
    private float timeSec     { get; set; }
    private int   wave        { get; set; } = 1;
    private int   highWave    { get; set; } = 1;
    private int   killedDinos { get; set; } = 0;

    public int score = 0;

    public bool isOver = true;
    public bool bPause = false;
    public bool bSetting = false;
    public bool bHelp = false;

    void Awake()
    {
        isOver = true;
        bPause = false;

        cam = FindObjectOfType<Camera>();
        targetSpawner = GetComponent<TargetSpawner>();
        gunShot = GetComponent<AudioSource>();

        //gunShot.clip = GameManager.Instance.effect_Sounds[0];
    }

    void Start()
    {
        timeSec = defaultSec;
        gunShot.volume = GameManager.Instance.EFFECT_VOLUME.normalizedValue;
        gunShot.mute = GameManager.Instance.EFFECT_MUTE.isOn;
        for(int i = 0; i < systemPanel.Length; i++)
        {
            systemPanel[i].SetActive(false);
        }
        readyText.DOFade(0, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && readyPanel.activeSelf)
        {

            readyText.DOFade(0, 0.5f).OnComplete(() => {
                isOver = false;
                readyPanel.SetActive(false);
            });
        }

        if(!isOver)
        {
            if (!bPause)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mousePos = Input.mousePosition;
                    mousePos = cam.ScreenToWorldPoint(mousePos);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, maxDis);
                    Debug.DrawRay(mousePos, transform.forward * 10, Color.red, 0.3f);

                    if (hit)
                    {
                        hitTarget = hit.transform.GetComponent<Target>();
                    }

                    if (hitTarget != null)
                    {
                        hitTarget.gameObject.transform.DOShakePosition(0.2f, 1, 100).OnComplete(() =>
                        {
                            hitTarget.HP--;

                            if(hitTarget.HP <= 0)
                            {
                                score += hitTarget.ownScore;
                                hitTarget.HitTarget();
                                killedDinos++;
                                if (killedDinos % 10 == 0)
                                {
                                    wave++;

                                    if (wave > highWave)
                                    {
                                        highWave++;
                                    }
                                }
                                
                                targetSpawner.spawnPoints[hitTarget.ownRand].SetActive(true);
                            }
                        });
                    }
                }

                timeSec = Mathf.Clamp(timeSec - Time.deltaTime, 0, defaultSec);

                if (timeSec <= 0f)
                {
                    GameOver();
                }
            }
            
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }

        if(bSetting)
        {
            gunShot.volume = GameManager.Instance.EFFECT_VOLUME.normalizedValue;
            gunShot.mute = GameManager.Instance.EFFECT_MUTE.isOn;
        }

        TextUpdate();
    }

    void TextUpdate()
    {
        waveText.text = $"{wave}";
        timeText.text = $"TIME : {timeSec:F2}";
        scoreText.text = $"SCORE : {score}";
    }

    public void Pause()
    {
        bPause = !bPause;
        bSetting = false;
        GameManager.Instance.bSetting = false;
        GameManager.Instance.settingPanel.SetActive(false);
        systemPanel[1].SetActive(bPause);
    }

    public void GameOver()
    {
        isOver = !isOver;
        systemPanel[0].SetActive(isOver);
        highWaveText.text = $"High Wave : {highWave}";
        lastScoreText.text = $"Score : {score}";
    }

    public void Restart()
    {
        isOver = false;
        bPause = false;
        bSetting = false;

        systemPanel[0].SetActive(isOver);
        systemPanel[1].SetActive(bPause);

        wave = 1;
        timeSec = 30f;
        score = 0;

        foreach (var item in targetSpawner.dinos)
        {
            Destroy(item);
        }
        targetSpawner.dinos.Clear();
        foreach (var item in targetSpawner.spawnPoints)
        {
            item.SetActive(true);
        }
    }

    public void Setting()
    {
        bSetting = true;
        GameManager.Instance.Setting();
    }

    public void Help()
    {
        bHelp = !bHelp;
        systemPanel[2].SetActive(true);
    }
}
