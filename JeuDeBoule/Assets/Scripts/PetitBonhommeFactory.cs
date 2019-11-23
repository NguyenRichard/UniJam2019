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
    private Transform depart;
    [SerializeField]
    private Transform sortie;
    [SerializeField]
    Camera cam;

    public void CreatePetitBonhomme()
    {
        GameObject petitBonhomme = Instantiate(petitBonhommePrefabs,depart) as GameObject;
        petitBonhomme.GetComponent<PetitBonhommeController>().Cam = cam;

    }







}
