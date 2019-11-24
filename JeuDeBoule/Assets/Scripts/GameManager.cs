using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField]
    GameObject jaugeScore;

    JaugeController jaugeController;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private List<GameObject> listEntrees2 = new List<GameObject>();
    private List<Transform> listSorties = new List<Transform>();
    private List<Transform> listCoffres = new List<Transform>();

    // retourne le coffre le plus proche a vol d'oiseau
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

    public void UpdateJauge(float value)
    {
        jaugeController.Point += value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only one GameManager can exist");
        }
        instance = this;

        //On remplis les listes de coffres , d'entree et de sortie au debut (avec les tags correspondant)
        foreach (var coffre in GameObject.FindGameObjectsWithTag("Coffre"))
        {
            listCoffres.Add(coffre.transform);
        }

        foreach (var sortie in GameObject.FindGameObjectsWithTag("Sortie"))
        {
            listSorties.Add(sortie.transform);
        }
        foreach (var entree in GameObject.FindGameObjectsWithTag("Entree"))
        {
            listEntrees2.Add(entree);
        }

        // Retrieve jauge controller
        jaugeController = jaugeScore.GetComponent<JaugeController>();

        StartCoroutine(SpawnPetitBonhommeCoroutine());
        StartCoroutine(SpawnChevalierCoroutine());
    }


    IEnumerator SpawnPetitBonhommeCoroutine()
    {
        yield return new WaitForSeconds(2); //Init time

        while (true)
        {
            //On choisis une entree et une sortie aleatoirement
            int i = Random.Range(0, listEntrees2.Count);
            int j = Random.Range(0, listSorties.Count);
            PetitBonhommeFactory.Instance.CreatePetitBonhomme(listEntrees2[i].transform, listSorties[j]);
            //Delai de 5s entre chaque spawn de bonhomme
            yield return new WaitForSeconds(5);
        }
    }


    IEnumerator SpawnChevalierCoroutine()
    {
        yield return new WaitForSeconds(4); //Init time

        while (true)
        {
            //On choisis une entree et une sortie aleatoirement
            int i = Random.Range(0, listEntrees2.Count);
            int j = Random.Range(0, listSorties.Count);
            PetitBonhommeFactory.Instance.CreateChevalier(listEntrees2[i].transform, listSorties[j]);
            //Delai de 5s entre chaque spawn de bonhomme
            yield return new WaitForSeconds(10);
        }
    }

    public void Defeat()
    {
        SceneManager.LoadScene("GameOver");

    }
}
