using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hareket2 : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private Vector3 yer2;
    private Animator anim;
    public LayerMask block;
    public bool isDead = false;
    private float timeleft = 5f;
    public bool reverse = false;
    private bool isLand;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        coll = this.GetComponent<CapsuleCollider2D>();
        anim = this.GetComponent<Animator>();
        yer2 = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(3, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
                anim.SetBool("isRun", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-3, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
                anim.SetBool("isRun", true);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("isRun", false);
            }
            if (Input.GetKeyDown(KeyCode.W) && isLand==true)
            {
                rb.velocity = new Vector2(rb.velocity.x, 6f);
                anim.SetTrigger("ofLand");
                AudioManager.instance.Play("Jump");
            }
        }
        else
        {
            if (coll.IsTouchingLayers(block))
            {
                coll.enabled = false;
                rb.simulated = false;
            }
        }
        if (reverse == true)
        {
            timeleft -= Time.deltaTime;
            if (timeleft < 0)
            {
                Reverse();
            }
        }
        if (coll.IsTouchingLayers(block))
        {
            isLand = true;
            anim.SetBool("isJumping", false);
        }
        else
        {
            isLand = false;
            anim.SetBool("isJumping", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead == false)
        {
            if (collision.gameObject.tag == ("deadline"))
            {
                Death();
                FindObjectOfType<slash>().skorRed++;
                FindObjectOfType<hareket>().reverse = true;
            }
            if (collision.gameObject.tag == ("HitBox1"))
            {
                Death();
                FindObjectOfType<slash>().skorRed++;
                FindObjectOfType<hareket>().reverse = true;
            }
        }
    }
    public void Reverse()
    {
        this.transform.position = yer2;
        isDead = false;
        reverse = false;
        timeleft = 5f;
        anim.SetBool("isAlive", true);
        coll.enabled = true;
        rb.simulated = true;
    }
    public void Death()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        anim.SetBool("isAlive", false);
        reverse = true;
        AudioManager.instance.Play("Death");
    }
    public void Stop()
    {
        rb.simulated=false;
    }
}
