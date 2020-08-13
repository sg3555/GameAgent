using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rm_move : MonoBehaviour
{
    public float maxspeed;
    public int speed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    [SerializeField]
    Transform pos;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask islayer;
    public int jumpcount;
    int jumpcnt;
    public Animator anim;
    public GameObject Bullet;
    public Transform buipos;


    bool isGround;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        jumpcnt = jumpcount;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
    private void FixedUpdate()//플레이어가 이동하게 하는 함수
    {
        float v = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(v * 3, rigid.velocity.y);
        if (v > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            anim.SetBool("isrun", true);
        }
        else if (v < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            anim.SetBool("isrun", true);


        }
        else
        {
            anim.SetBool("isrun", false);
        }
        //사다리 올라가는 것에 대한 애니메이션과 방향과 속도를 정하는 함수
        if (isladder)
        {
            float ver = Input.GetAxis("Vertical");
         if (Input.GetKeyDown(KeyCode.Z))
            {
                anim.SetBool("climbshoot", true);
                Invoke("bulletInstantiate", 0.3f);
                Invoke("climbshotDeactive", 0.3f);
            }
         else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("isclimb", true);
;
            }
            //else
            //{
            //    anim.SetBool("climbshoot", false);
            //}
            rigid.gravityScale = 0;
                rigid.velocity = new Vector2(rigid.velocity.x, ver * speed);
                               
        }
        else
        {
            
            rigid.gravityScale = 3f;
            anim.SetBool("isclimb", false);
        }
    }
    void bulletInstantiate()
    {
        Instantiate(Bullet, pos.position, transform.rotation);
    }
   void climbshotDeactive()
    {
        anim.SetBool("climbshoot", false);
    }
    public bool isladder = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder")) { isladder = true; }
       

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder")) { isladder = false; }
    }
    private void Update()
    {
        //땅에 있는 상태에서 점프할 경우
        isGround = Physics2D.OverlapCircle(pos.position,checkRadius,islayer);
        if (isGround==true&&Input.GetButtonDown("Jump")&&jumpcnt>0)
        {
            anim.SetTrigger("jump");
            rigid.velocity = Vector2.up * jumpPower;
            
        }
        //이미 한번 점프한 상태에서 다시 점프할 경우
         if (isGround == false && Input.GetButtonDown("Jump") && jumpcnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
    
        }
         //땅에 착지한 경우
        if (isGround && jumpcnt != jumpcount)
        {
            anim.SetTrigger("isground");
            rigid.velocity = Vector2.zero;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpcnt-- ;
        }
        if (isGround)
        {
            jumpcnt = jumpcount;
        }
      
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onDamaged(collision.transform.position);
        }
    }
    //데미지를 받아서 무적상태일 경우
    void onDamaged(Vector2 targetPos)
    {
        gameObject.layer = 11;

        sprite.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1:-1;
        rigid.AddForce(new Vector2(dirc, 1)*3, ForceMode2D.Impulse);
        
        //애니메이션
        anim.SetTrigger("damaged");
        Invoke("offDamaged", 3);
    }
    void offDamaged()
    {
        gameObject.layer = 9;
        sprite.color = new Color(1, 1, 1, 1);
    }
}
    // Start is called before the first frame update
    //    void Awake()
    //    {
    //        rigid = GetComponent<Rigidbody2D>();
    //        spriteRenderer = GetComponent<SpriteRenderer>();
    //        anim = GetComponent<Animator>();
    //    }

    //    void Update()//단발적인 키입력
    //    {
    //        //점프
    //        if (Input.GetButtonDown("Jump")&&!anim.GetBool("isjump"))
    //        {
    //            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    //            anim.SetBool("isjump", true);
    //        }
    //        if (Input.GetButtonUp("Horizontal"))
    //        {

    //            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.6f, rigid.velocity.y);
    //        }
    //        //방향 전환
    //        if (Input.GetButtonDown("Horizontal"))
    //        {
    //            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
    //        }
    //        //애니메이션(달리기,멈춤)
    //        if (rigid.velocity.normalized.x == 0)
    //        {
    //            anim.SetBool("isrun", false);
    //        }
    //        else if(Input.GetButtonDown("Horizontal"))
    //            anim.SetBool("isrun", true);
    //    }
    //    // Update is called once per frame
    //    void FixedUpdate()//지속적인 키입력
    //    {
    //        //키보드를 활용한 이동
    //        float h = Input.GetAxisRaw("Horizontal");
    //        rigid.velocity = new Vector2(h * 3, rigid.velocity.y);
    //        //rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

    //        if (rigid.velocity.x > maxspeed)//오른쪽 방향
    //        {
    //            rigid.velocity = new Vector2(maxspeed,rigid.velocity.y);
    //        }else if( rigid.velocity.x < maxspeed*(-1))//왼쪽 방향
    //            {
    //                rigid.velocity = new Vector2(maxspeed*(-1), rigid.velocity.y);
    //            }
    //        //땅에 접촉시
    //        if (rigid.velocity.y < 0) {
    //            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

    //            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));
    //            if (rayHit.collider != null)
    //            {
    //                if (rayHit.distance < 0.5f)
    //                {
    //                    anim.SetBool("isjump", false);
    //                }
    //            }
    //            if (isLadder)
    //            {
    //                float ver = Input.GetAxis("Vertical");
    //                rigid.velocity = new Vector2(rigid.velocity.x, ver * speed);
    //            }

    //        }

    //    }
    //    public bool isLadder;
    //    private void OnTriggerEnter2D(Collider2D collision)
    //    {
    //        if (collision.CompareTag("ladder"))
    //        {
    //            isLadder = true;
    //        }
    //    }
    //    private void OnTriggerExit2D(Collider2D collision)
    //    {
    //        if (collision.CompareTag("ladder"))
    //        {
    //            isLadder = false;
    //        }
    //    }

