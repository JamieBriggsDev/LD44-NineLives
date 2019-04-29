using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField]
    private Animator AController;

    [SerializeField]
    private bool Attack = true;

    [SerializeField]
    private float MinSpeed = 0.5f;
    [SerializeField]
    private float MaxSpeed = 1.0f;

    private float Speed;

    [SerializeField]
    private GameObject Life = null;

    public bool DropLife = false;


    // Animation Timers
    private float TimeTillAnimationStarts;
    private float Counter = 0.0f;

    // Reference to Player
    private GameObject Player = null;

    // Start is called before the first frame update
    void Start()
    {
        // Pause Animation on start
        AController.enabled = false;
        TimeTillAnimationStarts = Random.Range(0.0f, 3.0f);

        // Get Player
        Player = GameObject.FindGameObjectWithTag("Player");

        Speed = Random.Range(MinSpeed, MaxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Only update if enabled
        if(enabled)
        {
            // Enable animation once timer hits time
            if(!AController.enabled)
            {
                Counter += Time.deltaTime;
                if(Counter > TimeTillAnimationStarts)
                {
                    AController.enabled = true;
                }
            }

            // Rotate to face player
            transform.LookAt(Player.transform.position);
            // Fix rotation
            transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f), Space.Self);

            // Move Towards player (Move if not too close)
            if (Vector3.Distance(transform.position, Player.transform.position) > 0.5f)
            {
                transform.Translate(new Vector2(Speed, 0.0f) * Time.deltaTime);
            }
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().IncrementScore(1);

            // Check to see if life should be dropped
            if(DropLife)
            {
                GameObject ghost;
                ghost = Instantiate(Life, transform.position, Quaternion.identity) as GameObject;
            }

            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().ReduceLife();
            transform.Translate(new Vector2(-1.0f, 0.0f));
        }
    }
}
