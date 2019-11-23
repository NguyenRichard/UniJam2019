using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGUI : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    [SerializeField]
    GameObject introMenu;

    [SerializeField]
    GameObject gameMenu;

    // Temp
    [SerializeField]
    GameObject jauge;

    bool isDisappear = false;

    public void Awake()
    {
        introMenu.SetActive(true);
        //gameMenu.SetActive(false);
    }

    public void StartGame()
    {
        isDisappear = true;
        //gameMenu.SetActive(true);
        jauge.GetComponent<JaugeController>().Point = 10;
    }

    public void Update()
    {
        if(isDisappear)
        {
            Vector3 temp = introMenu.transform.position;
            temp.y += speed * Time.deltaTime;
            introMenu.transform.position = temp;
            introMenu.GetComponent<CanvasGroup>().alpha -= 0.05f;

            gameMenu.GetComponent<CanvasGroup>().alpha += 0.05f;

            if (introMenu.GetComponent<CanvasGroup>().alpha<=0)
            {
                isDisappear = false;
                introMenu.SetActive(false);
            }
        }
        if (Input.GetButton("Submit"))
        {
            StartGame();
        }
    }
}
