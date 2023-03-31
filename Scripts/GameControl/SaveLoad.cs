using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    // 캐릭터 위치 및 방향 
    //public Vector3 playerPos;
    //public Vector3 playerRot;
    public string userMoney; // 플레이어가 소지한 돈
    //public float curHP; // 플레이어 HP

    // 인벤토리 아이템 저장
    public List<int> invenArrayNumber = new List<int>(); // 인벤 슬롯 넘버
    public List<string> invenItemName = new List<string>(); // 아이템 이름
    public List<int> invenItemNumber = new List<int>(); // 아이템 개수

    // 퀵 슬롯 아이템 저장
    public List<int> quickArrayNumber = new List<int>(); // 인벤 슬롯 넘버
    public List<string> quickItemName = new List<string>(); // 아이템 이름
    public List<int> quickItemNumber = new List<int>(); // 아이템 개수

    // 캐릭터 스테이터스 창
}
public class SaveLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    Inventory theInven;
    QuickSlot theQuickSlot;
    StatusController statusController;

    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }
        
    }

    // 저장
    public void SaveData()
    {
        ArrayInitialization();

        theInven = FindObjectOfType<Inventory>();
        theQuickSlot = FindObjectOfType<QuickSlot>();
        saveData.userMoney = theInven.moneyText.text;
        //statusController = FindObjectOfType<StatusController>();
        //saveData.curHP = statusController.currentHp;
        
        Slot[] slots = theInven.GetSlots();
        Slot[] loadQuickSlots = theQuickSlot.GetUseSlots();

        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                saveData.invenArrayNumber.Add(i);
                saveData.invenItemName.Add(slots[i].item.itemName);
                saveData.invenItemNumber.Add(slots[i].itemCount);
            }
        }
        for (int i = 0; i < loadQuickSlots.Length; i++)
        {
            if (loadQuickSlots[i].item != null)
            {
                saveData.quickArrayNumber.Add(i);
                saveData.quickItemName.Add(loadQuickSlots[i].item.itemName);
                saveData.quickItemNumber.Add(loadQuickSlots[i].itemCount);
            }
        }

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);
        Debug.Log(json);

    }
    public void ArrayInitialization()
    {
        saveData.invenArrayNumber.Clear();
        saveData.invenItemName.Clear();
        saveData.invenItemNumber.Clear();
        saveData.quickArrayNumber.Clear();
        saveData.quickItemName.Clear();
        saveData.quickItemNumber.Clear();
    }

    // 불러오기
    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson); // json데이터를 savedata에 대입

            theInven = FindObjectOfType<Inventory>();
            theQuickSlot = FindObjectOfType<QuickSlot>();
            theInven.moneyText.text = saveData.userMoney;

            for (int i = 0; i < saveData.invenItemName.Count; i++)
            {
                theInven.LoadToInven(saveData.invenArrayNumber[i], saveData.invenItemName[i], saveData.invenItemNumber[i]);
            }
            for (int i = 0; i < saveData.quickItemName.Count; i++)
            {
                theQuickSlot.LoadToQuick(saveData.quickArrayNumber[i], saveData.quickItemName[i], saveData.quickItemNumber[i]);
            }
        }
        else
            Debug.Log("세이브 파일이 없습니다.");
    }
}
