using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    public float speed = 1.5f;
    bool facingRight = true;
    //public float jump = 20f;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    //Animator anim;
    public float jumpForce = 1700;
    private gameMaster gm;

    void Start ()
    {
        myRigidbody2D = this.GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();

        //anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //anim.SetBool("Ground", grounded);

        //anim.SetFloat("vSpeed", myRigidbody2D.velocity.y);


        float move = Input.GetAxis("Horizontal");

        myRigidbody2D.velocity = new Vector2(move * speed, myRigidbody2D.velocity.y);

        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if(move < 0 && facingRight)
        {
            Flip();
        }


        //if (Input.GetKey(KeyCode.UpArrow))
       // {
        //    transform.position += Vector3.up * jump * Time.deltaTime;
       // }
     }

    void Update()
    {
        if(grounded && Input.GetKeyDown(KeyCode.JoystickButton2)|| grounded && Input.GetKeyDown(KeyCode.Space))
        {
            //anim.SetBool("Ground", false);
            myRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            gm.points += 1;
        }
        if(col.CompareTag("Bad"))
        {
            Destroy(col.gameObject);
            gm.points -= 1;
        }
    }

}
