using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text = null;

    private GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").
            GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        string score = "Score: " + GameManager.GetScore().ToString();
        text.SetText(score);
    }
}
