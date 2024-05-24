using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    public Animator animator;
    public float detectionRange = 2.0f;
    public GameObject Skull;  // ������ �����
    public float chaseDuration = 2.0f; // ������������ ������

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
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // ���� ���������� �������� �������

        navMeshAgent.SetDestination(Skull.transform.position); // �������� ������ �� �������
        yield return new WaitForSeconds(chaseDuration); // ������ ������������ ��������� ������

        navMeshAgent.isStopped = true; // ������������� ������
        animator.SetTrigger("Death"); // ����������� �������� ������
    }
}
