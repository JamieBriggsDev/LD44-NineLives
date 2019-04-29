using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private int Lives = 9;

    [SerializeField]
    private float PlayerSpeed = 5;

    [SerializeField]
    private GameObject Ball = null;
    [SerializeField]
    private float FireRate = 1.0f;
    [SerializeField]
    private float FireSpeed = 5.0f;
    [SerializeField]
    private Transform BallSpawn = null;

    [SerializeField]
    private GameObject Ghost = null;

    [SerializeField]
    private Animator AController = null;

    [SerializeField]
    private RandomBreed Breeder = null;

    private float TimeSinceLastShot = 0.0f;

    private Rigidbody2D rb = null;

    private bool IsDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TimeSinceLastShot = -1.0f;
    }

    private void Update()
    {
        // Process Ball Yeet
        // Stop timer from going too low
        if(TimeSinceLastShot > -FireRate*5)
            TimeSinceLastShot -= Time.deltaTime;
        // Get Fire Input
        float FireHorizontal = Input.GetAxisRaw("FireHorizontal");
        float FireVertical = Input.GetAxisRaw("FireVertical");


        if ((FireVertical != 0.0f || 
            FireHorizontal != 0.0f) && 
            TimeSinceLastShot <= 0.0f)
        {
            Fire(FireHorizontal, FireVertical);
            TimeSinceLastShot = FireRate;
        }


        // Process Movement
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        if (Horizontal != 0 || Vertical != 0)
        {

            ProcessMovement(Horizontal, Vertical);
            AController.SetBool("IsMoving", true);
        }
        else
        {
            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            rb.velocity = rb.velocity * 0.5f;
            AController.SetBool("IsMoving", false);
        }

        // Check if still Alive
        if(Lives <= 0 && !IsDead)
        {
            IsDead = true;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameOver();
        }
    }


    private void Fire(float _horizontal, float _vertical)
    {
        // Rotate first
        if (_horizontal != 0)
        {
            if (_horizontal > 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            }
        }
        else
        {
            if (_vertical > 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            }
        }

        GameObject ball;
        ball = Instantiate(Ball, BallSpawn.position, Quaternion.identity) as GameObject;
        Physics2D.IgnoreCollision(ball.GetComponent<Collider2D>(), GetComponent<Collider2D>());


        ball.GetComponent<Rigidbody2D>().velocity = BallSpawn.right * FireSpeed;
        ball.GetComponent<Rigidbody2D>().AddForce(rb.velocity);
    }

    private void ProcessMovement(float _horizontal, float _vertical)
    {
        // Get degrees to rotate
        Vector2 Movement = new Vector3(_horizontal, _vertical);
        float Degrees = Mathf.Atan2(Movement.y, Movement.x) * Mathf.Rad2Deg;

        // Move Player
        rb.velocity = (Movement * PlayerSpeed);
        if(TimeSinceLastShot < -FireRate)
        {
            if(_horizontal != 0)
            {
                if(_horizontal > 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                }
            }
            else
            {
                if (_vertical > 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
                }
            }
        }
    }


    public int GetLives()
    {
        return Lives;
    }

    public void ReduceLifeForDoor()
    {
        // Reduces Lives
        Lives--;

        // Change Breed
        Breeder.ChangeBreed();

        // Update Lives
        GameObject[] lives = GameObject.FindGameObjectsWithTag("Lives");
        foreach(var item in lives)
        {
            item.GetComponent<LivesManager>().UpdateLives(Lives);
        }

        // Spit out ghost
        GameObject ghost;
        ghost = Instantiate(Ghost, BallSpawn.position, Quaternion.identity) as GameObject;
        ghost.GetComponent<Rigidbody2D>().velocity = Vector2.right * FireSpeed / 2.0f;

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SpawnRIPText();
    }

    public void ReduceLife()
    {
        // Reduces Lives
        Lives--;

        // Change Breed
        Breeder.ChangeBreed();

        // Update Lives
        GameObject[] lives = GameObject.FindGameObjectsWithTag("Lives");
        foreach (var item in lives)
        {
            item.GetComponent<LivesManager>().UpdateLives(Lives);
        }

        // Spit out ghost
        GameObject ghost;
        ghost = Instantiate(Ghost, BallSpawn.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))) as GameObject;
        ghost.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized;

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SpawnRIPText();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NextRoom")
        {
            GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.LoadNextRoom();
            collision.enabled = false;
        }
        else if (collision.gameObject.tag == "Health")
        {
            GainLife();
            Destroy(collision.gameObject);
        }
    }

    public void GainLife()
    {
        print(Lives);
        if(Lives < 9)
        {
            Lives++;
            // Update Lives
            GameObject[] lives = GameObject.FindGameObjectsWithTag("Lives");
            foreach (var item in lives)
            {
                item.GetComponent<LivesManager>().UpdateLives(Lives);
            }
        }
        // Reduces Lives


    }

}
