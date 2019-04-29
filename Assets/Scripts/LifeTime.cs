using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField]
    private float TimeTillDeath = 5.0f;
    [SerializeField]
    private bool Fade = true;
    //[SerializeField]
    //private List<SpriteRenderer> Sprites = null;
    private float Counter = 0.0f;

    private void Awake()
    {
        if(Fade)
        {
            StartCoroutine(FadeOutDestroy(TimeTillDeath));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Fade)
        {
            if(Counter >= TimeTillDeath)
            {
                Counter += Time.deltaTime;
                Destroy(gameObject);

            }
        }
    }

    //IEnumerator FadeOverTime(int seconds)
    //{
    //    while(seconds > 0)
    //    {
    //        Color temp = GetComponent<SpriteRenderer>().color;
    //        GetComponent<SpriteRenderer>().color = t
    //        yield return new wait();
    //    }
    //}

    //private IEnumerator FadeOutDestroy(float speed)
    //{
    //    foreach(var item in Sprites)
    //    {
    //        SpriteRenderer imageRenderer = item;
    //        for (float alpha = 1.0f; alpha > 0f; alpha -= (speed * Time.deltaTime))
    //        {
    //            Color nextColor = imageRenderer.color;
    //            nextColor.a = alpha;
    //            imageRenderer.color = nextColor;

    //            yield return new WaitForFixedUpdate();
    //        }


    //    }
    //    Destroy(gameObject);
    //}

    private IEnumerator FadeOutDestroy(float speed)
    {
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

        //Parent
        //spriteRenderers.Add(GetComponent<SpriteRenderer>());
        int numChildren = transform.childCount;

        //Children
        for (int i = 0; i < numChildren; i++)
        {
            SpriteRenderer rend = transform.GetChild(i).GetComponent<SpriteRenderer>();
            if (rend != null)
            {
                spriteRenderers.Add(rend);
            }
        }

        //Fadeout
        for (float alpha = 1.0f; alpha > 0f; alpha -= (speed * Time.deltaTime))
        {
            Color nextColor = spriteRenderers[0].color;
            nextColor.a = alpha;

            foreach (SpriteRenderer rend in spriteRenderers)
            {
                rend.color = nextColor;
            }

            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }
}
