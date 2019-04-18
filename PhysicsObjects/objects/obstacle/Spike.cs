using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBody;
    public PlayerCombat playerCombatScript;

    private float damageValue = 10;
    private float damageTimer = 0;
    private float damageCooldown = 1;
    private bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = GameObject.FindGameObjectWithTag("playerBody");
        playerCombatScript = player.GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collided) {
            Debug.Log("colled timer");
            damageTimer -= Time.deltaTime;

            if (damageTimer <= 0) {
                    Debug.Log("coll timer damage" );
                    playerCombatScript.playerHealth -= damageValue;
                    damageTimer = damageCooldown;
            }
        }
    }

       private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == playerBody) {
                Debug.Log("coll player");
                collided = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {

            collided = false;
            Debug.Log("exit coll player");

        }             
          
}

