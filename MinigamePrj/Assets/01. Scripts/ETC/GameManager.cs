using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static float defaultVolume = 0.5f;

    private static GameManager instance = null; //싱글톤
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return instance;
        }
    }

    [Header("오디오 관련")]
    public AudioSource audioSource;
    public AudioClip BGM_Sound;
    public AudioClip[] effect_Sounds;
    
    
    [Header("설정 관련")]
    [SerializeField]
    private string[] sceneNames;
    public bool bSetting = false;
    public bool bSceneCheck = false;
    public GameObject settingPanel;
    public Slider BGM_VOLUME;
    public Toggle BGM_MUTE;
    public Slider EFFECT_VOLUME;
    public Toggle EFFECT_MUTE;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM_Sound;
        audioSource.volume = defaultVolume;

        BGM_VOLUME.normalizedValue = defaultVolume;
        BGM_MUTE.isOn = false;
        EFFECT_VOLUME.normalizedValue = defaultVolume;
        EFFECT_MUTE.isOn = false;

        settingPanel.SetActive(false);
    }

    void Update()
    {
        if(bSetting)
        {
            audioSource.volume = BGM_VOLUME.normalizedValue;
            audioSource.mute = BGM_MUTE.isOn;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!bSceneCheck)
            {
                for(int i = 0; i < sceneNames.Length; i++)
                {
                    if(SceneManager.GetActiveScene().name == sceneNames[i])
                    {
                        bSceneCheck = true;
                        Setting();
                    }
                }
            }
            else
            {
                Setting();
            }
        }
    }

    public void Setting()
    {
        bSetting = !bSetting;
        settingPanel.SetActive(bSetting);
    }
}
