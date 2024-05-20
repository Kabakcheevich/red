using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    public Animator animator;
    public float detectionRange = 2.0f;
    public string BuffAnimation = "Buff";
    public GameObject Skull;

    // ƒобавл€ем параметр, который будет управл€ть переходами анимации
    private bool isBuff = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Skull != null)
        {
            float distance = Vector3.Distance(transform.position, Skull.transform.position);

            // ѕровер€ем рассто€ние и устанавливаем параметр "isBuff"
            if (distance < detectionRange)
            {
                isBuff = true;
            }
            else
            {
                isBuff = false;
            }

            // ”станавливаем параметр "IsBuff" в аниматоре
            animator.SetBool("isBuff", isBuff);

            // ѕроигрываем анимацию "Buff" при условии, что "isBuff" == false
            if (!isBuff)
            {
                animator.Play(BuffAnimation);
            }
        }
    }
}
