using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jweapon : MonoBehaviour
{
    public enum Type { Melee, Range }; // 공격 타입
    public Type type;
    public int damage; // 데미지
    public float rate; // 공격 속도
    public BoxCollider meleeArea; // 공격 범위
    public TrailRenderer trailEffect; // 공격 이펙트

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing()
    {
        // 1
        yield return new WaitForSeconds(0.1f); // 1프레임 대기, 여러 개를 실행하여 시간차 로직 구현가능 WaitForSeconds는 0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    //Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴 - 일반함수
    //Use() 메인루틴 + Swing() 서브루틴 - 코루틴

    
}
