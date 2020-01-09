using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public int speed = 10;
    public float jump = 2.5f;
    public UnityEvent onEat;
    public float hp = 3;
    public GameObject[] hpBar;
    public GameObject final;
    public int fruit = 10;
    public npc NPC;

    private Rigidbody2D r2d;
    private Animator ani;
    private bool isGround;
    private float hpMax;
    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();

        hpMax = hp;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Walk();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn();
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
        if (hp == 2)
        {
            Destroy(hpBar[2].gameObject);
        }
        if (hp == 1)
        {
            Destroy(hpBar[1].gameObject);
        }
        if (hp <=0)
        {
            Destroy(hpBar[0].gameObject);
            Destroy(hpBar[1].gameObject);
            Destroy(hpBar[2].gameObject);
        }
        if(this.transform.position.y<=-5.8)
        {
            hp = 0;
            Dead();
        }
        Jump();
    }
    private void Walk()
    {
        if (r2d.velocity.magnitude < 10)
            r2d.AddForce(new Vector2(speed * Input.GetAxisRaw("Horizontal"), 0));
        ani.SetBool("run", Input.GetAxisRaw("Horizontal") != 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGround = true;
            ani.SetBool("jump", false);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
            ani.SetBool("jump", true);
        }
    }
    private void Turn(int direction = 0)
    {
        transform.eulerAngles = new Vector3(0, direction, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "好吃")
        {
            Destroy(collision.gameObject);
            onEat.Invoke();
            fruit -= 1;
            if (fruit <= 0)
            {
                NPC.Complete();
            }

        }
    }
    public void Damage()
    {
        hp = hp - 1;

        if (hp <= 0) Dead();
    }


    private void Dead()
    {
        this.gameObject.SetActive(false);
        final.SetActive(true);
    }
}
