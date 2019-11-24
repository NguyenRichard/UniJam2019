using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChevalierController : MonoBehaviour
{
    [SerializeField]
    AudioClip laMort;
    [SerializeField]
    AudioClip laMort2;
    [SerializeField]
    AudioClip laMort3;
    [SerializeField]
    AudioClip laMort4;
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
    [SerializeField]
    private Collider collider;
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

    public void soundOfTheFoot()
    {
        audioSource.clip = apparition;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(currentDestination, gameObject.transform.position) < 1 && !hasPicked)
        {
            agent.SetDestination(sortie);
            agent.speed = agent.speed * heavySpeed;
            //  pilleurAnimator.PickObject();
            audioSource.clip = disparition;
            audioSource.Play();
            hasPicked = true;
        }

        if (Vector3.Distance(sortie, gameObject.transform.position) < 1)
        {
            GameManager.Instance.UpdateJaugeScore(-20);
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Die();
        }

    }

    private void DeathSound()
    {
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                audioSource.clip = laMort;
                break;
            case 1:
                audioSource.clip = laMort2;
                break;
            case 2:
                audioSource.clip = laMort3;
                break;
            case 3:
                audioSource.clip = laMort4;
                break;

            default:
                audioSource.clip = laMort;
                break;
        }
        audioSource.Play();
    }

    public void Die()
    {
        if (!isDead)
        {
            chevalierAnimator.Die();
            agent.speed = 0;
            agent.enabled = false;
            DeathSound();
            isDead = true;
            GameManager.Instance.UpdateJaugeScore(10);
            collider.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            BallVelocity ballVelocity = other.gameObject.GetComponent<BallVelocity>();
            if (ballVelocity.IsDashing)
            {
                ballVelocity.Rebound(1f);
                Die();
            }
            else
            {
                KickTheFoockingBall();
                ballVelocity.Rebound(1.5f);
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
