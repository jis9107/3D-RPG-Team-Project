using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class JBoss : MonoBehaviour
{
    public enum BossState
    {
        A,
        B
    }
    BossState _bossState;

    public int maxHealth = 100;
    public int curHealth = 100;
    public int bossArmor = 15;

    public GameObject missile;
    public Transform missilePort;
    public BoxCollider meleeArea;
    public GameObject enemy;

    float fireDelay;

    bool isPhase;
    bool isChase;
    bool isAttack;
    bool isDead;
    bool isDamage;
    bool isfire;

    Vector3 lookVec;
    Vector3 tauntVec;

    bool isLook;

    Rigidbody rigid;
    Material mat;
    NavMeshAgent nav;
    Transform target;
    BoxCollider boxCollider;
    Animator bossAnim;
    [SerializeField]
    Image bossHPBar;
    [SerializeField]
    Image bossHPBar_back;
    [SerializeField]
    Transform[] enemySpawn;
    [SerializeField]
    GameObject eggEx;

    void Awake()
    {
        _bossState = BossState.A;
        isLook = true;
        rigid = GetComponent<Rigidbody>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        bossAnim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        target = FindObjectOfType<Player>().GetComponent<Transform>();
        
        bossAnim.fireEvents = false;

        nav.isStopped = true;
        Invoke("ChaseStart", 3.7f);
        //StartCoroutine(Think());
    }

    void Update()
    {
        HPUpdate();
        if (isDead)
        {
            StopAllCoroutines();
            return;
        }
        if (nav.enabled && isPhase == false)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }

        // 체력 50% 이하일 때 원거리 공격 패턴으로 변환, 일반 몬스터 소환
        if (curHealth <= 50 && _bossState == BossState.A)
        {
            isPhase = true;
            _bossState = BossState.B;
            bossAnim.SetTrigger("doPhase");
            bossArmor = 12;
            bossAnim.SetBool("isWalk", false);
            isfire = true;
            StartCoroutine(RecallEnemy()); // 일반 몬스터 소환
        }

        if (_bossState == BossState.B)
        {
            Phase2();
        }
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
        if(_bossState == BossState.A)
            Targeting();
    }
    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void Phase2()
    {
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v);
            transform.LookAt(target.position + lookVec);
        }

        fireDelay += Time.deltaTime;
        if(isfire == true)
        {
            StartCoroutine(Think());
        }
    }
    void Targeting()
    {
        float targetRadius = 1f;
        float targetRange = 8f;

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }
    void ChaseStart()
    {
        isChase = true;
        bossAnim.SetBool("isWalk", true);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            if (!isDamage)
            {
                Weapon weapon = other.GetComponent<Weapon>();
                curHealth -= (weapon.damage - bossArmor);
                StartCoroutine(OnDamage());
            }
        }
        if(other.tag == "PlayerSkill")
        {
            if(!isDamage)
            {
                PlayerSkill skill = other.GetComponent<PlayerSkill>();
                curHealth -= (skill.damage - bossArmor);
                StartCoroutine(OnDamage());
                Destroy(other.gameObject);
            }
        }
    }
    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        bossAnim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;

        isChase = true;
        isAttack = false;
        bossAnim.SetBool("isAttack", false);
    }
    IEnumerator Think()
    {
        isfire = false;
        yield return new WaitForSeconds(2f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
            case 2:
                StartCoroutine(MissileShot());
                break;
                // 0, 1, 2 는 미사일 패턴
            case 3:
            case 4:
                StartCoroutine(MissileShot());
                break;
                // 점프 공격 패턴
        }
    }

    IEnumerator MissileShot()
    {
        
        bossAnim.SetTrigger("doAttack");
        yield return new WaitForSeconds(0.7f);
        GameObject instantMissile = Instantiate(missile, missilePort.position, missilePort.rotation);
        yield return new WaitForSeconds(0.3f);
        
        Destroy(instantMissile, 1.5f);
        isfire = true;
    }

    IEnumerator Taunt()
    {
        tauntVec = target.position + lookVec;

        isLook = false;
        nav.isStopped = false;
        boxCollider.enabled = false;
        bossAnim.SetTrigger("doTaunt");

        yield return new WaitForSeconds(1.5f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(1f);
        isLook = true;
        nav.isStopped = true;
        boxCollider.enabled = true;

        StartCoroutine(Think());
    }
    IEnumerator JumpAttack()
    {
        nav.enabled = false;
        isLook = false;
        bossAnim.SetTrigger("doTaunt");
        yield return new WaitForSeconds(Time.deltaTime * 1f);
        rigid.AddForce(Vector3.up * 30);
        isLook = true;
    }
    IEnumerator OnDamage()
    {
        isDamage = true;
        mat.color = Color.red;
        bossAnim.SetTrigger("isHit");
        yield return new WaitForSeconds(0.5f);

        isDamage = false;

        if (curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 11;
            isChase = false;
            nav.enabled = false;
            isDead = true;
            bossAnim.SetTrigger("doDie");
            StopAllCoroutines();
            Destroy(gameObject, 3);
        }
    }
    
    IEnumerator RecallEnemy()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < enemySpawn.Length; i++)
        {
            GameObject instanceEnemy = Instantiate(enemy, enemySpawn[i]);
            GameObject instanceEggEx = Instantiate(eggEx, enemySpawn[i]);
            Destroy(instanceEggEx, 2f);
        }

    }

    void HPUpdate()
    {
        bossHPBar.fillAmount = curHealth / 100f;
        bossHPBar_back.fillAmount = Mathf.Lerp(bossHPBar_back.fillAmount, curHealth / 100f, Time.deltaTime);
    }
}
