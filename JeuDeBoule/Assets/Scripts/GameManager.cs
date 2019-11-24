using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField]
    GameObject jaugeScore;

    [SerializeField]
    private float delayGameOver = 1;

    GameObject jaugeDash;

    JaugeController jaugeScoreController;
    JaugeController jaugeDashController;

    [SerializeField]
    private float timeFirstSpawnPetitBonhomme;
    [SerializeField]
    private float timeFirstSpawnChevalier;
    [SerializeField]
    private float delaySpawnPetitBonhomme;
    [SerializeField]
    private float delaySpawnChevalier;
    [SerializeField]
    private float timeLumiereDerriereLaPorte = 2;

    [SerializeField]
    private GameObject spawnPose;
    [SerializeField]
    private GameObject prefabSphere;

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

    public void UpdateJaugeScore(float value)
    {
        jaugeScoreController.Point += value;
    }

    public void UpdateJaugeDash(float value)
    {
        jaugeDashController.Point += value;
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
        jaugeScoreController = jaugeScore.GetComponent<JaugeController>();
        jaugeDashController  = jaugeScore.GetComponent<JaugeController>();

        StartGame();
    }

    public void StartGame()
    {
        Instantiate(prefabSphere, spawnPose.transform.position, spawnPose.transform.rotation);
        StartCoroutine(SpawnPetitBonhommeCoroutine());
        StartCoroutine(SpawnChevalierCoroutine());
        GameObject.Find("InputController").GetComponent<InputController2>().InitBall();
    }

    IEnumerator SpawnPetitBonhommeCoroutine()
    {
        yield return new WaitForSeconds(timeFirstSpawnPetitBonhomme- timeLumiereDerriereLaPorte); //Init time

        while (true)
        {
            //On choisis une entree et une sortie aleatoirement
            int i = Random.Range(0, listEntrees2.Count);
            int j = Random.Range(0, listSorties.Count);
            //swith on the light
            listEntrees2[i].GetComponent<EntreeManager>().petitBonhommeLightOn();
            yield return new WaitForSeconds(timeLumiereDerriereLaPorte);
            
            PetitBonhommeFactory.Instance.CreatePetitBonhomme(listEntrees2[i].transform, listSorties[j]);
            //Delai de 5s entre chaque spawn de bonhomme
            listEntrees2[i].GetComponent<EntreeManager>().switchOffLight();
            yield return new WaitForSeconds(delaySpawnPetitBonhomme- timeLumiereDerriereLaPorte);
        }
    }


    IEnumerator SpawnChevalierCoroutine()
    {
        yield return new WaitForSeconds(timeFirstSpawnChevalier- timeLumiereDerriereLaPorte); //Init time

        while (true)
        {
            //On choisis une entree et une sortie aleatoirement
            int i = Random.Range(0, listEntrees2.Count);
            int j = Random.Range(0, listSorties.Count);
            listEntrees2[i].GetComponent<EntreeManager>().chevalierLightOn();
            yield return new WaitForSeconds(timeLumiereDerriereLaPorte);
            PetitBonhommeFactory.Instance.CreateChevalier(listEntrees2[i].transform, listSorties[j]);
            //Delai de 5s entre chaque spawn de bonhomme
            listEntrees2[i].GetComponent<EntreeManager>().switchOffLight();
            yield return new WaitForSeconds(delaySpawnChevalier - timeLumiereDerriereLaPorte);
        }
    }

    public void Defeat()
    {

        StartCoroutine("GameOver");

    }
    
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(delayGameOver);

        SceneManager.LoadScene("GameOver");
    }
}
