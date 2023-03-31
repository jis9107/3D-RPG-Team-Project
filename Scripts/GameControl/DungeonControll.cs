using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonControll : MonoBehaviour
{
    public int monsterCount;

    [SerializeField]
    Text monsterText;
    [SerializeField]
    GameObject text;
    [SerializeField]
    GameObject toBossStage;

    // Start is called before the first frame update
    void Start()
    {
        monsterCount = int.Parse(monsterText.text);
    }
    private void Update()
    {
       if(monsterCount == 0)
        {
            text.SetActive(false);
            monsterText.text = "보스 던전 입구를 찾아서 이동하세요";
            toBossStage.SetActive(true);
        }
    }

    public void countUpdate()
    {
        monsterCount = monsterCount -1;
        monsterText.text = monsterCount.ToString();
    }


}
