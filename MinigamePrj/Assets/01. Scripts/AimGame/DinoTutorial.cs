using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DinoTutorial : MonoBehaviour
{
    const float TYPE_SPEED = 2f;

    private int tutorialNum;
    public bool bTutorial = true;

    public Button[] nextBtns;
    public Button skipBtn;
    public GameObject[] tutorialPanels;
    public Text[] chatTexts;
    public Text skipText;
    
    private string[] firstString = 
    {
        "공 룡 잡 기 게 임\n등장하는 공룡들을\n클릭하여 잡아라!"
    };

    private string[] secondString = 
    {
        "공룡의 종류는\n총 8가지...",
        "Hp는\n클릭해야할\n횟수를...",
        "Pt는 잡을 시\n주어지는\n점수를 뜻해..!",
        "최고점수 90점마다 처음 주어지는 시간이 증가해",
        "플레이를 하려면\n위에 Play! 버튼을\n눌러!"
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

    // public void NextTutorial(int n)
    // {
    //     tutorialPanels[tutorialNum].SetActive(false);
    //     tutorialNum = Mathf.Clamp(tutorialNum + n, 0, tutorialPanels.Length - 1);
    //     tutorialPanels[tutorialNum].SetActive(true);

    //     for(int i = 0; i < nextBtns.Length; i++) { nextBtns[i].interactable = false; }
    //     Chat(chatTexts[tutorialNum]);
    // }

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
        }
    }

    // public void Chat(Text text)
    // {
    //     switch(tutorialNum)
    //     {
    //         case 0:
    //             text.DOText(firstString, TYPE_SPEED, true, ScrambleMode.Custom, "     ").OnComplete(() => { 
    //                 for(int i = 0; i < nextBtns.Length; i++) { nextBtns[i].interactable = true; 
    //                 }});
    //         break;
    //         case 1:
    //             text.DOText(secondString[0], TYPE_SPEED, true, ScrambleMode.Custom, "     ").OnComplete(() => {
    //                 text.DOText(secondString[1], TYPE_SPEED, true, ScrambleMode.Custom, "     ").SetDelay(1f).OnComplete(() => {
    //                     text.DOText(secondString[2], TYPE_SPEED, true, ScrambleMode.Custom, "     ").SetDelay(1f).OnComplete(() => { 
    //                         skipText.text = "Play!";
    //                         for(int i = 0; i < nextBtns.Length; i++) { nextBtns[i].interactable = true; 
    //                     }});
    //                 });
    //             });
    //         break;
    //     }
    // }

    public void Chat(Text text, string[] strs, int n, float delayTime)
    {
        text.DOText(strs[n], TYPE_SPEED, true, ScrambleMode.Custom, "     ").SetDelay(delayTime).OnComplete(() => {
            n++;
            if(n < strs.Length) { Chat(text, strs, n, 1f); }
            else
            {
                if(strs != secondString)
                foreach (var item in nextBtns) { item.interactable = true; }
                
                if(tutorialNum == chatTexts.Length - 1)
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
