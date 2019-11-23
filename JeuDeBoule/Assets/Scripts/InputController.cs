using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private GameObject testJoystick;

    [SerializeField]
    private Transform dungeon;

    [SerializeField]
    private GameObject ball;

    private BallVelocity ballVelocity;

    private Renderer renderer;

    private float max_degree = 15;

    private void Start()
    {
        renderer = testJoystick.GetComponent<Renderer>();
        ballVelocity = ball.GetComponent<BallVelocity>();
        Debug.Assert(ballVelocity, "You must add BallVelocity component to the ball");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
             renderer.material.color = Color.green;
        }
        if (Input.GetButtonUp("Jump"))
        {
            renderer.material.color = Color.white;
        }
        if (Input.GetButtonDown("Dash"))
        {
            renderer.material.color = Color.red;

        }
        if (Input.GetButtonUp("Dash"))
        {
            renderer.material.color = Color.white;
        }

        UpdateDungeonPose();

        Dash();


    }

    void UpdateDungeonPose()
    {
        float posX = Input.GetAxis("Horizontal");
        float posY = Input.GetAxis("Vertical");

        dungeon.eulerAngles = new Vector3(convertToDegree(posY), 0, -convertToDegree(posX));
        //testJoystick.transform.position = new Vector2(posX, posY);

    }

    void Dash()
    {
        float posX = Input.GetAxisRaw("DHorizontal");
        float posY = Input.GetAxisRaw("DVertical");
        if(posX != 0 || posY != 0)
        {
            ballVelocity.StartDash(posX, posY);
        }
    }

    /// <summary>
    /// Convert GetAxis [-1;1] to [-max_degree, max_degree] in degrees.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    float convertToDegree(float value)
    {
        return value * max_degree;
    }


}
