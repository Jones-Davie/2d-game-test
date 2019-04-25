using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBile : MonoBehaviour
{
    private float lifeTime;
     public GameObject player;
     public GameObject playerBody;
     public GameObject[] spikes;
    public PlayerCombat playerCombatScript;
    private float damageValue = 15;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 2.5f;    
        player = GameObject.FindGameObjectWithTag("Player");
        playerCombatScript = player.GetComponent<PlayerCombat>();
        playerBody = GameObject.FindGameObjectWithTag("playerBody");
        spikes = GameObject.FindGameObjectsWithTag("spike");
 
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        position = transform.position;
        position.y -= (Time.deltaTime * 4);
        transform.position = position;

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

        Debug.Log("collission player");      
        playerCombatScript.playerHealth -= damageValue;
       
        }

        foreach (GameObject spike in spikes) {
            if (collision.gameObject == spike) {
                collision.SendMessageUpwards("hitByBile", true);
            }
        }

         Destroy(gameObject);

    }


}
