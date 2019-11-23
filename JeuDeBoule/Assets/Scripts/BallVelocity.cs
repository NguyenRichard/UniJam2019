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

    [SerializeField]
    private int maxDashBar = 100;
    [SerializeField]
    private int regenDashBar = 20;
    [SerializeField]
    private int dashCost = 25;
    [SerializeField]
    private float dashRegenTimeRate = 1;
    private float nextRegen;

    private int dashBar;

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
        dashBar = maxDashBar;
        rb = GetComponent<Rigidbody>();
        nextRegen = Time.time + dashRegenTimeRate;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speedX, 0, speedZ);
        regenerateDashBar();
        Debug.Log("ho");
    }

    public void StartDash(float x, float z)
    {
        if (!isDashing && dashBar >= dashCost)
        {
            isDashing = true;
            dash_direction = new Vector3(x, 0, z).normalized;
            speedX = dash_direction.x*dash_speed;
            speedZ = dash_direction.z*dash_speed;
            StartCoroutine("Dash");
            dashBar -= dashCost;
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        speedX = 0;
        speedZ = 0;
    }

    private void regenerateDashBar()
    {
        if(Time.time > nextRegen)
        {
            dashBar += 20;
            if(dashBar > maxDashBar)
            {
                dashBar = maxDashBar;
            }
            nextRegen = Time.time + dashRegenTimeRate;
        }
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
        speedX = Mathf.Lerp(speedX, x * max_speed, Time.deltaTime*2);
        speedZ = Mathf.Lerp(speedZ, z * max_speed, Time.deltaTime*2);

    }
}
