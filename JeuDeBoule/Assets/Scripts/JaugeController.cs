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
    GameObject imgColor;

    [SerializeField]
    bool isDash = false;

    [SerializeField]
    Sprite scoreYellow;

    [SerializeField]
    Sprite scoreRed;

    [SerializeField]
    bool isGodMode = false;

    Image rColor;

    public void Awake() { 
        rColor = imgColor.GetComponent<Image>();
        // Not "point" because we will update the display

        if (!isDash)
        {
            Point = maxPoints / 2;
        }
    }

    public float Point
    {
        set
        {
            point = value;
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

        if (point <= 0)
        {
            point = 0;

            if (!isGodMode || !isDash)
            {
                GameManager.Instance.Defeat();
            }
            // End the game
        }

        rColor.fillAmount = (point / maxPoints);

        if (!isDash)
        {
            if ((point / maxPoints) >= 0.5)
            {
                rColor.sprite = scoreYellow;
            }
            else
            {
                rColor.sprite = scoreRed;
            }
        }
    }
}
