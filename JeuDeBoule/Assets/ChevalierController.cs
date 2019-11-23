using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChevalierController : MonoBehaviour
{
    [SerializeField]
    AudioClip laMort;
    [SerializeField]
    AudioClip apparition;
    [SerializeField]
    AudioClip disparition;
    [SerializeField]
    AudioClip kickTheBall;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float normalSpeed = 1.5f;
    [SerializeField]
    private float heavySpeed = 0.5f;

    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    private ChevalierAnimator chevalierAnimator;
    private Vector3 currentDestination;
    bool hasPicked = false;
    private Vector3 sortie;
    public Vector3 Sortie
    {
        set { sortie = value; }
    }

    private bool isDead = false;

    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        currentDestination = gameManager.GetCoffre(gameObject.transform.position);
        agent.SetDestination(currentDestination);
        agent.speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(currentDestination, gameObject.transform.position) < 1 && !hasPicked)
        {
            agent.SetDestination(sortie);
            agent.speed = agent.speed * heavySpeed;
          //  pilleurAnimator.PickObject();
            hasPicked = true;
        }

        if (Vector3.Distance(sortie, gameObject.transform.position) < 1)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Die();
        }

    }
    public void Die()
    {
        if (!isDead)
        {
            chevalierAnimator.Die();
            agent.speed = 0;
            agent.enabled = false;
            audioSource.clip = laMort;
            audioSource.Play();
            isDead = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("ball");
            BallVelocity ballVelocity = other.gameObject.GetComponent<BallVelocity>();
            if (ballVelocity.IsDashing)
            {
                Die();
            }
            else
            {
                Debug.Log("rebound");
                KickTheFoockingBall();
                ballVelocity.Rebound();
            }
        }
    }


    public void KickTheFoockingBall()
    {
        audioSource.clip = kickTheBall;
        audioSource.Play();
        chevalierAnimator.Kick();
    }

}
