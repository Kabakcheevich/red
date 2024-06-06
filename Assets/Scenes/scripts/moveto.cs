using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveTo : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public List<GameObject> targets = new List<GameObject>();
    public int targetIndex = 0;
    public float shootDelay = 2.0f; // �������� ����� ���������
    public string bearTag = "Bear"; // ��� �������
    public Button moveButton; // ������ �� ������ � ���������������� ����������
    public Button partyButton; // ������ �� ������ "Let's party"
    public float bearStopDistance = 15.0f; // ��������� ��������� ��� �������
    public float stoneStopDistance = 4.0f; // ��������� ��������� ��� �����

    private GameObject target;
    private bool isShooting = false; // ����, ������������, ��� ����� ���������� ��������
    private bool isMoving = false; // ����, ������������, ��� ����� ��������
    private bool isPartying = false; // ����, ������������, ��� ����� ���������� ���������
    private int partyTargetIndex; // ������ ���� ��� ����� � ������ targets

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

        // ����������� ����� StartParty � ������� ������� �� ������
        if (partyButton != null)
        {
            partyButton.onClick.AddListener(StartParty);
        }

        // ������������, ��� ������ �������� ��������� � ������ targets
        partyTargetIndex = targets.Count - 1;
    }

    void Update()
    {
        if (isMoving && target != null)
        {
            MoveToTarget();
        }
        else if (isPartying && targets[partyTargetIndex] != null)
        {
            PartyMove();
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void MoveToTarget()
    {
        if (target.CompareTag(bearTag))
        {
            agent.stoppingDistance = bearStopDistance;
        }

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < agent.stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetBool("isRunning", false);

            if (target.CompareTag(bearTag) && !isShooting)
            {
                StartCoroutine(ShootAfterDelay());
            }
            else
            {
                isMoving = false; // ��������� ��������
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

    void PartyMove()
    {
        agent.stoppingDistance = stoneStopDistance;
        GameObject partyTarget = targets[partyTargetIndex];
        float distance = Vector3.Distance(transform.position, partyTarget.transform.position);

        if (distance < agent.stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Dance");
            isPartying = false; // ��������� �����
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(partyTarget.transform.position);
            animator.SetBool("isRunning", true);
        }
    }

    void StartMovement()
    {
        if (!isMoving && !isShooting && !isPartying)
        {
            isMoving = true;
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            animator.SetBool("isRunning", true);
        }
    }

    void StartParty()
    {
        if (!isPartying && !isMoving && !isShooting)
        {
            isPartying = true;
            agent.isStopped = false;
            agent.SetDestination(targets[partyTargetIndex].transform.position);
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
        isMoving = false; // ��������� �������� � ��������
    }
}
