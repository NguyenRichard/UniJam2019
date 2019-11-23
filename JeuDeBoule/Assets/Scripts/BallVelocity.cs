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
        if (grounded)
        {
            if (!isDashing)
            {
                speedX = (Mathf.Abs(rb.velocity.x) > max_speed) ? max_speed * Mathf.Sign(rb.velocity.x) : rb.velocity.x;
                speedZ = (Mathf.Abs(rb.velocity.z) > max_speed) ? max_speed * Mathf.Sign(rb.velocity.z) : rb.velocity.z;

                rb.velocity = new Vector3(speedX, 0, speedZ);
            }
            else
            {
                rb.velocity = dash_direction * dash_speed;
            }
        }


    }

    public void StartDash(float x, float z)
    {
        if (!isDashing)
        {
            isDashing = true;
            dash_direction = new Vector3(x, 0, z).normalized;
            StartCoroutine("Dash");
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.8f);
        isDashing = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
}
