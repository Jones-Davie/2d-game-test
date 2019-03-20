using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackHitBox : MonoBehaviour
{

    public GameObject player;
    public PlayerControler playerControlerScript;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerControlerScript = player.GetComponent<PlayerControler>();

    }

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

       private void OnTriggerEnter2D(Collider2D collision)
    {
              
        Debug.Log("trigger Collision");
        collision.SendMessageUpwards("beenHit", true);
        
    }

}
