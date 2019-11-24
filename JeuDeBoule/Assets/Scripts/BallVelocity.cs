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
    public bool IsDashing
    {
        get { return isDashing; }
    }

    private Vector3 speed_direction;
    private Vector3 dash_direction;

    [SerializeField]
    private int maxDashBar = 100;
    [SerializeField]
    private int regenDashBar = 20;
    [SerializeField]
    private int dashCost = 25;
    [SerializeField]
    private float dashRegenTimeRate = 1;
    [SerializeField]
    private float dashDuration = 0.4f;

    private float nextRegen;
    private int dashBar;
    private float dashEnergyBar;

    [SerializeField]
    private float defeatMultiplier = 0.8f;

    [SerializeField]
    private float stunDuration = 0.3f;
    private bool isStun = false;

    [SerializeField]
    private GameObject trail;

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    Rigidbody rb;

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
        rb.velocity = new Vector3(speed_direction.x, 0, speed_direction.z);
        regenerateDashBar();
    }

    public void StartDash(float x, float z)
    {
        if (!isDashing && dashBar >= dashCost)
        {
            isDashing = true;
            dash_direction = new Vector3(x, 0, z).normalized*dash_speed;
            StartCoroutine("Dash");
            dashBar -= dashCost;
            trail.SetActive(true);
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        speed_direction = speed_direction / 2;
        trail.SetActive(false);
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
            if(speed_direction.magnitude >= max_speed * defeatMultiplier)
            {
                speed_direction = Vector3.zero;
                GameManager.Instance.Defeat();
            }
        }

    }


    public void SetSpeed(float x, float z)
    {
        if (isDashing)
        {
            return;
        }
        speed_direction = Vector3.Lerp(speed_direction,new Vector3(x, 0, z).normalized * max_speed,Time.deltaTime);

    }

    public void Rebound(float multiplier)
    {
        if (!isStun)
        {
            speed_direction = speed_direction * (-multiplier);
            isStun = true;
            StartCoroutine("Stun");
        }

    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunDuration);
        isStun = false;
    }
}
