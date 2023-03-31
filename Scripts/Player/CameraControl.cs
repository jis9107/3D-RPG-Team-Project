using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl: MonoBehaviour
{
    //public Camera camera;
    //Ray ray;
    //RaycastHit hit;
    [Header("AutoZoom")]
    //public float nowlength;
    public float zoommaxlength;
    float maxlength = 7f;
    float minlength = 2f;
    Ray _ray;
    RaycastHit[] _raycastHits;
    Vector3 pivotDis = new Vector3();
    public Camera lookCamera;
    public GameObject cameraPivot;
    public GameObject cameraPivot2;
    float wheelspeed = 2.0f;

    void Start()
    {

    }


    void Update()
    {
        var zoomdis = lookCamera.transform.localPosition.z;//카메라의 Z 거리
        zoomdis += Input.GetAxis("Mouse ScrollWheel") * wheelspeed;
        if (zoomdis > -2.0f)
            zoomdis = -2.0f;
        if (zoomdis < -zoommaxlength)
            zoomdis = -zoommaxlength;
        lookCamera.transform.localPosition = new Vector3(0, 0, zoomdis);
        //CheckZoom();
    }

    void CheckZoom()
    {
        _ray.origin = cameraPivot.transform.position;//카메라의 위치
        _ray.direction = -cameraPivot2.transform.forward;//카메라의 방향
        Debug.DrawRay(_ray.origin, _ray.direction * maxlength, Color.yellow, 0.1f);//예시로 에디터에서만 보이는 Ray
        _raycastHits = Physics.RaycastAll(_ray, maxlength);//부딪친 모든 정보 가져오기
        zoommaxlength = maxlength;
        if (_raycastHits.Length != 0)//부딪힌 것이 하나라도 있으면
        {
            for (int i = 0; i < _raycastHits.Length; i++)
            {
                var le = Vector3.Distance(cameraPivot.transform.position, _raycastHits[i].point);//부딪힌것과 거리재기
                //print($"le !  : {le}");
                if (zoommaxlength > le)
                {
                    zoommaxlength = le;
                }
                if (zoommaxlength <= minlength)
                {
                    zoommaxlength = minlength;
                    break;
                }
            }
        }
    }
}
