using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    public Animator animator;
    public float detectionRange = 2.0f;
    public GameObject Skull;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Skull != null)
        {
            float distance = Vector3.Distance(transform.position, Skull.transform.position);

            if (distance < detectionRange)
            {
                animator.SetBool("IsBuff", true);
            }
            else
            {
                animator.SetBool("IsBuff", false);
            }
        }
    }
}
