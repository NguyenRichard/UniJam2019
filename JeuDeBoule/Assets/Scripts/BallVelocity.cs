using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVelocity : MonoBehaviour
{

    [SerializeField]
    private float max_speed = 1;

    [SerializeField]
    private float dash_speed = 4;

    private bool isDashing = false;
    private Vector3 dash_direction;

    private bool grounded = false;

    private float dashEnergyBar;

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    Rigidbody rb;

    float speedX;
    float speedY;
    float speedZ;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(speedX + "," + speedZ);
        rb.velocity = new Vector3(speedX, 0, speedZ);
    }

    public void StartDash(float x, float z)
    {
        if (!isDashing)
        {
            isDashing = true;
            dash_direction = new Vector3(x, 0, z).normalized;
            speedX = dash_direction.x*dash_speed;
            speedZ = dash_direction.z*dash_speed;
            StartCoroutine("Dash");
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    public void SetSpeed(float x, float z)
    {
        if (isDashing)
        {
            return;
        }
        if (Mathf.Abs(speedX) < Mathf.Abs(max_speed * x))
        {
            speedX = Mathf.Lerp(speedX, x * max_speed, Time.deltaTime*30);
        }
        else
        {
            speedX = Mathf.Lerp(speedX, x * max_speed, Time.deltaTime/8);
        }

        if (Mathf.Abs(speedZ) < Mathf.Abs(max_speed * z))
        {

            speedZ = Mathf.Lerp(speedZ, z * max_speed, Time.deltaTime*30);
        }
        else
        {
            speedZ = Mathf.Lerp(speedZ, z * max_speed, Time.deltaTime/8);

        }
    }
}
