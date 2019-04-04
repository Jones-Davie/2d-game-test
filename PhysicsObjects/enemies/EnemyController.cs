using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth = 100;
    public float playerDistance;
    public float attackCooldown;
    public float attackRange = 40;
    public float projectileSpeed = 100;

    public bool idle;
    public bool agro = false;

    public GameObject bile;
    public GameObject player;
    public Transform playerPosition;
    public Animator anim;


    // Start; is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.GetComponent<Transform>();
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
}
