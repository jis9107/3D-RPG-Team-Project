using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissile : MonoBehaviour
{
    [SerializeField]
    BoxCollider missileArea;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MissileAttack());
    }

    IEnumerator MissileAttack()
    {
        yield return new WaitForSeconds(0.4f);
        missileArea.enabled = true;
    }

}
