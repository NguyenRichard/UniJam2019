using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }


    private List<Transform> listCoffres = new List<Transform>();
    public List<Transform> ListCoffres
    {
        get { return listCoffres; }
    }

    public Vector3 GetCoffre(Vector3 position)
    {
        Vector3 coffreLePlusProche = listCoffres[0].position;
        float distanceMin = Vector3.Distance(position, coffreLePlusProche);
        foreach(var coffre in listCoffres)
        {
            if (Vector3.Distance(position, coffre.position) < distanceMin)
            {
                distanceMin = Vector3.Distance(position, coffre.position);
                coffreLePlusProche = coffre.position;
            }
        }
        return coffreLePlusProche;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only one GameManager can exist");
        }
        instance = this;


        foreach (var coffre in GameObject.FindGameObjectsWithTag("Coffre"))
        {
            listCoffres.Add(coffre.transform);
        }

        StartCoroutine(SpawnCoroutine());
    }


    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(10); //Init time

        while (true)
        {
            PetitBonhommeFactory.Instance.CreatePetitBonhomme();

            yield return new WaitForSeconds(10);
        }
    }
}
