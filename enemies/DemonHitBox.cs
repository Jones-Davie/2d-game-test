using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHitBox : MonoBehaviour
{

    public float damageCooldown = 2f;
    public float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        damageTimer = damageCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer -= Time.deltaTime;
    }

       private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (damageTimer < 0f ) {
        
        collision.SendMessageUpwards("playerHit", 20f);
        damageTimer = damageCooldown;
        }
    }
}
