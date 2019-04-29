using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RIPText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro TMPMessage;
    //private void Start()
    //{
    //    TMPMessage = GetComponent<TextMeshPro>();
    //}

    public void Initialize(string name, float startPosX)
    {
        //string message = string.Format("R.I.P. {0} - {1}-2019",
        //    name, year.ToString());
        string message = "R.I.P. " + name;
        TMPMessage.text = message;
        transform.position = new Vector3(startPosX, -7.8f, 0.0f);
        StartCoroutine(FadeOutFloatUp(1));
    }

    IEnumerator FadeOutFloatUp(int speed)
    {
        //Fadeout
        for (float alpha = 1.0f; alpha > 0f; alpha -= (speed / 4.0f * Time.deltaTime))
        {
            Color nextColor = TMPMessage.color;
            nextColor.a = alpha;
            TMPMessage.color = nextColor;

            Vector3 nextMove = transform.position;
            nextMove.y += Time.deltaTime;

            transform.position = nextMove;

            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }


    //IEnumerable FloatUp(int speed)
    //{
    //    Vector3 Direction = Vector3.up;
    //    Vector3 EndGoal = transform.position + (Direction * 3);

    //    //Fadeout
    //    for (float y = 0.0f; y < 1f; y += (speed * Time.deltaTime))
    //    {
    //        Color nextColor = TMPMessage.color;
    //        nextColor.a = y;
    //        TMPMessage.color = nextColor;

    //        yield return new WaitForFixedUpdate();
    //    }
    //}
}
