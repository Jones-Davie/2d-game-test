using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //script, object en waarden van playerHealth
    public float playerHealth;
    private float playerOldHealth;
    public float playerMaxHealth = 100;
    public bool playerDead = false;
    private float delayDestruct;

    //script en object van hitBoxes
    public Collider2D attackHitBoxFront;
    public Collider2D attackHitBoxBack;
    public GameObject attackHitBoxFrontObject;
    public GameObject attackHitBoxBackObject;

    //script en object van destructibles
    public Destructibles destructiblesScript;
    public GameObject[] destructibles;
    public List<GameObject> destructibleList;

    //script en object van player
    public GameObject player;
    public PlayerControler playerControlerScript;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {

        //init health en vind healthbar
        playerHealth = 50;
        playerOldHealth = playerHealth;
        
        //vind player
        player = GameObject.FindGameObjectWithTag("Player");
        playerControlerScript = GetComponent<PlayerControler>();
        animator = GetComponent<Animator>();

        //vind hitboxes en zet ze uit
        attackHitBoxBackObject = GameObject.FindGameObjectWithTag("PlayerAttackHitBoxBack");
        attackHitBoxBack = attackHitBoxBackObject.GetComponent<BoxCollider2D>();

        attackHitBoxFrontObject = GameObject.FindGameObjectWithTag("PlayerAttackHitBox");
        attackHitBoxFront = attackHitBoxFrontObject.GetComponent<BoxCollider2D>();

        attackHitBoxBack.enabled = false;
        attackHitBoxFront.enabled = false;
    }

    private void Awake()
    {
   
    }

    private void Update()
    {    
        playerAttack();
        playerHealthUpdate();
      
    }

    //zet hitbox voor/achter aan/uit als player links of rechts kijkt
    private void playerAttack () 
    {
          if (playerControlerScript.isAttacking && playerControlerScript.spriteRenderer.flipX)
        {
            attackHitBoxBack.enabled = true;
        }

        else if (playerControlerScript.isAttacking && playerControlerScript.spriteRenderer.flipX == false) 
        {
            attackHitBoxFront.enabled = true;
        }

        else
        {
            attackHitBoxFront.enabled = false;
            attackHitBoxBack.enabled = false;
        }
    }

    //check player health en update healthbar
    private void playerHealthUpdate () {

        if ( playerHealth != playerOldHealth ) {
            playerOldHealth = playerHealth;
            
            if (playerHealth > playerMaxHealth ) {
                playerHealth = playerMaxHealth;
            }

        }

        if (playerHealth <= 0f && playerDead == false) {
            
            playerDead = true;
            animator.SetBool("playerDead", playerDead);
            delayDestruct = 2f;
            playerControlerScript.enableInput = false;
        }

        if (playerDead == true) {
            delayDestruct -= Time.deltaTime;

        }

        if (playerDead == true && delayDestruct < 0f) {
            Destroy(gameObject);
        }
    }

    private void playerDeath() {

    }
    

}
