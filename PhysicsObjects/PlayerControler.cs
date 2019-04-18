using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : PhysicsObject
{
    public float jumpSpeed = 8;
    public float maxSpeed = 7;
    public float yMovement = 0;
    private float whenFalling = 10;

    public bool falling = false;
    public bool jumping = false;

    public double attackTime = 0.25d;
    public double attackTimer = 0;
    public bool isAttacking = false;
    public bool leftMouseButtonDown = false;
    public bool enableInput = true;
    
   
    private Animator animator;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void playerInput()
    {
        if (enableInput) {
        // ****** aanvallen *****

        //speler valt aan als linkermuisknop in is gedrukt en hij niet al bezig is met aanvallen en hij op de grond staat
            if (Input.GetMouseButtonDown(0) && isAttacking == false && leftMouseButtonDown == false && grounded == true)
            {
                attack(); //voer aanval functie uit
                leftMouseButtonDown = true; //moet meerdere keren klikken om aan te vallen, kan niet in blijven houden
            }

            //als speler muis knop heeft los gelaten mag hij weer aanvallen
            if (Input.GetMouseButtonUp(0))
            {
                leftMouseButtonDown = false;
            }

            attackCountDown();


            // ****** beweging *****
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

                /*            if (Input.GetButtonUp("Jump"))
                            {
                                Debug.Log("ButtonUp");
                                if (velocity.y > 0)
                                {
                                    velocity.y = velocity.y * .6f;
                                    Debug.Log("cancel de sprong");
                                } 
                            }*/

            }
            targetVelocity = move * maxSpeed;
        }
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

        if (velocity.y > 0.05f || velocity.y < -0.05f) // als er beweging is op Y-as
        {
            grounded = false; //sta je niet op de grond

            if (yMovement < whenFalling && falling == false) //als de Y positie van het vorige frame hoger is dan de vorige, val je
            {
                falling = true;
                jumping = false;
                animator.SetBool("playerFalling", falling);
                animator.SetBool("playerJumping", jumping);
            }

            else if (yMovement > whenFalling && jumping == false)
            { //als je niet valt en er wel Y beweging is ga je omhoog en dus spring je
                falling = false;
                jumping = true;
                animator.SetBool("playerFalling", falling);
                animator.SetBool("playerJumping", jumping);
            }
        }

        else if (velocity.y < 0.05f) //als er geen Y beweging is dan sta je op de grond
        {
            grounded = true;
            falling = false;
            jumping = false;
            animator.SetBool("playerFalling", falling);
            animator.SetBool("playerJumping", jumping);
        }

        yMovement = velocity.y; //update de vorige y positie met de huidige y positie

    }

    //flip animatie als player andere kant op rent
    public void flipAnimation(Vector2 move)
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

    //functie om aan te vallen
    private void attack()
    {
        isAttacking = true;
        animator.SetBool("playerAttack", isAttacking);
        attackTimer = attackTime;


    }

    //laat een timer lopen nadat player aan heeft gevallen zodat hij weer op non-attacking wordt gezet
    private void attackCountDown()
    {
        if (attackTimer != 0f)
        {

            attackTimer -= Time.deltaTime;

            if (attackTimer < 0f)
            {
                attackTimer = 0f;
                isAttacking = false;
                animator.SetBool("playerAttack", isAttacking);
            }

        }


    }

}
