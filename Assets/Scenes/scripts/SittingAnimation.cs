using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingAnimation : MonoBehaviour
{
    public Transform chair; // ������ �� ������ �����
    public Animator animator; // ������ �� ��������� �������� �����
    private UnityEngine.AI.NavMeshAgent agent; // ������ �� ��������� Ai Navigation

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // ������������� �������� ������
            agent.isStopped = true;
            // ��������� �������� �������
            animator.SetTrigger("Sit");
        }
    }
}
