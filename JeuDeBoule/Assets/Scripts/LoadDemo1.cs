using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDemo1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButton("Submit"))
        {
            SceneManager.LoadScene("Demo1");
        }
    }
}
