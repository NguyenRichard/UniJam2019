using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeController : MonoBehaviour
{
    float point = 0;

    [SerializeField]
    int maxPoints = 100;

    [SerializeField]
    GameObject rawColor;

    RawImage rColor;

    public void Awake()
    {
        rColor = GameObject.Find("JaugeScore/Color").GetComponent<RawImage>();
    }

    public float Point
    {
        set
        {
            point = value;
            Debug.Log(point);
            ChangeDisplay();
        }
        get
        {
            return point;
        }
    }

    public void ChangeDisplay()
    {
        if (point >= maxPoints)
        {
            point = maxPoints;
            // End the game
        }
        Vector3 tempPos   = gameObject.GetComponent<RectTransform>().localPosition;
        Vector2 tempScale = gameObject.GetComponent<RectTransform>().sizeDelta;

        tempPos.y = -70f + (point / maxPoints) * (70f-0f);
        tempScale.y = 0f + (point / maxPoints) * 128f;
        gameObject.GetComponent<RectTransform>().localPosition = tempPos;
        gameObject.GetComponent<RectTransform>().sizeDelta = tempScale;

        Debug.Log((point / maxPoints) * 255);

        if (point > maxPoints / 2)
        {
            rColor.color = new Color(255, 255, 0);
        }
        else
        {
            rColor.color = new Color(255, 0, 0);
        }
    }
}
