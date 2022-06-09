using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slash2 : MonoBehaviour
{


    public LayerMask whatIsEnemy;
    private Animator anim;
    public Text SkorBoard;
    public int skorBlue = 0;
    float nextAttackTime = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.Space) && this.GetComponent<hareket2>().isDead == false)
            {
                AudioManager.instance.Play("Slash");
                Attack();
                nextAttackTime = Time.time + 0.75f;
            }
        }
        SkorBoard.text = ("Blue=" + skorBlue);
    }

    void Attack()
    {
        anim.SetTrigger("hit");
    }
}
