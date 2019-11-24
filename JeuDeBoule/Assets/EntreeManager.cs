using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntreeManager : MonoBehaviour
{
    [SerializeField]
    Light spotlight;

    // Start is called before the first frame update
    void Start()
    {
        spotlight.enabled = false;
    }

    public void petitBonhommeLightOn()
    {
        spotlight.color = Color.white;
        spotlight.enabled = true;
    }
    public void chevalierLightOn()
    {
        spotlight.color = Color.red;
        spotlight.enabled = true;
    }
    public void switchOffLight()
    {
        spotlight.enabled = false;
    }
}
