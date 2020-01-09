using UnityEngine;

public class moster : MonoBehaviour
{
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("傷害"), Range(0, 100)]
    public float damage = 20;
    [Header("檢查地板")]
    public Transform checkPoint;


    private Rigidbody2D r2d;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "player")
        {
            Track(collision.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player" )
        {
            collision.gameObject.GetComponent<player>().Damage();
        }
    }

    void Move()
    {
        r2d.AddForce(-transform.right * speed);

        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, -checkPoint.up, 1.5f, 1 << 8);

        if (hit == false)
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }

    void Track(Vector3 target)
    {
        if (target.x < transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
