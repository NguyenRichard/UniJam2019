using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetitBonhommeController : MonoBehaviour
{
    [SerializeField]
    private float normalSpeed = 0.7f;
    [SerializeField]
    private float heavySpeed = 0.33f;

    [SerializeField]
    protected Camera cam;
    public Camera Cam
    {
        set { cam = value; }
    }
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            pilleurAnimator.Die();
            agent.speed = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (Vector3.Distance(currentDestination, gameObject.transform.position) < 1 && !hasPicked)
        {
            agent.SetDestination(sortie);
            agent.speed = agent.speed * heavySpeed;
            pilleurAnimator.PickObject();
            hasPicked = true;
        }

        if (Vector3.Distance(sortie, gameObject.transform.position) < 1)
        {
            Destroy(gameObject);
        }


    }

}

