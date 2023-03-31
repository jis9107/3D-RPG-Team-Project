using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RecallEnemy : MonoBehaviour
{
    public int damage = 10;
    public int maxHealth;
    public int curHealth;

    int monsterCount;

    bool isChase;
    bool isAttack;
    bool isDead;
    bool isDamage;

    Rigidbody rigid;
    Material mat;
    NavMeshAgent nav;
    Transform target;
    Animator anim;

    public BoxCollider meleeArea;

    private void Awake()
    {

        rigid = GetComponent<Rigidbody>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = FindObjectOfType<Player>().GetComponent<Transform>();
        anim.fireEvents = false;

        Invoke("ChaseStart", 2);
    }

    public void Start()
    {

    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void Targeting()
    {
        float targetRadius = 0.5f;
        float targetRange = 1f;

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }
    void FixedUpdate()
    {
        FreezeVelocity();
        Targeting();
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }
    void Update()
    {
        if (isDead)
        {
            StopAllCoroutines();
            return;
        }
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                curHealth -= weapon.damage;
                StartCoroutine("OnDamage");
            }
        }
        if (other.tag == "PlayerSkill")
        {
            if (!isDamage)
            {
                PlayerSkill skill = other.GetComponent<PlayerSkill>();
                curHealth -= skill.damage;
                StartCoroutine("OnDamage");
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        mat.color = Color.red;
        anim.SetTrigger("isHit");
        yield return new WaitForSeconds(0.5f);

        isDamage = false;

        if (curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            BoxCollider enemyBox = GetComponent<BoxCollider>();
            enemyBox.enabled = false;
            mat.color = Color.gray;
            gameObject.layer = 11;
            isChase = false;
            nav.enabled = false;
            isDead = true;
            anim.SetTrigger("doDie");
            Destroy(gameObject, 3);
            Destroy(this);
        }
    }
}
