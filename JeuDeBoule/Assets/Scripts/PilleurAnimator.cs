using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilleurAnimator : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    Transform holdingTransform;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Debug.Assert(animator, "Animtor can't be null");
        Debug.Assert(holdingTransform, "Transform can't be null");

        animator.SetBool("isWalking", true);
    }

    public void SetWalking(bool value)
    {
        animator.SetBool("isWalking", value);
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
    }

    public void PickObject(GameObject obj)
    {
        obj.transform.parent = holdingTransform;
    }
}
