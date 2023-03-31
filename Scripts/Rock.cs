using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; // 바위의 체력

    [SerializeField]
    private float destroyTime; // 파편 제거 시간

    [SerializeField]
    private SphereCollider col; // 구체 콜라이더

    //필요한 게임 오브젝트
    [SerializeField]
    private GameObject go_rock; // 일반 바위
    [SerializeField]
    private GameObject go_debris; // 깨진 바위
    [SerializeField]
    private GameObject go_rock_item_prefab; // 돌맹이 아이템

    //돌맹이 아이템 등장 개수
    [SerializeField]
    private int count;

    public void Mining()
    {
        hp--;
        if(hp <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        col.enabled = false;

        for (int i = 0; i < count; i++)
        {
            Instantiate(go_rock_item_prefab, go_rock.transform.position, Quaternion.identity);

        }


        Destroy(go_rock);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
