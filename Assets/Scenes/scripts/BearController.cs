using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    public Animator animator;
    public float detectionRange = 2.0f;
    public GameObject Skull;  // Объект героя
    public float chaseDuration = 2.0f; // Длительность погони

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Skull != null)
        {
            float distance = Vector3.Distance(transform.position, Skull.transform.position);

            if (distance < detectionRange)
            {
                animator.SetBool("IsBuff", true);
                StartCoroutine(ChasePlayer());
            }
            else
            {
                animator.SetBool("IsBuff", false);
            }
        }
    }

    private IEnumerator ChasePlayer()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Ждем завершения анимации рычания

        navMeshAgent.SetDestination(Skull.transform.position); // Начинаем погоню за игроком
        yield return new WaitForSeconds(chaseDuration); // Погоня продолжается несколько секунд

        navMeshAgent.isStopped = true; // Останавливаем погоню
        animator.SetTrigger("Death"); // Проигрываем анимацию смерти
    }
}
