using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public float stopDistance = 0.1f;
    public List<GameObject> targets = new List<GameObject>();
    public int targetIndex = 0;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // ���� ������ ����� �� ������, ������������� ������ ����
        if (targets.Count > 0)
        {
            target = targets[targetIndex];
            agent.SetDestination(target.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);

            // �������� ���������� �� ����
            if (Vector3.Distance(transform.position, target.transform.position) < stopDistance)
            {
                animator.SetBool("isRunning", false);  // �������� �����

                targetIndex++;
                if (targetIndex >= targets.Count)
                {
                    targetIndex = 0;
                }
                target = targets[targetIndex];
            }
            else
            {
                animator.SetBool("isRunning", true);  // �������� ����
            }
        }
        else
        {
            animator.SetBool("isRunning", false);  // ���� ���� ���, �������� �����
        }
    }
}
