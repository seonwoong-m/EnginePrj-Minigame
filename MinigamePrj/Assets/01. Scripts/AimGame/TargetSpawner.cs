using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSpawner : MonoBehaviour
{
    const float MIN_SEC = 0.2f;
    const float MAX_SEC = 1.0f;

    public  List<GameObject> dinoPrefabs;   // 공룡 종류
    public  List<GameObject> spawnPoints;   // 스폰될 위치
    public  List<GameObject> dinos;         // 처치할 공룡 배열
    public  GameObject   parent;
    public  GameObject   target;

    private AimManager   aimManager;

    public  float spawnTime = 0f;
    public  float spawnSec = 0f;
    public  int   aimScore = 0;
    private int   randP;
    private int   randSpawn;

    void Awake()
    {
        aimManager = GetComponent<AimManager>();
    }

    void Start()
    {
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
                if(spawnPoints[randP].activeSelf) { break; }
                else { return; }
            }

            spawnTime += Time.deltaTime;

            if (spawnTime >= spawnSec)
            {
                randSpawn = Random.Range(1, 1000);

                if      (randSpawn <= 400)                     { SpawnTarget(0); } // 40% - 1pt
                else if (randSpawn > 400 && randSpawn <= 600)  { SpawnTarget(1); } // 20% - 2pt
                else if (randSpawn > 600 && randSpawn <= 750)  { SpawnTarget(2); } // 15% - 3pt
                else if (randSpawn > 750 && randSpawn <= 800)  { SpawnTarget(3); } // 5% - 4pt
                else if (randSpawn > 800 && randSpawn <= 900)  { SpawnTarget(4); } // 10% - -2pt
                else if (randSpawn > 900 && randSpawn <= 980)  { SpawnTarget(5); } // 8% - 2hp
                else if (randSpawn > 980 && randSpawn <= 995)  { SpawnTarget(6); } // 1.5% - 3hp
                else if (randSpawn > 995 && randSpawn <= 1000) { SpawnTarget(7); } // 0.5% - 6hp

                SetSpawnSec();

                spawnTime = 0f;
            }
        }
    }

    private void SpawnTarget(int n)
    {
        target = Instantiate(dinoPrefabs[n], spawnPoints[randP].transform.position, Quaternion.identity, parent.transform);
        dinos.Add(target);
        target.GetComponent<Target>().ownRand = randP;
        spawnPoints[randP].SetActive(false);
    }
}
