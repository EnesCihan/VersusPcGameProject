using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hareket : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private Vector3 yer1;//karakterin başlangıç pozisyonu
    private Animator anim;
    public LayerMask block;
    public bool isDead = false;//canlı olup olmadığını kontrol eden değişken
    private float timeleft = 5f;//alttaki değer true olduktan sonra fonksiyonun çalışması için gereken bekleme süresi
    public bool reverse = false;//sahnedeki karakterleri ilk yerlerine döndürmek için gerekli değişken
    private bool isLand;//Karakterin yerde olup olmadığını kontrol etmek
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        coll = this.GetComponent<CapsuleCollider2D>();
        anim = this.GetComponent<Animator>();
        yer1 = this.transform.position;
    }

    void Update()
    {
        if (isDead == false)//karakterin ölmediğinden emin oluyoruz öldüyse hareket edemiyor
        {
            //walk
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(3, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
                anim.SetBool("isRun", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
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
            //walk end
            //jump
            if (Input.GetKeyDown(KeyCode.UpArrow) && isLand==true)
            {
                rb.velocity = new Vector2(rb.velocity.x, 6f);
                anim.SetTrigger("ofLand");
                AudioManager.instance.Play("Jump");
            }
            //jump end
        }
        else//karakter ölüyse yere değdiği an collider yok oluyor ve düşmesin diye sabitleniyor bu sayede ölü karaktere çarpmıyoruz
        {
            if (coll.IsTouchingLayers(block))
            {
                coll.enabled = false;
                rb.simulated = false;
            }
        }
        //reverse true olursa timeleft değişkeni deltatime kadar azalır ve sıfır olursa Reverse() fonksiyonu çalışır
        if (reverse == true)
        {
             timeleft -= Time.deltaTime;
             if (timeleft < 0)
             {
                Reverse();
             }
        }
        //karakter yerdemi kontroll
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
            if (collision.gameObject.tag == ("deadline"))//boşluğa düşen karakterlerin alttaki colidera çarpması durumunda onları öldüren kod.
            {
                Death();
                FindObjectOfType<slash2>().skorBlue++;
                FindObjectOfType<hareket2>().reverse = true;
            }
            if (collision.gameObject.tag == ("HitBox2"))//kılıçtaki colider karaktere çarparsa onu öldüren kod.
            {
                Death();
                FindObjectOfType<slash2>().skorBlue++;
                FindObjectOfType<hareket2>().reverse = true;
            }
        }
    }
    void Reverse()//karakterleri başlangıç durumuna döndüren fonksiyon
    {
        this.transform.position = yer1;
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
