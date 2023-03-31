using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    Animator animator;
    Player _player;
    UI ui;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.FindWithTag("Enemy").GetComponent<Animator>();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        ui = GameObject.Find("Fluid").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.32f
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.45f)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _player.hp -= 0.1f;
            ui.SetValue(_player.hp);
        }
    }
}
