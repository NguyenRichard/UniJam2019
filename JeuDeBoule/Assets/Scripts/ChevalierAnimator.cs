using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChevalierAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void Kick()
    {
        StartCoroutine(KickCoroutine());
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
    }

    IEnumerator KickCoroutine()
    {
        animator.SetBool("isKicking", true);
        yield return new WaitForSeconds(.2f);
        animator.SetBool("isKicking", false);
    }
}
