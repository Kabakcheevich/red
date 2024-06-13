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
    public float shootDelay = 2.0f; // Задержка перед стрельбой
    public string bearTag = "Bear"; // Тег медведя
    public Button moveButton; // Ссылка на кнопку в пользовательском интерфейсе
    public Button partyButton; // Ссылка на кнопку "Let's party"
    public Button smokeButton; // Ссылка на кнопку "Smoke"
    public float bearStopDistance = 15.0f; // Дистанция остановки для медведя
    public float stoneStopDistance = 4.0f; // Дистанция остановки для камня
    public float chairStopDistance = 1.0f; // Дистанция остановки для стула
    public string chairTag = "Chair"; // Тег стула

    private GameObject target;
    private bool isShooting = false; // Флаг, показывающий, что герой собирается стрелять
    private bool isMoving = false; // Флаг, показывающий, что герой движется
    private bool isPartying = false; // Флаг, показывающий, что герой собирается танцевать
    private bool isSmoking = false; // Флаг, показывающий, что герой собирается курить
    private int partyTargetIndex; // Индекс цели для танца в списке targets
    private int chairTargetIndex; // Индекс стула в списке targets

    void Start()
    {
        animator = GetComponent<Animator>();

        // Останавливаем агента при старте
        agent.isStopped = true;
        animator.SetTrigger("Idle");

        if (targets.Count > 0)
        {
            target = targets[targetIndex];
        }

        // Привязываем метод StartMovement к событию нажатия на кнопку
        if (moveButton != null)
        {
            moveButton.onClick.AddListener(StartMovement);
        }

        // Привязываем метод StartParty к событию нажатия на кнопку
        if (partyButton != null)
        {
            partyButton.onClick.AddListener(StartParty);
        }

        // Привязываем метод StartSmoking к событию нажатия на кнопку
        if (smokeButton != null)
        {
            smokeButton.onClick.AddListener(StartSmoking);
        }

        // Предполагаем, что камень добавлен последним в список targets
        partyTargetIndex = targets.Count - 1;

        // Найдем индекс стула в списке targets
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
                isMoving = false; // Завершаем движение
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
            isPartying = false; // Завершаем танец
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
            isSmoking = false; // Завершаем движение к стулу
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
        animator.SetTrigger("Shoot"); // Проигрываем анимацию стрельбы
        yield return new WaitForSeconds(shootDelay);
        animator.SetTrigger("Idle"); // После задержки переходим в состояние покоя
        isShooting = false;
        isMoving = false; // Завершаем стрельбу и движение
    }

    private IEnumerator StartSmokingAfterSit()
    {
        yield return new WaitForSeconds(1.0f); // Ждем, пока персонаж сядет
        animator.SetTrigger("Smoke");
    }
}
