using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBody;
    public PlayerCombat playerCombatScript;

    public Rigidbody2D rb;

    private float damageValue = 10;
    private float damageTimer = 0;
    private float damageCooldown = 1;
    private bool collided = false;
    private bool beenHitByBile = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = GameObject.FindGameObjectWithTag("playerBody");
        playerCombatScript = player.GetComponent<PlayerCombat>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            damagePlayer();
            if (rb.velocity.x < 0 && beenHitByBile ) {
                damageValue = 30;
                Invoke ("Death", 1);
            }
    }

       private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == playerBody) {
                collided = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {

            collided = false;
            damageTimer = 0;
        }             

        public void hitByBile (bool bile) {
            if (bile) {
                damageValue = 15;
                beenHitByBile = true;
                
            }
        }

        private void damagePlayer() {
            if (collided) {
                damageTimer -= Time.deltaTime;

                if (damageTimer <= 0) {
                        playerCombatScript.playerHealth -= damageValue;
                        damageTimer = damageCooldown;
                }
            }
        }

        private void Death () {
            Destroy(gameObject);
        }
          
}

