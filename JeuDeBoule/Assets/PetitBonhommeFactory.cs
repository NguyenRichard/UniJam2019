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

    public void createPetitBonhomme()
    {
        GameObject petitBonhomme = Instantiate(petitBonhommePrefabs,depart) as GameObject;
    }







}
