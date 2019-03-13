using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    public float gravityModifier = 1f;
    public float minGroundNormalY = 1f;

    //maak variabelen om te kijken of iets de grond raakt en waar de grond is.
    protected bool grounded;

    //groundNormal = lijn die loodrecht op de grond staat
    protected Vector2 groundNormal;

    //protected = inherit en accessible maar niet aanpasbaar.
    protected Vector2 velocity;


    protected Vector2 targetVelocity;

    //Rigidbody2d is object type die een unity object blootstelt aan de physics.
    protected Rigidbody2D rb2d;
    protected const float minMoveDistance = 0.001f;

    //contactfilter kijkt of er een 2d collision is met iets anders dat een 2d collission heeft
    protected ContactFilter2D contactFilter;

    //maak een array van dingen die mogelijk een collission kunnen hebben voor de contactfilter
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];


    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    //maak een kleine forcefield om je karakter om te zorgen dat het nooit in een andere collissiondetector komt zodat het vast blijft zitten.
    protected const float shellRadius = 0.01f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //don't use the standard triggers
        contactFilter.useTriggers = false;

        // gebruik de contactfilter om te checken of de LayerCollissionMask van het gameObject een collission ziet met een ander object
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        //gebruik de layermasker om dit te doen
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = Vector2.zero;
        playerInput();
        
    }

    protected virtual void playerInput()
    {

    }

    private void FixedUpdate()
    {

        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime * 1.5f;

        velocity.x = targetVelocity.x;

        grounded = false;

        //maak een nieuwe vector op dezelfde hoogte als de grondlijn, met een lijn die niet loodrecht op de grond staat maar over de grond heen loopt 
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        //zwaartekracht
        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

         move = Vector2.up * deltaPosition.y;

        Movement(move, true);

    }

    void Movement(Vector2 move, bool yMovement)
    {
        //kijk hoe groot de lengte/kracht van een sprong is
        float distance = move.magnitude;

        //als kracht groter is dan minimum afstand
        if (distance > minMoveDistance)
        {
            //kijk wat er in de rigid body 2d gebeurd, hoe beweegt het, is het ergens mee in contact, raakt het iets, wat is de magnitude en collision buffer
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();

            //gooi alles wat rb2d raakt in een buffer
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            //voor elk ding in de buffer, check of het contact maakt in de juiste hoek, zo ja: poppetje staat op grond en staat stil.
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }
}
