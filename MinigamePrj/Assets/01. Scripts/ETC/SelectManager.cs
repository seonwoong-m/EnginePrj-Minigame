using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public int selectNum { get; set; }
    public Image game;
    public Text  gameText;
    public Text[] gameNames;
    public string[] gameScenes;
    public Sprite[] gameImage;
    public Dictionary<KeyCode, int> keyDic = new Dictionary<KeyCode, int>
    {
        { KeyCode.RightArrow, 1 },
        { KeyCode.DownArrow,  1 },
        { KeyCode.Space,      0 },
        { KeyCode.LeftArrow, -1 },
        { KeyCode.UpArrow,   -1 }
    };

    private Vector3 defaultPos;
    private LoadSceneBtn loadScene;

    void Awake()
    {
        loadScene = GetComponent<LoadSceneBtn>();
    }

    void Start()
    {
        selectNum = 0;
        ChangeGameImage();
        defaultPos = gameNames[1].rectTransform.position;
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            foreach (var dic in keyDic)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    ChangeSelect(dic.Value);
                }
            }
        }
    }

    public void ChangeSelect(int n)
    {
        if(n == 0)
        {
            loadScene.SceneLoading(gameScenes[selectNum]);
        }
        else
        {
            selectNum = Mathf.Clamp(selectNum + n, 0, gameScenes.Length - 1);
            ChangeGameImage();

            foreach (var item in gameNames)
            {
                if(item == gameNames[selectNum])
                {
                    item.rectTransform.localScale = new Vector3(
                        Mathf.Clamp(item.rectTransform.localScale.x * 1.1f, 1f, 1.1f),
                        Mathf.Clamp(item.rectTransform.localScale.y * 1.1f, 1f, 1.1f),
                        1 );
                }
                else
                {
                    item.rectTransform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    public void ChangeGameImage()
    {
        game.sprite = Resources.Load<Sprite>($"GameImages/GameImage_{selectNum}");
        gameText.text = gameNames[selectNum].text;
    }
}
