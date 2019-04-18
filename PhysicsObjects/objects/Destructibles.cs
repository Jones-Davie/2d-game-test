using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles: MonoBehaviour
{

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void beenHit(bool hit)
    {
        if (hit)
        {
            anim.SetBool("Broken", true);
            Invoke ("Death", 1);
        }
    }

    private void Death () {
        Destroy(gameObject);
    }
}
