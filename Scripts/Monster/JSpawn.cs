using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    Transform target;
    public GameObject enemy;
    public GameObject eggEx;

    bool isSpawn;

    float distance;
    // Start is called before the first frame update
    void Start()
    {
        isSpawn = true;
        target = FindObjectOfType<Player>().GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position, this.transform.position) < 4 && isSpawn)
        {
            isSpawn = false;
            eggEx.SetActive(true);
            GameObject instantenemy = Instantiate(enemy, spawnPoint);
            Destroy(this.gameObject, 1f);
        }
    }
}
