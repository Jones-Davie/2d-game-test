using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : PhysicsObject
{

    public float jumpSpeed = 7;
    public float maxSpeed = 7;
    public float yMovement = 0;
    public float whenFalling = 10;
    public bool falling = false;
    public bool jumping = false;


    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {


    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    protected override void computeVelocity()
    {

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");


        //roep de functie aan om de snelheden in te stellen
        moveX(move);
        moveY();
        flipAnimation(move);

        //spring als iemand op spatie drukt
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpSpeed * 2f;

            if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * .6f;
                    Debug.Log("cancel de sprong");
                }
            }

        }
        targetVelocity = move * maxSpeed;
    }


    //functie om de velocityX te setten
    private void moveX(Vector2 move)
    {
        if (move.x < -0.01f) //loop naar links
        {
            animator.SetFloat("velocityX", move.x * -1f);
        }

        if (move.x > 0.01f) //loop naar rechts
        {
            animator.SetFloat("velocityX", move.x);
        }

        if (move.x == 0) //loop niet
        {
            animator.SetFloat("velocityX", move.x);
        }
    }

    //functie om spring en val animatie te tonen
    private void moveY()
    {

        if (velocity.y > 0.01f || velocity.y < -0.01f) // als er beweging is op Y-as
        {
            grounded = false; //sta je niet op de grond
            
            if (yMovement < whenFalling && falling == false) //als de Y positie van het vorige frame hoger is dan de vorige, val je
            {
                Debug.Log("falling: ymovement =" + yMovement);
                Debug.Log("falling: velocity.y =" + velocity.y);
                falling = true;
                jumping = false;
                animator.SetBool("playerFalling", falling);
                animator.SetBool("playerJumping", jumping);
                Debug.Log("falling");
            }

            else if (yMovement > whenFalling && jumping == false)
            { //als je niet valt en er wel Y beweging is ga je omhoog en dus spring je
                falling = false;
                jumping = true;
                animator.SetBool("playerFalling", falling);
                animator.SetBool("playerJumping", jumping);
                Debug.Log("jumping: ymovement = " + yMovement);
                Debug.Log("jumping: velocity.y = " + velocity.y);
            }
        }

        else if (velocity.y == 0f) //als er geen Y beweging is dan sta je op de grond
        {
            grounded = true;
            falling = false;
            jumping = false;
            animator.SetBool("playerFalling", falling);
            animator.SetBool("playerJumping", jumping);
            Debug.Log("grounded");
        }

        yMovement = velocity.y; //update de vorige y positie met de huidige y positie

    }


    //flip animatie als player andere kant op rent
    private void flipAnimation(Vector2 move)
    {
        if (move.x < -0.01f && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;

        }

        if (move.x > 0.01f && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }
    }
}