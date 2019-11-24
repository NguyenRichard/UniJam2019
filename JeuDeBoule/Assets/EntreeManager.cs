using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntreeManager : MonoBehaviour
{
    [SerializeField]
    Light spotlight;

    int lightNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        spotlight.enabled = false;
    }

    public void petitBonhommeLightOn()
    {
        lightNumber++;
        spotlight.color = Color.white;
        spotlight.enabled = true;
    }
    public void chevalierLightOn()
    {
        lightNumber++;
        spotlight.color = Color.red;
        spotlight.enabled = true;
    }
    public void switchOffLight()
    {
        lightNumber--;
        if (lightNumber == 0)
        {
            spotlight.enabled = false;
        }
        
    }
}
