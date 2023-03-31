using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MinimapController : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCamera;
    [SerializeField]
    private TextMeshProUGUI textMapName;

    void Awake()
    {
        textMapName.text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
