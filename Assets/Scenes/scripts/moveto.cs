using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveTo : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public float stopDistance = 0.1f;
    public List<GameObject> targets = new List<GameObject>();
    public int targetIndex = 0;
    public float shootDelay = 2.0f; // �������� ����� ���������
    public string bearTag = "Bear"; // ��� �������
    public Button moveButton; // ������ �� ������ � ���������������� ����������

    private GameObject target;
    private bool isShooting = false; // ����, ������������, ��� ����� ���������� ��������
    private bool isMoving = false; // ����, ������������, ��� ����� ��������

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // ������������� ������ ��� ������
        agent.isStopped = true;
        animator.SetBool("isRunning", false);

        if (targets.Count > 0)
        {
            target = targets[targetIndex];
        }

        // ����������� ����� StartMovement � ������� ������� �� ������
        if (moveButton != null)
        {
            moveButton.onClick.AddListener(StartMovement);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < stopDistance)
            {
                agent.isStopped = true;
                animator.SetBool("isRunning", false);

                if (target.CompareTag(bearTag) && !isShooting)
                {
                    StartCoroutine(ShootAfterDelay());
                }
                else
                {
                    targetIndex++;
                    if (targetIndex >= targets.Count)
                    {
                        targetIndex = 0;
                    }
                    target = targets[targetIndex];
                    agent.SetDestination(target.transform.position);
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                animator.SetBool("isRunning", true);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    // ������ �������� � ����
    void StartMovement()
    {
        if (!isMoving && target != null)
        {
            isMoving = true;
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            animator.SetBool("isRunning", true);
        }
    }

    private IEnumerator ShootAfterDelay()
    {
        isShooting = true;
        animator.SetTrigger("Shoot"); // ����������� �������� ��������
        yield return new WaitForSeconds(shootDelay);
        animator.SetTrigger("Idle"); // ����� �������� ��������� � ��������� �����
        isShooting = false;
    }
}
