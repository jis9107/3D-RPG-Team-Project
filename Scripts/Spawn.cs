using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    Transform target;
    public int maxCount; //최대 몬스터 수
    public int enemyCount; //현재 몬스터 수
    public float spawnTime; //몬스터 소환 후 다음 몬스터 소환까지의 스폰 간격
    public float curTime;
    public Transform[] spawnPoints;
    public bool[] isSpawn;
    public GameObject enemy;

    public static Spawn _instance;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        isSpawn = new bool[spawnPoints.Length];
        for (int i = 0; i < isSpawn.Length; i++)
        {
            isSpawn[i] = false;
        }
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (curTime >= spawnTime && enemyCount < maxCount)
        {
            int x = Random.Range(0, spawnPoints.Length);
            if(!isSpawn[x] && distance <= 15)
            {
                SpawnEnemy(x);
            }
        }
        curTime += Time.deltaTime;
    }
    public void SpawnEnemy(int ranNum)
    {
        curTime = 0;
        enemyCount++;
        Instantiate(enemy, spawnPoints[ranNum]); 
        isSpawn[ranNum] = true;
    }
}
