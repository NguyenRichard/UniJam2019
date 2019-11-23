using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetitBonhommeFactory : MonoBehaviour
{

    static PetitBonhommeFactory instance;

    public static PetitBonhommeFactory Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only one PetitBonhommeFactory can exist");
        }
        instance = this;
    }


    [SerializeField]
    private GameObject petitBonhommePrefabs;

    [SerializeField]
    Camera cam;

    //instancie un petit bonhomme qui pars de l'entree va choper le coffre le plus prhce a vol d'oiseau et raprs a une sortie
    public void CreatePetitBonhomme(Transform entree, Transform sortie)
    {
        GameObject petitBonhomme = Instantiate(petitBonhommePrefabs,entree) as GameObject;
        petitBonhomme.GetComponent<PetitBonhommeController>().Cam = cam;
        petitBonhomme.GetComponent<PetitBonhommeController>().Sortie = sortie.position;

    }







}
