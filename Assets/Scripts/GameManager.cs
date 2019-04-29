using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject TutorialRoom = null;
    [SerializeField]
    private GameObject Room = null;

    private GameObject CurrentRoom = null;
    private GameObject LastRoom = null;

    [SerializeField]
    private Camera GameCamera = null;
    [SerializeField]
    private float CameraSpeed = 5.0f;
    private float CameraPercentage = 0.0f;
    private bool MovingCamera = false;

    private int Score = 0;


    [SerializeField]
    private Image Renderer = null;

    private int TotalRooms = 0;

    [SerializeField]
    TextAsset source;
    private List<string> CatNames = null;

    [SerializeField]
    private GameObject RipText = null;

    private void Start()
    {
        StartCoroutine(ScreenFadeIn(1));
        LastRoom = new GameObject();
        CurrentRoom = new GameObject();
        // Instantiate tutorial room and add room
        CurrentRoom = Instantiate(TutorialRoom,
            CurrentRoom.transform.position + (TotalRooms * new Vector3(18.0f, 0.0f)),
            Quaternion.identity) as GameObject;
        TotalRooms++;

        LoadCatNames();
    }

    private void LoadCatNames()
    {
        CatNames = new List<string>(source.text.Split('\n'));
    }

    public void LoadNextRoom()
    {
        // Loads the next room
        // Save old room
        LastRoom = CurrentRoom;
        // Instantiate next room
        CurrentRoom = Instantiate(Room,
            CurrentRoom.transform.position + new Vector3(18.0f, 0.0f), 
            Quaternion.identity) as GameObject;

        // Prepare Camera to move
        CameraPercentage = 0.0f;
        // Inc Total Rooms
        TotalRooms++;
    }

    public void Update()
    {
        if(CameraPercentage < 1.0f)
        {
            // Get start and goal
            Vector3 start = new Vector3(LastRoom.transform.position.x,
                LastRoom.transform.position.y, -10.0f);
            Vector3 goal = new Vector3(CurrentRoom.transform.position.x,
                CurrentRoom.transform.position.y, -10.0f);
                
            // Find Lerp
            Vector3 lerp = Vector3.Lerp(start,
                goal, Mathf.Sin(CameraPercentage * Mathf.PI / 2.0f));
            // Slowly move camera
            GameCamera.transform.position = lerp;

            CameraPercentage += 0.5f * Time.deltaTime;
            if(CameraPercentage > 0.1f)
            {
                CurrentRoom.GetComponent<RoomManager>().MakeBlock();
            }

            if(CameraPercentage >= 1.0f)
            {
                GameCamera.transform.position = goal;
                Destroy(LastRoom, 2.0f);
            }
        }
        
    }

    public void IncrementScore(int score)
    {
        Score++;
    }

    public int GetScore()
    {
        return Score;
    }

    public void GameOver()
    {
        StartCoroutine(ScreenFadeOut(1));
    }

    private IEnumerator ScreenFadeOut(int speed)
    {
        //Fadeout
        for (float alpha = 0.0f; alpha < 1f; alpha += (speed * Time.deltaTime))
        {
            Color nextColor = Renderer.color;
            nextColor.a = alpha;
            Renderer.color = nextColor;

            yield return new WaitForFixedUpdate();
        }
        // Load the only scene
        SceneManager.LoadScene(0);
    }

    private IEnumerator ScreenFadeIn(int speed)
    {
        Color nextColor = Renderer.color;
        nextColor.a = 1f;
        Renderer.color = nextColor;
        //Fadeout
        for (float alpha = 1.0f; alpha > 0f; alpha -= (speed * Time.deltaTime))
        {
            nextColor = Renderer.color;
            nextColor.a = alpha;
            Renderer.color = nextColor;

            yield return new WaitForFixedUpdate();
        }
    }

    public void SpawnRIPText()
    {
        RIPText temp = Instantiate(RipText).GetComponent<RIPText>();
        int id = Random.Range(0, 201);
        temp.Initialize(CatNames[id], 18.0f * (TotalRooms - 1));
    }

}
