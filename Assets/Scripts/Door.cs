using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool Delete = false;

    public bool IsDelete()
    {
        return Delete;
    }

    public void DeleteDoor()
    {
        StartCoroutine(FadeOutDestroy(2f));
        //Delete = true;
    }

    //private void Update()
    //{
    //    //if(Delete)
    //    //{
    //    //    Destroy(gameObject, 0.5f);
    //    //}
    //}

    private IEnumerator FadeOutDestroy(float speed)
    {
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

        //Parent
        spriteRenderers.Add(GetComponent<SpriteRenderer>());
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
