using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilleurAnimator : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    Transform holdingTransform;

    [SerializeField]
    GameObject particle;

    [SerializeField]
    private GameObject tresorPrefabs;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Assert(animator, "Animtor can't be null");
        Debug.Assert(holdingTransform, "Transform can't be null");
        tresorPrefabs.SetActive(false);
        animator.SetBool("isWalking", true);
    }

    public void SetWalking(bool value)
    {
        animator.SetBool("isWalking", value);
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
        particle.SetActive(true);
    }

    public void PickObject()
    {
        tresorPrefabs.SetActive(true);

        animator.SetBool("isWalking", false);
    }
}
