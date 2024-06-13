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
    public Button smokeButton; // ������ �� ������ "Smoke"
    public float bearStopDistance = 15.0f; // ��������� ��������� ��� �������
    public float stoneStopDistance = 4.0f; // ��������� ��������� ��� �����
    public float chairStopDistance = 1.0f; // ��������� ��������� ��� �����
    public string chairTag = "Chair"; // ��� �����

    private GameObject target;
    private bool isShooting = false; // ����, ������������, ��� ����� ���������� ��������
    private bool isMoving = false; // ����, ������������, ��� ����� ��������
    private bool isPartying = false; // ����, ������������, ��� ����� ���������� ���������
    private bool isSmoking = false; // ����, ������������, ��� ����� ���������� ������
    private int partyTargetIndex; // ������ ���� ��� ����� � ������ targets
    private int chairTargetIndex; // ������ ����� � ������ targets

    void Start()
    {
        animator = GetComponent<Animator>();

        // ������������� ������ ��� ������
        agent.isStopped = true;
        animator.SetTrigger("Idle");

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

        // ����������� ����� StartSmoking � ������� ������� �� ������
        if (smokeButton != null)
        {
            smokeButton.onClick.AddListener(StartSmoking);
        }

        // ������������, ��� ������ �������� ��������� � ������ targets
        partyTargetIndex = targets.Count - 1;

        // ������ ������ ����� � ������ targets
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].CompareTag(chairTag))
            {
                chairTargetIndex = i;
                Debug.Log("Chair found at index: " + chairTargetIndex);
                break;
            }
        }
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
        else if (isSmoking && targets[chairTargetIndex] != null)
        {
            SmokeMove();
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
            animator.SetTrigger("Idle");

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
            animator.SetTrigger("Run");
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
            animator.SetTrigger("Dance");
            isPartying = false; // ��������� �����
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(partyTarget.transform.position);
            animator.SetTrigger("Run");
        }
    }

    void SmokeMove()
    {
        agent.stoppingDistance = chairStopDistance;
        GameObject chairTarget = targets[chairTargetIndex];
        float distance = Vector3.Distance(transform.position, chairTarget.transform.position);

        if (distance < agent.stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetTrigger("Sit");
            StartCoroutine(StartSmokingAfterSit());
            isSmoking = false; // ��������� �������� � �����
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(chairTarget.transform.position);
            Debug.Log("Moving to chair: " + chairTarget.name);
            animator.SetTrigger("Run");
        }
    }

    void StartMovement()
    {
        if (!isMoving && !isShooting && !isPartying && !isSmoking)
        {
            isMoving = true;
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            animator.SetTrigger("Run");
        }
    }

    void StartParty()
    {
        if (!isPartying && !isMoving && !isShooting && !isSmoking)
        {
            isPartying = true;
            agent.isStopped = false;
            agent.SetDestination(targets[partyTargetIndex].transform.position);
            animator.SetTrigger("Run");
        }
    }

    void StartSmoking()
    {
        if (!isSmoking && !isMoving && !isShooting && !isPartying)
        {
            isSmoking = true;
            agent.isStopped = false;
            agent.SetDestination(targets[chairTargetIndex].transform.position);
            Debug.Log("Heading to chair at index: " + chairTargetIndex);
            animator.SetTrigger("Run");
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

    private IEnumerator StartSmokingAfterSit()
    {
        yield return new WaitForSeconds(1.0f); // ����, ���� �������� �����
        animator.SetTrigger("Smoke");
    }
}
