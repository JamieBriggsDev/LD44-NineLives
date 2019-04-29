using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBreed : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer Body = null;

    [SerializeField]
    private SpriteRenderer Tail = null;

    [SerializeField]
    private List<Color> Colors = new List<Color>();

    private void Start()
    {
        ChangeBreed();
    }

    public void ChangeBreed()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int id = (int)Random.Range(0, Colors.Count);
        Color color = Colors[id];

        Body.color = color;
        Tail.color = color;
    }
}
