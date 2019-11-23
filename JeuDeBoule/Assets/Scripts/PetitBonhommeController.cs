using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetitBonhommeController : MonoBehaviour
{
    [SerializeField]
    protected Camera cam;
    public Camera Cam
    {
        set { cam = value; }
    }
    [SerializeField]
    protected NavMeshAgent agent;

    private Vector3 currentDestination;

    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        currentDestination = gameManager.GetCoffre(gameObject.transform.position);
        agent.SetDestination(currentDestination);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (Vector3.Distance(currentDestination, gameObject.transform.position) < 0.5)
        {
            //agent.SetDestination(GameManager.so)
        }


    }

}

