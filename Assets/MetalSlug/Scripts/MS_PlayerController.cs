using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_PlayerController : MonoBehaviour
{
    Vector2 originPosition; //캐릭터 최초위치
    Rigidbody2D rigid; //물리엔진
    Animator anim;  //스프라이트 애니메이션 담당
    AudioSource audioSource; //소리제어자
    Collider2D col; //충돌제어자
    SpriteRenderer sprite; //스프라이트제어자

    public bool startGame;  //게임 시작상태 bool
    public bool clear; //클리어 여부 
    public bool dead;
    private float maxSpeed = 5f; // Player Speed
    //public int jumpForce = 450; // Player jump force
    private int height = 15; // Player jump force
    private int groundLayerNum = 21; // ground의 레이어 번호
    private int playerLayerNum = 22;
    private int goalLayerNum = 24; 
    private int P_BulletLayerNum = 25; 
    private int E_BulletLayerNum = 26; 
    //private int PlatformLayerNum = 8; 
    private int enemyLayerNum = 23; 
    public GameObject knife;
    //public bool isKnife = false;
    private Transform groundCheck;
    private bool onGround = false;
    private bool groundLineCheck = false;
    private bool groundColCheck = false;
    public bool GM_isdead, GM_goal, GM_clear; //게임매니저 수신용
    private float fireRate = 0.6f;
    private float nextFire = 0f;
    private float throwRate = 2f;
    private float nextThrow = 0f;
    public Transform muzzle;
    public Transform grenade;
    public GameObject bulletPrefab;
    public GameObject grenadePrefab;
    public AudioClip audioKnife_rope;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startGame = false;
        clear = false;
        dead = false;
        originPosition = this.gameObject.transform.position;
        groundCheck = gameObject.transform.Find("GroundCheck");
        Physics2D.IgnoreLayerCollision(playerLayerNum, enemyLayerNum, true);
    }

    void FixedUpdate()
    {
        eri_move();
        eri_speed();
        eri_jump();
        multiGroundCheck();
        useKnifeLayer();
        fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == goalLayerNum && onGround)
        {
            useKnife();
            clear = true;
            Invoke("eri_clear", 1.2f);
        }

        if (collision.gameObject.layer == E_BulletLayerNum && !dead)
        {
            startGame = false;
            rigid.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("Die");
            GM_isdead = true;
            col.isTrigger = true;
            // 여기에 플레이어 죽는 애니메이션
            //Debug.Log(collision.gameObject.name);
            dead = true;
            //anim.SetBool("IsDead", true);
            //Invoke("stopScene", 1.0f);
        }

        if (collision.tag == "Sign")
        {
            if (collision.name.Contains("Sign_B"))
            {
                if (startGame)
                {
                    anim.SetTrigger("Grenade");
                    Invoke("throwGrenade", 0.4f);
                    //throwGrenade();
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Sign")
        {
            if (collision.name.Contains("Sign_Up"))
            {
                if (!anim.GetBool("IsJump") && startGame)
                {
                    rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
                    anim.SetBool("IsJump", true);
                }
            }
            if (collision.name.Contains("Sign_Down"))
            {

            }
            if (collision.name.Contains("Sign_Left"))
            {

            }
            if (collision.name.Contains("Sign_Right"))
            {

            }
            if (collision.name.Contains("Sign_A"))
            {
                if (!anim.GetBool("IsJump") && startGame)
                {
                    rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
                    anim.SetBool("IsJump", true);
                }
            }
            if (collision.name.Contains("Sign_B"))
            {

            }
            if (collision.name.Contains("Sign_X"))
            {

            }
            if (collision.name.Contains("Sign_Y"))
            {

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.layer + "Enter");
        if (collision.gameObject.layer == groundLayerNum)
        {
            groundColCheck = true;
            //if (anim.GetBool("IsJump") && groundLineCheck)
            if (anim.GetBool("IsJump"))
            {
                anim.SetBool("IsJump", false);
            }
            else if (anim.GetBool("IsFall"))
            {
                anim.SetBool("IsFall", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.layer + "Exit");
        if (collision.gameObject.layer == groundLayerNum)
        {
            groundColCheck = false;
        }
    }

    void eri_move()
    {
        //게임 시작상태에서만 움직임 활성화
        if (startGame && !clear)
        {
            rigid.AddForce(Vector2.right * 1f, ForceMode2D.Impulse);
        }

        if (Mathf.Abs(rigid.velocity.x) > 0.3)
        {
            anim.SetBool("IsRunning", true);
        }
        else
            anim.SetBool("IsRunning", false);
    }
    void eri_speed()
    {
        if (startGame)
        {
            //최대속력 설정
            if (rigid.velocity.x > maxSpeed && !clear)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < -maxSpeed && !clear)
                rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }
        

        //클리어 시 정지
        if (clear || dead)
            rigid.velocity = new Vector2(0, 0);
    }
    void eri_jump()
    {

        if (anim.GetBool("IsJump") && rigid.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(playerLayerNum, groundLayerNum, true);
        }
        else if (anim.GetBool("IsJump") && rigid.velocity.y < 0)
        {
            Debug.DrawRay(groundCheck.position, Vector2.down * 0.5f, new Color(0, 255, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                Physics2D.IgnoreLayerCollision(playerLayerNum, groundLayerNum, false);
            }
        }


        if(!anim.GetBool("IsJump") && rigid.velocity.y < 0 && onGround == false)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
            if (rayHit.collider == null)
            {
                anim.SetBool("IsFall", true);
            }
        }
        else if(onGround == true)
        {
            anim.SetBool("IsFall", false);
        }
    }
    void eri_clear()
    {
        anim.SetBool("IsClear", true);
        GM_clear = true;
    }
    void multiGroundCheck()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null)
        {
            //Debug.Log(rayHit.distance);
            groundLineCheck = true;
        }
        else
        {
            groundLineCheck = false;
        }

        if (groundLineCheck && groundColCheck)
            onGround = true;
        else
            onGround = false;
    }

    public void startMove()
    {
        Debug.Log("start");
        rigid.bodyType = RigidbodyType2D.Dynamic;
        startGame = true;
    }

    public void resetGame()
    {
        Debug.Log("reset");
        rigid.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsJump", false);
        anim.SetBool("IsClear", false);
        anim.SetBool("IsFall", false);
        gameObject.transform.position = originPosition;
        Physics2D.IgnoreLayerCollision(playerLayerNum, groundLayerNum, false);
        startGame = false;
    }

    //GameManager나 다른 오브젝트에서 플레이어의 상태를 조회
    public string getState()
    {
        if (GM_clear)
        {
            return "clear";
        }else if (GM_isdead)
        {
            return "dead";
        }
        else
        {
            return "null";
        }
    }
    private void useKnife()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Eri_Knife"))
        {
            anim.SetTrigger("useKnife");
            PlaySound(audioKnife_rope);
        }
    }
    private void useKnifeLayer()
    {
        //근접공격 중 칼날에 타격판정 부여
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Eri_Knife"))
        {
            knife.layer = P_BulletLayerNum;
            //Debug.Log(knife.layer);
        }
        else
        {
            knife.layer = playerLayerNum;
        }
    }
    private void fire()
    {
        Debug.DrawRay(this.gameObject.transform.position, Vector2.right * 12f, new Color(0, 255, 0));
        //RaycastHit2D rayHit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.right, 12f, LayerMask.GetMask("Enemy"));
        RaycastHit2D rayHit = Physics2D.Raycast(muzzle.transform.position, Vector2.right, 12f);
        Debug.Log(rayHit.collider);

        if(rayHit.collider != null)
        {
            if(startGame && onGround && Time.time > nextFire)
            {
                if(rayHit.collider.name == "Arabian" || rayHit.collider.name == "Soldier")
                {
                    //    //anim.SetTrigger("Fire");
                    anim.SetBool("IsFire", true);
                    nextFire = Time.time + fireRate;
                    //    //GameObject tempBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
                    Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
                }
                else
                {
                    anim.SetBool("IsFire", false);
                }
            }
            else
            {
                anim.SetBool("IsFire", false);
            }

        }
        //if (rayHit.collider != null && startGame && onGround && Time.time > nextFire)
        //{
        //    Debug.Log(rayHit.collider.name);
        //    //anim.SetTrigger("Fire");
        //    anim.SetBool("IsFire", true);
        //    nextFire = Time.time + fireRate;
        //    //GameObject tempBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        //    Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        //    //tempBullet.transform.eulerAngles = new Vector3(0, 0, 180f);
        //}
        //else
        //{
        //    anim.SetBool("IsFire", false);
        //    //anim.SetBool("IsAttack", false);
        //}
    }

    private void throwGrenade()
    {
        if (Time.time > nextThrow)
        {
            nextThrow = Time.time + throwRate;
            GameObject tempGrenade = Instantiate(grenadePrefab, grenade.position, grenade.rotation);
        }
        else
        {
            //anim.SetBool("IsFire", false);
        }
    }

    private void stopScene()
    {
        Time.timeScale = 0f;
    }

    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }
}
