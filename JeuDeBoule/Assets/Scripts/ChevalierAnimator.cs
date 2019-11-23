using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChevalierAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void Kick()
    {
        animator.SetBool("isKicking", true);
        animator.SetBool("isKicking", false);
    }
}
