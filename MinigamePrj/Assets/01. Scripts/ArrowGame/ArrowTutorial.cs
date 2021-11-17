using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ArrowTutorial : MonoBehaviour
{
    const float TYPE_SPEED = 2f;

    private int tutorialNum;
    public bool bTutorial = true;

    public Button[] nextBtns;
    public Button skipBtn;
    public GameObject[] tutorialPanels;
    public Text[] chatTexts;
    public Text skipText;
    
    public string[] firstString = 
    {
        "화 살 표 누 르 기\n\n알맞는 화살표를 눌러\n최대한 많은\n웨이브를 통과하라!"
    };
    public string[] secondString = 
    {
        "니가 화살표를\n누르는 시점에서\n제한시간이 줄어들기\n시작해",
        "그리고 웨이브는\n모든 화살표를\n제거하면\n1씩 증가해"
    };
    public string[] thirdString = 
    {
        "5웨이브마다\n귀여운 공룡이\n달려올거야!",
        "하지만...\n니가 3번\n틀릴 때 마다\n앞에있는 공룡이\n하나하나\n날라갈거야",
        "최대한 틀리지 말고\n통과해보도록!\n\n시작하려면\n위에 Play를 눌러!"
    };

    void Awake()
    {
        skipText = skipBtn.GetComponentInChildren<Text>();
    }

    void Start()
    {
        tutorialNum = 0;
        tutorialPanels[0].SetActive(true);
        for(int i = 1; i < tutorialPanels.Length; i++)
        {
            tutorialPanels[i].SetActive(false);
        }
        NextTutorial(0);
    }

    public void NextTutorial(int n)
    {
        tutorialPanels[tutorialNum].SetActive(false);
        tutorialNum = Mathf.Clamp(tutorialNum + n, 0, tutorialPanels.Length - 1);
        tutorialPanels[tutorialNum].SetActive(true);

        for(int i = 0; i < nextBtns.Length; i++) { nextBtns[i].interactable = false; }

        switch (tutorialNum)
        {
            case 0:
                Chat(chatTexts[tutorialNum], firstString, 0, 0);
            break;
            case 1:
                Chat(chatTexts[tutorialNum], secondString, 0, 0);
            break;
            case 2:
                Chat(chatTexts[tutorialNum], thirdString, 0, 0);
            break;
        }
    }

    public void Chat(Text text, string[] strs, int n, float delayTime)
    {
        text.DOText(strs[n], TYPE_SPEED, true, ScrambleMode.Custom, "     ").SetDelay(delayTime).OnComplete(() => {
            n++;
            if(n < strs.Length) { Chat(text, strs, n, 1f); }
            else
            {
                foreach (var item in nextBtns) { item.interactable = true; }
                if(tutorialNum == chatTexts.Length)
                {
                    skipText.text = "Play!";
                }
            }
        });
    }

    public void Skip()
    {
        bTutorial = false;
        this.gameObject.SetActive(false);
    }
}
