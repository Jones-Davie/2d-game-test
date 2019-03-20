using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControlerScript = GetComponent<PlayerControler>();

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
      
    }

    private void playerAttack () 
    {
          if (playerControlerScript.isAttacking && playerControlerScript.spriteRenderer.flipX)
        {
            Debug.Log("backBox");
            attackHitBoxBack.enabled = true;
        }

        else if (playerControlerScript.isAttacking && playerControlerScript.spriteRenderer.flipX == false) 
        {
            Debug.Log("frontBox");
            attackHitBoxFront.enabled = true;
        }

        else
        {
            attackHitBoxFront.enabled = false;
            attackHitBoxBack.enabled = false;
        }
    }
    

}
