using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetitBonhommeController : MonoBehaviour
{
    [SerializeField]
    AudioClip laMort;
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
    protected Camera cam;

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

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
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
            gameManager.UpdateJauge(-10);
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Die();
        }

    }
    private void Die()
    {
        if (!isDead)
        {
            pilleurAnimator.Die();
            agent.speed = 0;
            agent.enabled = false;
            audioSource.clip = laMort;
            audioSource.Play();
            isDead = true;
            gameManager.UpdateJauge(5);
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

