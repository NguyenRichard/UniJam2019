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

    [SerializeField]
    private float defeatMultiplier = 0.8f;

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
        speedX = speedX/2;
        speedZ = speedZ/2;
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            if(Mathf.Sqrt(speedX*speedX+speedZ*speedZ) >= max_speed * defeatMultiplier)
            {
                speedX = 0;
                speedZ = 0;
                GameManager.Instance.Defeat();
            }
        }

        if (collision.gameObject.CompareTag("Chevalier"))
        {
            if (isDashing)
            {
                collision.gameObject.GetComponent<ChevalierController>().Die();
                Debug.Log("Le chevalier meurt");
            }
            else
            {
                Debug.Log("KICKING IN THE BALLZ");
                collision.gameObject.GetComponent<ChevalierController>().KickTheFoockingBall();
                speedX = - speedX * 2;
                speedZ = -speedZ * 2;
            }
        }
    }


    public void SetSpeed(float x, float z)
    {
        if (isDashing)
        {
            return;
        }
        speedX = Mathf.Lerp(speedX, x * max_speed, Time.deltaTime);
        speedZ = Mathf.Lerp(speedZ, z * max_speed, Time.deltaTime);

    }

}
