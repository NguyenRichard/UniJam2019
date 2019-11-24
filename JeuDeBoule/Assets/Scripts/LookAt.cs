using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    Vector3 target;

    void Update()
    {
        Vector3 targetPostition = new Vector3(target.x,this.transform.position.y,target.z);

        this.transform.LookAt(targetPostition);
    }
}
