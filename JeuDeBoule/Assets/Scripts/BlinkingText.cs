using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    Text blinkingText;

    void Start()
    {
        //get the Text component
        blinkingText = GetComponent<Text>();
        //Call coroutine BlinkText on Start
        StartCoroutine(BlinkText());
    }

    //function to blink the text 
    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement
        while (true)
        {
            blinkingText.color = new Color(255, 255, 255);
            yield return new WaitForSeconds(6f);

            blinkingText.color = new Color(0, 0, 0);
            yield return new WaitForSeconds(0.1f);
            blinkingText.color = new Color(255, 255, 255);
            yield return new WaitForSeconds(0.1f);
            blinkingText.color = new Color(0, 0, 0);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
