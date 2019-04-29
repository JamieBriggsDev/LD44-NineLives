using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    [SerializeField]
    private float Frequency = 0.5f;
    private float TimeStood = 0.5f;

    [SerializeField]
    private GameObject Door = null;
    [SerializeField]
    private GameObject Room = null;
    [SerializeField]
    private SpriteRenderer SR = null;

    private bool Enabled = false;
    private bool LoadComplete = false;
    private float AlphaCounter = 0.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            TimeStood += Time.deltaTime;
            if (TimeStood >= Frequency)
            {
                TimeStood = 0.0f;
                if(Room.GetComponent<RoomManager>().UpdateDoor())
                {
                    collision.gameObject.GetComponent<PlayerController>().ReduceLifeForDoor();

                }
            }
            // Reduce Life
        }
        else
        {
            TimeStood = 0.0f;
        }


    }

    public void ActivateMark()
    {
        if(!Enabled)
        {
            Enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    public void Update()
    {
        if(Enabled && !LoadComplete)
        {
            // Turn on mark
            AlphaCounter += 1 * Time.deltaTime;
            if(AlphaCounter >= 150)
            {
                AlphaCounter = 150;
                LoadComplete = true;
            }
            SR.color = new Color(255.0f, 255.0f, 255.0f, AlphaCounter);
        }
    }
}
