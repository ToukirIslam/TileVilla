using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] float projctileSpeed = 1f;
    [SerializeField] float bulletSpeed = 10f;
    //State
    public bool isAlive = true;

    //cached reference
    [SerializeField] GameObject bulletPrefeb;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;
    Bullet bullet;
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
        Die();
        Fire();
    }

    private void Run()
    {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed;
        Vector2 playerVelocity = new Vector2(h, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        var newXpos = transform.position.x + h;
        transform.position = new Vector2(newXpos, transform.position.y);
        bool playerRunAnimation = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        {
            myAnimator.SetBool("Running", playerRunAnimation);
        }
    }
    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("ground"))) { return; }
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }
    private void FlipSprite()
    {
        //if player moving horizontally 
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            //reverse the current scaling of x axis
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x),1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ClimbLadder();
    }
    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climing")))
        {
            myAnimator.SetBool("Climing", false);

            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        float v = Input.GetAxis("Vertical") * Time.deltaTime * climbSpeed;
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, v);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        var newYpos = transform.position.y + v;
        transform.position = new Vector2(transform.position.x, newYpos);
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        {
            myAnimator.SetBool("Climing", playerHasVerticalSpeed);

        }
    }
    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy","Spikes")))//using tag reference 
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    private void Fire()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject bullet = Instantiate(bulletPrefeb, transform.position, Quaternion.identity) as GameObject;
            MoveBullet();
        }
    }
    public void MoveBullet()
    {
            transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }
}

