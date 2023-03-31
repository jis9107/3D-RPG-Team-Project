using UnityEngine;


public class StartToVillage_0 : MonoBehaviour
{
    bool isLoad;
    float interval;

    Animator _anim;

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if ( isLoad )
        {
            interval += Time.deltaTime;
            if( interval > 0.5f)
            {
                LoadingManager_1.LoadScene("Village");
            }
        }
    }

    public void LoadScene()
    {
        isLoad = true;
        _anim = transform.parent.GetComponent<Animator>();
        _anim.SetTrigger("enterStartButton");
    }
}
