using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    [SerializeField]
    private Object Life = null;
    public void Start()
    {
        PlayerController pc = 
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        UpdateLives(pc.GetLives());
    }
    public void UpdateLives(int TotalLives)
    {
        // First remove all lives
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Next add lives depending on how many lives the
        //  player has
        for(int i = 0; i < 9; i++)
        {
            GameObject obj = 
                Instantiate(Life, new Vector3(0.0f, 0.0f, 0.0f), 
                Quaternion.identity, transform) as GameObject;
            obj.transform.parent = transform;
            obj.transform.localPosition = new Vector3(i, 0.0f, 0.0f);
            if(i >= TotalLives)
            {
                Color temp = obj.GetComponent<SpriteRenderer>().color;
                temp.a = 0.35f;
                obj.GetComponent<SpriteRenderer>().color = temp;
                //obj.GetComponent<SpriteRenderer>().color
            }
        }
    }
}
