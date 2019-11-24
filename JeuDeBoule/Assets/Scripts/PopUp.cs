using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    Sprite[] tutosImg;
    int id = 0;

    [SerializeField]
    Image image;

    bool isOpen = false;
    
    void Start()
    {
        OpenPopUp();
    }

    public void OpenPopUp()
    {
        if( id < 3)
        {
            isOpen = true;
            Time.timeScale = 0.0f;
            image.sprite = tutosImg[id];
        }
        
    }

    public void ClosePopUp()
    {
       

        if( id == 2)
        {
            isOpen = false;
            Time.timeScale = 1.0f;
            image.enabled = false;

            //TODO : Lancer le jeu
        }
        id++;

    }


    void Update()
    {

        if (Input.GetButtonDown("Confirm") && isOpen)
        {
            ClosePopUp();
            OpenPopUp();
        }   
    }

}
