using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Material _material;
    // Start is called before the first frame update
    void Start()
    {
        _material.SetFloat("_FillLevel", 1.0f);
    }
    public void SetValue(float value)
    {
        _material.SetFloat("_FillLevel", value);
    }
}
