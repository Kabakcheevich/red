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

        // Если список целей не пустой, устанавливаем первую цель
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
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < stopDistance)
            {
                agent.isStopped = true;  // Останавливаем NavMeshAgent
                animator.SetBool("isRunning", false);  // Персонаж стоит

                targetIndex++;
                if (targetIndex >= targets.Count)
                {
                    targetIndex = 0;
                }
                target = targets[targetIndex];
            }
            else
            {
                agent.isStopped = false;  // Продолжаем движение
                agent.SetDestination(target.transform.position);
                animator.SetBool("isRunning", true);  // Персонаж идет
            }
        }
        else
        {
            animator.SetBool("isRunning", false);  // Если цели нет, персонаж стоит
        }
    }
}
