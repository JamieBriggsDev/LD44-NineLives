using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private Door Door = null;
    [SerializeField]
    private List<Sprite> Doors = new List<Sprite>();
    // Lives to continue to next room
    private int ProgressionLevel = 0;

    private int CurrentLevelOnDoor = 0;

    private int EnemyCounter = 0;

    [SerializeField]
    private List<Transform> Spawns = null;

    [SerializeField]
    private List<GameObject> Enemies = null;

    [SerializeField]
    private Mark Mark = null;

    [SerializeField]
    private TilemapCollider2D Block = null;

    [SerializeField]
    private bool SpawnEnemies = true;


    void Start()
    {
        if(SpawnEnemies)
        {
            ProgressionLevel = Random.Range(1, 5);
            Door.GetComponent<SpriteRenderer>().sprite = Doors[ProgressionLevel];
            CurrentLevelOnDoor = ProgressionLevel;

            EnemyCounter = (int)Random.Range(ProgressionLevel, Spawns.Count);
        

            for(int i = 0; i < EnemyCounter; i++)
            {

                int id = Random.Range(0, Enemies.Count);
                GameObject Enemy;
                Enemy = Instantiate(Enemies[id], Spawns[i].position, Quaternion.identity) as GameObject;    
                if(i < ProgressionLevel)
                {
                    Enemy.GetComponent<Mouse>().DropLife = true;
                }
            }

        }
        else
        {
            ProgressionLevel = 1;
            Door.GetComponent<SpriteRenderer>().sprite = Doors[ProgressionLevel];
            CurrentLevelOnDoor = ProgressionLevel;
        }


    }

    public bool UpdateDoor()
    {
        if(CurrentLevelOnDoor != 0)
        {
            CurrentLevelOnDoor--;
            Door.GetComponent<SpriteRenderer>().sprite = Doors[CurrentLevelOnDoor];
        }
        else
        {
            if(Door!= null || Door.IsDelete())
                Door.DeleteDoor();
            return false;
        }

        return true;
    }


    public void Update()
    {
        int TotalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(TotalEnemies == 0)
        {
            Mark.ActivateMark();
            //Altar.GetComponent<Collision2D>().enabled = true;
        }
    }

    public void MakeBlock()
    {
        Block.enabled = true;
    }


}
