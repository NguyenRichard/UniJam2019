using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouleColorModifier : MonoBehaviour
{
    Renderer rend;

    [SerializeField]
    BallVelocity ball;


    void Start()
    {
        rend = GetComponent<Renderer>();
        Debug.Assert(rend != null, "Renderer null on sphere");
  
    }

    // Update is called once per frame
    void Update()
    {
        float value = ball.SpeedDirection.magnitude / ball.MaxSpeed;
        rend.material.SetFloat("_Blend", value);
    }
}
