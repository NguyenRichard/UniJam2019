using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    static audioManager instance;

    public static audioManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only one audioManager can exist");
        }
        instance = this;
    }
}
