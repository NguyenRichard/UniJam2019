using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGUI : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    [SerializeField]
    GameObject introMenu;

    [SerializeField]
    GameObject gameMenu;

    bool isDisappear = false;

    [SerializeField]
    string levelName;


    public void StartGame()
    {
        //isDisappear = true;
        //gameMenu.SetActive(true);
        //jauge.GetComponent<JaugeController>().Point = 10;
        SceneManager.LoadScene(levelName);
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
