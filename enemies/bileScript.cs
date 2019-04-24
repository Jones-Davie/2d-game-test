using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bileScript : MonoBehaviour
{
   
    public GameObject player;
    public GameObject playerBody;
    public GameObject[] spikes;
    public PlayerCombat playerCombatScript;

    private float lifeTime;
    private float damageValue = 10;

    // Start is called before the first frame update
    void Start()
    {

        lifeTime = 1f;

        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = GameObject.FindGameObjectWithTag("playerBody");
        spikes = GameObject.FindGameObjectsWithTag("spike");

        playerCombatScript = player.GetComponent<PlayerCombat>();

    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0f){

            Destroy(gameObject);
        }
    }

        public void beenHit(bool hit)
    {
        if (hit)
        {
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerBody) {
   
        playerCombatScript.playerHealth -= damageValue;
        Destroy(gameObject);

        }

        foreach (GameObject spike in spikes) {
            if (collision.gameObject == spike) {

                collision.SendMessageUpwards("hitByBile", true);
                Destroy(gameObject);
                
            }
        }
    }


}



