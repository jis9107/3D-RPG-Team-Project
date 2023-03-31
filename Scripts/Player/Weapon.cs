using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public Transform skillPort;
    public Transform skillPort_2;
    public GameObject skill;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StartCoroutine(Swing());
            //StartCoroutine("Swing");
        }
    }


    public void UseSkill()
    {
        if(type == Type.Melee)
        {
            StartCoroutine(Skill());
        }
    }


    IEnumerator Skill()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject instantSkill = Instantiate(skill, skillPort.position, skillPort.rotation);

        yield return new WaitForSeconds(0.4f);
        instantSkill = Instantiate(skill, skillPort_2.position, skillPort_2.rotation);

        yield return new WaitForSeconds(0.4f);
        instantSkill = Instantiate(skill, skillPort.position, skillPort.rotation);
    }


    IEnumerator Swing()
    {

        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        //trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        //trailEffect.enabled = false;
    }


}
