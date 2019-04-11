using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth = 100;
    public float playerDistance;
    public float attackCooldown;
    public float attackTimer;
    public float attackRange = 40;
    public float projectileSpeed;

    public bool idle;
    public bool agro = false;

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
    

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("enemy active");
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerDistance();
        if (agro) {
            attack();
        }
    }

    void checkPlayerDistance() {
        playerDistance = Vector2.Distance(transform.position, playerPosition.transform.position);
        if (playerDistance < attackRange && agro == false) {
            agro = true;
            idle = false;
            anim.SetBool("spitterAttack", agro);
            anim.SetBool("spitterIdle", idle);
            Debug.Log("enemy: agro");
        }

        else if (playerDistance > attackRange && agro == true) {
            idle = true;
            agro = false;
            Debug.Log("enemy: idle");
            anim.SetBool("spitterAttack", agro);
            anim.SetBool("spitterIdle", idle);
        }
    }

    public void attack () {

      attackTimer -= Time.deltaTime;
      Debug.Log(attackTimer);

      if (attackTimer < 0f ) {
          
          Vector2 attackDirection = playerPosition.transform.position - transform.position;
            attackDirection.Normalize(); //geeft de vector een magnitude (lengte) van 1

            GameObject bileShooter;
            bileShooter = Instantiate(bile, mouth.transform.position, transform.rotation) as GameObject;
            bileShooter.GetComponent<Rigidbody2D>().velocity = attackDirection * projectileSpeed;

            
            Debug.Log("enemy attacks");
            attackTimer = attackCooldown; 
            
      }

    }
    
}
