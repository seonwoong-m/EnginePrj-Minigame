using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class MouseCursor : MonoBehaviour
{
    public  Sprite[]    cursorImages; // 0 : up, 1 : down
    private Vector2     hotSpot;
    private AimManager  aimManager;
    private AudioSource aimGunShot;

    void Start()
    {
        StartCoroutine(MyCursor(cursorImages[0].texture));
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(SceneManager.GetActiveScene().name == "02. AimGame")
            {
                if (aimManager == null)
                {
                    aimManager = FindObjectOfType<AimManager>();
                    aimGunShot = aimManager.gunShot;

                    Shot();
                }
                else
                {
                    Shot();
                }
            }
        }

        if(Input.GetMouseButton(0))
        {
            StopCoroutine(MyCursor(cursorImages[0].texture));
            StartCoroutine(MyCursor(cursorImages[1].texture));
        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopCoroutine(MyCursor(cursorImages[1].texture));
            StartCoroutine(MyCursor(cursorImages[0].texture));
        }
    }

    IEnumerator MyCursor(Texture2D cursor)
    {
        yield return new WaitForEndOfFrame();

        if (cursor != null)
        {
            hotSpot.x = cursor.width / 2;
            hotSpot.y = cursor.height / 2;
            Cursor.SetCursor(cursor, hotSpot, CursorMode.ForceSoftware);
        }
    }

    private void Shot()
    {
        if (!aimManager.bPause && !aimManager.isOver)
        {
            if (!aimGunShot.isPlaying)
            {
                aimGunShot.Play();
            }
            else
            {
                aimGunShot.Stop();
                aimGunShot.Play();
            }
        }
    }
}