using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPlayer : MonoBehaviour
{
    public int health;

    //플레이어 이동
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown; //걷기
    bool jDown; //점프
    bool fDown; //공격

    bool isJump;
    bool isRoll;
    bool isFireReady = true;
    bool isDamage; // 플레이어 무적타임 

    Vector3 moveVec;
    Vector3 rollVec;

    Rigidbody rigid;
    Animator anim;
    Jweapon weapon;

    float fireDelay;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        weapon = GetComponentInChildren<Jweapon>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Attack();
        Roll();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetKeyDown(KeyCode.Mouse0);        
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; //normalized를 통해 대각선으로 갈 때 속도를 1로 맞춰준다.

        if (isRoll)
            moveVec = rollVec;

        if (!isFireReady)
            moveVec = Vector3.zero;
         
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime; // 아래와 같은 조건문을 삼항연산자로 변환

        //if(wDown)
        //    transform.position += moveVec * speed * 0.3f * Time.deltaTime;
        //else
        //    transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 8, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Attack()
    {
        fireDelay += Time.deltaTime;
        isFireReady = weapon.rate < fireDelay;

        if(fDown && isFireReady && !isRoll && !isJump)
        {
            weapon.Use();
            anim.SetTrigger("doSwing");
            fireDelay = 0;
        }
    }
    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (moveVec != Vector3.zero && !isJump && !isRoll)
            {
                rollVec = moveVec;
                speed *= 2;
                anim.SetTrigger("doRoll");
                isRoll = true;

                Invoke("RollOut", 0.4f);
            }
        }
    }

    void RollOut()
    {
        speed *= 0.5f;
        isRoll = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnermyAttack")
        {
            if (!isDamage)
            {
                EnermyAttack enermyAttack = other.GetComponent<EnermyAttack>();
                health -= enermyAttack.damage;
                StartCoroutine("OnDamage");
            }
        }
        else
            return;
    }

    // 맞았을 때 리액션
    IEnumerator OnDamage()
    {
        isDamage = true;
        anim.SetTrigger("isHit");
        yield return new WaitForSeconds(1f); // 맞았을 때 무적타임 1초

        isDamage = false;
    }
    void StopToWall()
    {

    }
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
    private void FixedUpdate()
    {
        FreezeRotation();
        StopToWall();
    }


}
