using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    Image skill_Image;
    [SerializeField]
    GameObject playerDead;

    public float jumpForce;
    bool IsJump;

    Rigidbody rigid;
    Animator animator;
    Transform _transform;

    float fireDelay;
    float skillDelay;
    bool isSkillReady;
    bool isFireReady;
    public Weapon equipWeapon;
    public GameObject miniMap;

    RaycastHit hit;
    float MaxDistance = 15f; // Ray의 거리


    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        IsJump = false;
    }

    void Update()
    {
        LookAround();
        Move();
        Jump();
        Attack();
        SkillAttack();
        Die();
    }

    private void FixedUpdate()
    {
        FreezeRotation();    
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }


    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;
        float moveParameter = Mathf.Abs(moveInput.magnitude * offset);
        bool isMove = moveInput.magnitude != 0;
        animator.SetFloat("MoveSpeed", moveParameter);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += moveDir * Time.deltaTime * 5f;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.position -= moveDir * Time.deltaTime * 2f;
            }
            transform.position += moveDir * Time.deltaTime * 3f;
        }
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsJump)
            {
                IsJump = true;
                
                animator.SetBool("isJump", true);
                rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    void Attack()
    {
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;
        if (Input.GetMouseButtonDown(0) && isFireReady && IsJump == false && Cursor.visible == false)
        {
            equipWeapon.Use();
            animator.SetTrigger("Attack");
            fireDelay = 0;
        }
    }
    
    void SkillAttack()
    {
        skillDelay += Time.deltaTime;
        skill_Image.fillAmount = 20 * skillDelay / 100f;
        if(skillDelay > 5 && Input.GetMouseButtonDown(1) && IsJump == false)
        {
            equipWeapon.UseSkill();
            animator.SetTrigger("SkillAttack");
            skillDelay = 0;
        }
    }

    void Die()
    {
        if (GetComponent<Player>().hp <= 0)
        {
            animator.SetBool("isDie", true);
            gameObject.GetComponent<TPSCharacterController>().enabled = false;
            playerDead.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsJump = false;
            animator.SetBool("isJump", false);
        }
    }
}
