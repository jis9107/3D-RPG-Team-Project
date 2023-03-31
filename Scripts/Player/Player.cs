using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using TMPro;



public class Player : MonoBehaviour
{
    public enum State
    {
        Village,
        Dungeon,
        Boss
    }
    public State _state;
    public float hp;

    private float dist;
    float interval;

    public bool isdead;
    bool isDamage; // 플레이어 무적시간
    
    public GameObject player;
    public GameObject _store;

    Animator anim;
    StatusController thestatusController;
    Store store;
    UI ui;
    Weapon wp;
    GameControl _gameControl;

    //bool isFindBoss;
    //bool isBossActive;

    public TextMeshProUGUI ConNPCName1;
    public TextMeshProUGUI ConNPCName2;

    public GameObject _FindBob;
    public GameObject _DungeonEntry;
    public GameObject _FakeEntry;
    public GameObject _BobCon;    // NPC-Bob 대화창
    public GameObject _SandyCon;  // NPC-Sandy 대화창
    public GameObject _ShopCon;
    public GameObject _SaveAndBoss;
    public GameObject _bossAtmosphere;
    public GameObject _bossNotice;
    public GameObject _bossReady;
    public GameObject _bossHere;
    public GameObject _win;

    public GameObject moveToV;

    

    private void Awake()
    {
        Cursor.visible = false;

        if (SceneManager.GetActiveScene().name.Contains("Village"))
        {
            _state = State.Village;
            //thestatusController.currentHp = 100;
        }

        else if (SceneManager.GetActiveScene().name.Contains("Dungeon"))
        {
            _state = State.Dungeon;
        }

        else if (SceneManager.GetActiveScene().name.Contains("Boss Stage"))
        {
            _state = State.Boss;
        }
    }


    void Start()
    {
        //모든 씬
        _gameControl = FindObjectOfType<GameControl>();
        anim = GameObject.Find("Knight_Black").GetComponent<Animator>();
        store = FindObjectOfType<Store>();
        thestatusController = FindObjectOfType<StatusController>();
        


        //if (_state == State.Dungeon)
        //{
        //    hit = FindObjectOfType<Enemy>();
        //    wp = GameObject.Find("Sword").GetComponent<Weapon>();
        //}
        //else if (_state == State.Boss)
        //{
        //    hit = FindObjectOfType<Enemy>();
        //    wp = GameObject.Find("Sword").GetComponent<Weapon>();
        //    //_hp = GameObject.Find("Boss").GetComponent<BossHP>();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(hp > 0)
        {
            // 몬스터 공격 함수
            if (other.gameObject.tag == "EnermyAttack")
            {
                if (!isDamage)
                {
                    thestatusController.DecreaseHP(10);
                    StartCoroutine(OnDamage());
                }
            }

            if (other.gameObject.tag == "Missile")
            {
                if (!isDamage)
                {
                    thestatusController.DecreaseHP(15);
                    StartCoroutine(OnDamage());
                }
            }
        }


        if (_state == State.Village)
        {
            if (other.gameObject.CompareTag("ToDungeon"))
            {
                _DungeonEntry.SetActive(true);
                //Cursor.visible = true;
            }

            if (other.gameObject.CompareTag("ToDungeonFake"))
            {
                _FakeEntry.SetActive(true);
            }

            // NPC 트리거에 닿았을 때 열리는 대화창
            if (other.gameObject.layer == 10 && other.name == "Bob")
            {
                _BobCon.SetActive(true);
                ConNPCName1.text = other.name;
            }
            if (other.gameObject.layer == 10 && other.name == "Sandy")
            {
                _SandyCon.SetActive(true);
                ConNPCName2.text = other.name;
            }

            if (other.gameObject.tag == "Shop")
            {
                store.OpenStore();
            }
        }
        if (other.gameObject.tag == "ToBoss")
        {
            EnterToBossStage();
        }

        if (_state == State.Boss)
        {
            if (other.gameObject.tag == "BossTrigger")
            {
                if (other.gameObject.name == "Sense Trigger")
                {
                    _bossAtmosphere.SetActive(true);
                }
                else if (other.gameObject.name == "Block Trigger")
                {
                    _bossNotice.SetActive(true);
                }
                else if (other.gameObject.name == "Ready Trigger")
                {
                    _bossReady.SetActive(true);

                    if (_bossReady.activeSelf)
                    {
                        Destroy(_bossReady, 1.4f);
                        Invoke("BossActive", 3f);
                    }
                    else
                        return;
                }
            }

            if (other.gameObject.name == "moveSpot")
            {

                EnterToVillage();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(_state == State.Village)
        {
            if (other.gameObject.CompareTag("Shop"))
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _ShopCon.SetActive(false);
                    _store.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_state == State.Village)
        {
            if (other.gameObject.tag == "ToDungeon")
            {
                _DungeonEntry.SetActive(false);
                //Cursor.visible = false;
            }

                if (other.gameObject.tag == "ToDungeonFake")
            {
                _FakeEntry.SetActive(false);
            }

            // NPC 트리거에 떨어졌을 때 닫히는 대화창
            if (other.gameObject.layer == 10 && other.name == "Bob")
            {
                _BobCon.SetActive(false);
            }
            if (other.gameObject.layer == 10 && other.name == "Sandy")
            {
                _SandyCon.SetActive(false);
            }
            if (other.gameObject.tag == "Shop")
            {
                _ShopCon.SetActive(false);
                store.CloseStore();
            }
        }
            
        if (_state == State.Dungeon)
        {
        
        }

        if (_state == State.Boss)
        {
            if (other.gameObject.CompareTag("BossTrigger"))
            {
                if (other.gameObject.name == "Sense Trigger")
                {
                    _bossAtmosphere.SetActive(false);
                }
                else if (other.gameObject.name == "Block Trigger")
                {
                    _bossNotice.SetActive(false);
                    other.GetComponent<BoxCollider>().isTrigger = (false);
                }
               
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    void Update()
    {
        hp = thestatusController.currentHp;
        //if (_state == State.Village)
        //{
        //    //dist = Vector3.Distance(player.transform.position, npc.transform.position);
        //}
        //if (_state == State.Dungeon)
        //{
        //    if (!monster.activeInHierarchy)  //던전스테이지에서 몬스터 20마리를 잡았을 때로 수정하기 > 조건부 설정
        //    {
        //        OpenSpaceToBossStage();
        //    }
        //}

        //if (_state == State.Boss)
        //{
        //    if (!boss.gameObject)
        //    {
        //        Invoke(nameof(Win), 1f);
        //        if (_win)
        //        {
        //            moveToV.SetActive(true);
        //        }
        //    }
        //}
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        anim.SetTrigger("isHit");
        yield return new WaitForSeconds(1f); // 맞았을 때 무적타임 1초

        isDamage = false;
    }
    public void EnterToDungeon()  // 빌리지 -> 던전
    {

        _gameControl._state = GameControl.State.Import;
        _gameControl.theSaveLoad.SaveData();
        Debug.Log("빌리지 -> 던전 저장 완료");
        SceneManager.LoadScene("Dungeon");
    }

    public void EnterToBossStage()     // 던전 -> 보스
    {
        _gameControl._state = GameControl.State.Import;
        _gameControl.theSaveLoad.SaveData();
        Debug.Log("던전 -> 보스 저장 완료");
        SceneManager.LoadScene("Boss Stage");
    }
    public void EnterToVillage() // 보스 -> 마을
    {
        _gameControl._state = GameControl.State.Import;
        _gameControl.theSaveLoad.SaveData();
        SceneManager.LoadScene(1);
    }


    public void OpenSpaceToBossStage()     // 던전맵에서 보스맵으로 이동할 수 있는 히든공간 오픈
    {
        GameObject.FindWithTag("Hidden").SetActive(false);
    }


    //void BossActive()     // 보스 등장
    //{
    //    boss.SetActive(true);
    //    _bossHere.SetActive(true);
    //    Destroy(_bossHere, 1.5f);
    //}

    void Win()
    {
        _win.SetActive(true);
        Destroy(_win, 2f);
    }
}


//// 씬별 Inspector 정리
//[CustomEditor ( typeof ( Player ))]
//class Settings : Editor
//{
//    SerializedProperty Option_Scene;
//    SerializedProperty S_V_NPCName1;
//    SerializedProperty S_V_NPCName2;
//    SerializedProperty S_V_FindBob;
//    SerializedProperty S_V_ToDungeon;
//    SerializedProperty S_V_FakeEntry;
//    SerializedProperty S_V_BobCon;
//    SerializedProperty S_V_SandyCon;
//    SerializedProperty S_V_ShopCon;
//    SerializedProperty S_B_Atmosphere;
//    SerializedProperty S_B_Notice;
//    SerializedProperty S_B_Ready;
//    SerializedProperty S_B_Here;
//    SerializedProperty S_B_Win;


//    private void Awake()
//    {
//        Option_Scene = serializedObject.FindProperty("_state");
//        S_V_NPCName1 = serializedObject.FindProperty("ConNPCName1");
//        S_V_NPCName2 = serializedObject.FindProperty("ConNPCName2");
//        S_V_FindBob = serializedObject.FindProperty("_FindBob");
//        S_V_ToDungeon = serializedObject.FindProperty("_DungeonEntry");
//        S_V_FakeEntry = serializedObject.FindProperty("_FakeEntry");
//        S_V_BobCon = serializedObject.FindProperty("_BobCon");
//        S_V_SandyCon = serializedObject.FindProperty("_SandyCon");
//        S_V_ShopCon = serializedObject.FindProperty("_ShopCon");
//        S_B_Atmosphere = serializedObject.FindProperty("_bossAtmosphere");
//        S_B_Notice = serializedObject.FindProperty("_bossNotice");
//        S_B_Ready = serializedObject.FindProperty("_bossReady");
//        S_B_Here = serializedObject.FindProperty("_bossHere");
//        S_B_Win = serializedObject.FindProperty("_win");
//    }
    
//    /*
//    public override void OnInspectorGUI()
//    {
//        EditorGUILayout.PropertyField(Option_Scene);

//        if ((MainUIController.State)Option_Scene.enumValueIndex == MainUIController.State.Village)
//        {
//            EditorGUILayout.PropertyField(S_V_NPCName1);
//            EditorGUILayout.PropertyField(S_V_NPCName2);
//            EditorGUILayout.PropertyField(S_V_FindBob);
//            EditorGUILayout.PropertyField(S_V_ToDungeon);
//            EditorGUILayout.PropertyField(S_V_FakeEntry);
//            EditorGUILayout.PropertyField(S_V_BobCon);
//            EditorGUILayout.PropertyField(S_V_SandyCon);
//            EditorGUILayout.PropertyField(S_V_ShopCon);
//        }

//        else if ((MainUIController.State)Option_Scene.enumValueIndex == MainUIController.State.Dungeon)
//        {
        
//        }

//        else if ((MainUIController.State)Option_Scene.enumValueIndex == MainUIController.State.Boss)
//        {
//            EditorGUILayout.PropertyField(S_B_Atmosphere);
//            EditorGUILayout.PropertyField(S_B_Notice);
//            EditorGUILayout.PropertyField(S_B_Ready);
//            EditorGUILayout.PropertyField(S_B_Here);
//            EditorGUILayout.PropertyField(S_B_Win);

//        }
//    }
//    */
//}
