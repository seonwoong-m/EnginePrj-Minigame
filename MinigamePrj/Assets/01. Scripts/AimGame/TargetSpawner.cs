using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSpawner : MonoBehaviour
{
    const float MIN_SEC = 0.2f;
    const float MAX_SEC = 1.0f;

    public  GameObject[] applePrefabs = new GameObject[2];
    public  GameObject   parent;
    public  GameObject   target;
    public  List<GameObject> spawnPoints;
    public  List<GameObject> dinos;

    private AimManager   aimManager;

    public float spawnTime = 0f;
    public float spawnSec = 0f;
    public int   aimScore = 0;

    private int randP;
    private int randSpawn;

    void Awake()
    {
        aimManager = GetComponent<AimManager>();
    }

    void Start()
    {
        //applePrefabs[0].transform.localScale = new Vector3(10f, 10f, 1f);
        spawnSec = Random.Range(MIN_SEC, MAX_SEC);
    }

    private float SetSpawnSec()
    {
        spawnSec = Random.Range(MIN_SEC, MAX_SEC);

        return spawnSec;
    }

    void Update()
    {
        if (!aimManager.isOver && !aimManager.bPause)
        {
            while(true)
            {
                randP = Random.Range(0, spawnPoints.Count);

                if(spawnPoints[randP].activeSelf)
                {
                    break;
                }
                else
                {
                    return;
                }
            }

            spawnTime += Time.deltaTime;

            if (spawnTime >= spawnSec)
            {
                randSpawn = Random.Range(1, 100);

                if (randSpawn <= 50)
                {
                    SpawnTarget(0);
                }
                else if (randSpawn > 50 && randSpawn <= 80)
                {
                    SpawnTarget(1);
                }
                else if (randSpawn > 80 && randSpawn <= 95)
                {
                    SpawnTarget(2);
                }
                else if (randSpawn > 95 && randSpawn <= 100)
                {
                    SpawnTarget(3);
                }

                SetSpawnSec();

                spawnTime = 0f;
            }
        }
    }

    private void SpawnTarget(int n)
    {
        target = Instantiate(applePrefabs[n], spawnPoints[randP].transform.position, Quaternion.identity, parent.transform);
        dinos.Add(target);
        target.GetComponent<Target>().ownRand = randP;
        spawnPoints[randP].SetActive(false);
    }
}
