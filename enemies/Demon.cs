using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public float playerDistance;
    public float attackCooldownClose;
    public float attackCooldownFar;
    public float attackTimer;
    public float attackRange = 9;
    public float projectileSpeed;
    public float demonHealth;

    public bool playerClose = false;
    public bool dead = false;
    public bool demonAttacking = false;
    public bool demonAttack = false;
    public bool gameOver = false;

    public GameObject demonBile;
    public GameObject player;
    public GameObject bileDropper;
    public GameObject demonHitBox;
    public PlayerControler playerControler;

    public GameObject youWon;

    public BoxCollider2D demonHitBoxCollider;

    public Transform playerPosition;
    public Animator anim;

    //private Sprite[] Sprites;
   

    // Start; is called before the first frame update
    void Awake()
    {

        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.GetComponent<Transform>();
        playerControler = player.GetComponent<PlayerControler>();

        bileDropper = GameObject.FindGameObjectWithTag("bileDropper");
        demonHitBox = GameObject.FindGameObjectWithTag("demonHitBox");
        demonHitBoxCollider = demonHitBox.GetComponent<BoxCollider2D>();

        attackCooldownFar = 4f;
        attackCooldownClose = 2.5f;
        attackTimer = 3f;
        
        projectileSpeed = 20f;
        demonHealth = 100f;

        youWon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            checkPlayerDistance();
            attack();
        }
    }

    void checkPlayerDistance() {
        
        if (!dead) {
            
            playerDistance = Vector2.Distance(transform.position, playerPosition.transform.position);

            if (playerDistance < attackRange && playerClose == false) {

                playerClose = true;
                anim.SetBool("playerClose", playerClose);
                Debug.Log("player = close");
            }

            else if (playerDistance > attackRange && playerClose == true) {

                playerClose = false;
                anim.SetBool("playerClose", playerClose);
                Debug.Log("player = notClose");
            }
        }
    }

    public void attack () {
    
      attackTimer -= Time.deltaTime;

      if (attackTimer < 0f ) {

          if (playerClose) {
              
              Debug.Log("attacking player Close");
              
              demonAttack = true;

              anim.SetBool("demonAttack", demonAttack);
              attackTimer = attackCooldownClose;
              Invoke("setDemonAttacking", 1 );
          }
          
          if (!playerClose) {

            Debug.Log("attacking player far");

            GameObject bileDropping;
            bileDropping = Instantiate(demonBile, bileDropper.transform.position, transform.rotation) as GameObject;

            demonAttack = true;
            anim.SetBool("demonAttack", demonAttack);
            
            attackTimer = attackCooldownFar;
            Invoke("setIdle", 1);
          }
      }

    }

    public void beenHit(bool hit)
    {
        if (hit && !dead)
        {
            demonHealth -= 20f;
            Debug.Log("beenHit, demonHealth = " + demonHealth);

            if (demonHealth <= 0f ) {

                Debug.Log("Demon has died");
                dead = true;
                anim.SetBool("demonDead", dead);
                Invoke ("Death", 2);

            }
            
        }
    }

    private void Death () {

        youWon.SetActive(true);
        gameOver = true;
        playerControler.enableInput = false;
        Destroy(gameObject);
    
    }

    private void setIdle () {

        demonAttack = false;
        anim.SetBool("demonAttack", demonAttack);
        
        demonAttacking = false;
        anim.SetBool("demonAttacking", demonAttacking);
        
        demonHitBoxCollider.enabled = false;

    }

    private void setDemonAttacking () {
        
        demonAttacking = true;
        anim.SetBool("demonAttacking", demonAttacking);
        
        demonHitBoxCollider.enabled = true;
        
        Invoke("setIdle", 0.3f);
    
    }

    
}
