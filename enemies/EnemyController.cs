using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float playerDistance;
    public float attackCooldown;
    public float attackTimer;
    public float attackRange = 40;
    public float projectileSpeed;

    public bool idle;
    public bool agro = false;
    public bool dead = false;

    public GameObject bile;
    public GameObject player;
    public GameObject mouth;
    public Transform playerPosition;
    public Animator anim;

    //private Sprite[] Sprites;
   

    // Start; is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.GetComponent<Transform>();
        attackCooldown = 1f;
        attackTimer = attackCooldown;
        mouth = GameObject.FindGameObjectWithTag("spitterMouth");
        projectileSpeed = 20f;
        
        //Sprites = Resources.LoadAll<Sprite>("spitter");
        //bile = GetSpriteByName("bile");
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            checkPlayerDistance();
            if (agro) {
                attack();
            }
        }
    }

    void checkPlayerDistance() {
        
        if (!dead) {
            
            playerDistance = Vector2.Distance(transform.position, playerPosition.transform.position);

            if (playerDistance < attackRange && agro == false) {
                agro = true;
                idle = false;
                anim.SetBool("spitterAttack", agro);
                anim.SetBool("spitterIdle", idle);
            }

            else if (playerDistance > attackRange && agro == true) {
                idle = true;
                agro = false;
                anim.SetBool("spitterAttack", agro);
                anim.SetBool("spitterIdle", idle);
            }
        }
    }

    public void attack () {
    
      attackTimer -= Time.deltaTime;

      if (attackTimer < 0f ) {
          
          Vector2 attackDirection = playerPosition.transform.position - transform.position;
            attackDirection.Normalize(); //geeft de vector een magnitude (lengte) van 1

            GameObject bileShooter;
            Transform aChild = transform.FindChild( "Mouth" );
            bileShooter = Instantiate(bile, aChild.transform.position, transform.rotation) as GameObject;
            bileShooter.GetComponent<Rigidbody2D>().velocity = attackDirection * projectileSpeed;

            
            attackTimer = attackCooldown; 
            
      }

    }

    public void beenHit(bool hit)
    {
        if (hit && !dead)
        {
            idle = false;
            agro = false;
            dead = true;
            
            anim.SetBool("spitterAttack", agro);
            anim.SetBool("spitterIdle", idle);
            anim.SetBool("spitterDeath", dead);
            
            Invoke ("Death", 1);
        }
    }

    private void Death () {

        Destroy(gameObject);
    }

    
}
