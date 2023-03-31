using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainUIController : MonoBehaviour
{
    public enum State
    {
        Start,
        Village,
        Dungeon,
        Boss
    }
    public State _state;

    float time = 0f;
    float F_time = 1f;

    MenuController_1 MenuController;
    Player Player;
    [SerializeField] 
    Texture2D cursorImg;

    public GameObject _minimap;
    public GameObject _inventory;
    public GameObject _menu; 
    public GameObject _pause; 

    public GameObject _player; //플레이어

    public GameObject notice_location_village;
    public GameObject notice_location_dungeon;
    public GameObject notice_location_boss;
    public GameObject _FindBob;
    public GameObject _clearMonster; // 던전 입장 시 혼잣말
    public GameObject _toBossStage;
    public GameObject notice_youDead; // 사망팝업창
    

    bool bPaused;

    [SerializeField]
    GameObject[] uiControl;
    [SerializeField]
    GameObject countText;
    [SerializeField]
    GameObject dungeonMSG;


    //public bool isGameOver = false; //게임종료여부

    private void Awake()
    {
        Cursor.visible = false;

        if (SceneManager.GetActiveScene().name.Contains("Start"))
        {
            Cursor.visible = true;
        }

        else if (SceneManager.GetActiveScene().name.Contains("Village"))
        {
            _state = State.Village;
        }

        else if (SceneManager.GetActiveScene().name.Contains("Dungeon"))
        {
            _state = State.Dungeon;
            Invoke("ClearMonster", 3f);
        }

        else if (SceneManager.GetActiveScene().name.Contains("Boss Stage"))
        {
            _state = State.Boss;
        }
    }



    void Start()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
        StartCoroutine(DelayTime(0.5f));

        if (_state == State.Village)
        {
            Invoke(nameof(FindBob), 3f);
            Destroy(_FindBob, 4.5f);
        }

        else if (_state == State.Dungeon)
        {

        }
    }

    // 씬 로드시 나타나는 장소안내창
    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);

        if(SceneManager.GetActiveScene().name == "Village")
        {
            notice_location_village.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            notice_location_dungeon.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Boss Stage")
        {
            notice_location_boss.SetActive(true);
        }
    }

   
    void Update()
    {
        ControlUI();


        // minimap
        if (Input.GetKeyDown(KeyCode.P))
        {
            _minimap.SetActive(!_minimap.activeSelf);
        }

        // inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            _inventory.SetActive(!_inventory.activeSelf);
        }

        // menu
        if (Input.GetKeyDown(KeyCode.M))
        {
            _menu.SetActive(!_menu.activeSelf);

            if (!_menu.activeSelf)
            {
                MenuController.OnClickGraphicClose();
            }
        }

        // pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pause.SetActive(!_pause.activeSelf);
        } 

        if ( _state == State.Dungeon)
        {
            time += Time.deltaTime;

            if (_clearMonster.activeSelf == true && Input.GetMouseButtonUp(0))
            {
                _clearMonster.SetActive(false);
                time = 0;
                dungeonMSG.SetActive(true);
            }

            if(time > 5 && dungeonMSG)
            {
                dungeonMSG.SetActive(false);
                countText.SetActive(true);
            }

            if (!GameObject.FindWithTag("Hidden").activeSelf)
            {
                _toBossStage.SetActive(true);
            }
        }
    }
    
    public void OnClickMenuClose()
    {
        _menu.SetActive(false);
    }

    public void OnClickPauseClose()
    {
        _pause.SetActive(false);
    }

    public void OnClickNo()
    {
        uiControl[4].SetActive(false);
    }

    public void OnClickYouDead()
    {
        notice_youDead.SetActive(false);
        LoadingManager_1.LoadScene("Start");
    }

    public void OnClickTryAgain()
    {
        notice_youDead.SetActive(false);
        LoadingManager_1.LoadScene("Village");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    void FindBob()
    {
        if (notice_location_village.GetComponentInChildren<Image>().color.a == 0)
        {
            _FindBob.SetActive(true);
        }
    }

    void ClearMonster()
    {
        if (notice_location_dungeon.GetComponentInChildren<Image>().color.a == 0)
        {
            _clearMonster.SetActive(true);
        }
    }

    void OnApplicationPause(bool pause)
    {
        if (_pause.activeSelf)
        {
            bPaused = true;
        }
    }

    public void ControlUI()
    {
        if (uiControl[0].activeSelf == false && uiControl[1].activeSelf == false && uiControl[2].activeSelf == false && uiControl[3].activeSelf == false && uiControl[4].activeSelf == false)
        {
            Cursor.visible = false;
            _player.GetComponent<TPSCharacterController>().enabled = true;
        }
        else if(uiControl[0].activeSelf == true)
        {
            Cursor.visible = true;
            _player.GetComponent<TPSCharacterController>().enabled = true;
        }
        else
        {
            Cursor.visible = true;
            _player.GetComponent<TPSCharacterController>().enabled = false;
        }
    }
}


//// 씬별 Inspector 정리
//[CustomEditor ( typeof (MainUIController))]
//class SceneSettings : Editor
//{
//    SerializedProperty Option_Scene;
//    SerializedProperty S_S_cursor;
//    SerializedProperty S_V_NoticeLocation;
//    SerializedProperty S_V_FindBob;
//    SerializedProperty S_D_NoticeLocation;
//    SerializedProperty S_D_ClearMonster;
//    SerializedProperty S_D_ToBossStage;
//    SerializedProperty S_D_textNext;
//    SerializedProperty S_D_NoticeDead;
//    SerializedProperty S_B_NoticeLocation;
//    SerializedProperty S_B_NoticeDead;

//    private void Awake()
//    {
//        Option_Scene = serializedObject.FindProperty("_state");
//        S_S_cursor = serializedObject.FindProperty("cursorImg");
//        S_V_NoticeLocation = serializedObject.FindProperty("notice_location_village");
//        S_V_FindBob = serializedObject.FindProperty("_FindBob");
//        S_D_NoticeLocation = serializedObject.FindProperty("notice_location_dungeon");
//        S_D_ClearMonster = serializedObject.FindProperty("_clearMonster");
//        S_D_ToBossStage = serializedObject.FindProperty("_toBossStage");
//        S_D_textNext = serializedObject.FindProperty("text_next");
//        S_D_NoticeDead = serializedObject.FindProperty("notice_youDead");
//        S_B_NoticeLocation = serializedObject.FindProperty("notice_location_boss");
//        S_B_NoticeDead = serializedObject.FindProperty("notice_youDead");
//    }

//    /*
//    public override void OnInspectorGUI()
//    {
//        EditorGUILayout.PropertyField(Option_Scene);

//        if((MainUIController.State)Option_Scene.enumValueIndex == MainUIController.State.Start)
//        {
//            EditorGUILayout.PropertyField(S_S_cursor);
//        }

//        else if ((MainUIController.State) Option_Scene.enumValueIndex == MainUIController.State.Village)
//        {
//            EditorGUILayout.PropertyField(S_V_NoticeLocation);
//            EditorGUILayout.PropertyField(S_V_FindBob);
//        }
        
//        else if ((MainUIController.State)Option_Scene.enumValueIndex == MainUIController.State.Dungeon)
//        {
//            EditorGUILayout.PropertyField(S_D_NoticeLocation);
//            EditorGUILayout.PropertyField(S_D_ClearMonster);
//            EditorGUILayout.PropertyField(S_D_ToBossStage);
//            EditorGUILayout.PropertyField(S_D_NoticeDead);

//            if ( S_D_ClearMonster.hasChildren )
//            {
//                EditorGUILayout.PropertyField(S_D_text1);
//                EditorGUILayout.PropertyField(S_D_text2);
//                EditorGUILayout.PropertyField(S_D_textNext);
//            }
//        }

//        else if ((MainUIController.State)Option_Scene.enumValueIndex == MainUIController.State.Boss)
//        {
//            EditorGUILayout.PropertyField ( S_B_NoticeLocation );
//            EditorGUILayout.PropertyField ( S_B_NoticeDead );
//        }

//        //serializedObject.ApplyMpdifiedProperties ();
//    }*/
//}