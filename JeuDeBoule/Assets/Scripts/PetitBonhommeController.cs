using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetitBonhommeController : MonoBehaviour
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
    private AudioSource audioSource;
    [SerializeField]
    private float normalSpeed = 0.7f;
    [SerializeField]
    private float heavySpeed = 0.33f;

    [SerializeField]
    private Collider collider;

    [SerializeField]
    protected NavMeshAgent agent;

    [SerializeField]
    private PilleurAnimator pilleurAnimator;
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
       /* if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }*/

        if (Vector3.Distance(currentDestination, gameObject.transform.position) < 1 && !hasPicked)
        {
            agent.SetDestination(sortie);
            agent.speed = agent.speed * heavySpeed;
            pilleurAnimator.PickObject();
            hasPicked = true;
        }

        if (Vector3.Distance(sortie, gameObject.transform.position) < 1)
        {
            GameManager.Instance.UpdateJaugeScore(-10);
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Die();
        }

    }
    private void DeathSound()
    {
        int i = Random.Range(0, 4);
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

    private void Die()
    {
        if (!isDead)
        {
            pilleurAnimator.Die();
            agent.speed = 0;
            agent.enabled = false;
            DeathSound();
            isDead = true;
            GameManager.Instance.UpdateJaugeScore(5);
            Destroy(gameObject, 30);
            collider.enabled = false;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            Die();
        }
    }

}

