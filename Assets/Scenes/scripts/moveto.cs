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
    public float smokeStopDistance = 2.0f; // Дистанция остановки для стула
    public float smokeHeightOffset = 1.0f; // Смещение по высоте для позиции курения

    private GameObject target;
    private bool isShooting = false; // Флаг, показывающий, что герой собирается стрелять
    private bool isMoving = false; // Флаг, показывающий, что герой движется
    private bool isPartying = false; // Флаг, показывающий, что герой собирается танцевать
    private bool isSmoking = false; // Флаг, показывающий, что герой собирается курить
    private int partyTargetIndex; // Индекс цели для танца в списке targets
    private int smokeTargetIndex; // Индекс цели для курения в списке targets

    void Start()
    {
        animator = GetComponent<Animator>();

        // Останавливаем агента при старте
        agent.isStopped = true;
        animator.SetBool("isRunning", false);

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

        // Привязываем метод StartSmoke к событию нажатия на кнопку
        if (smokeButton != null)
        {
            smokeButton.onClick.AddListener(StartSmoke);
        }

        // Устанавливаем индексы для танца и курения
        partyTargetIndex = targets.Count - 2; // Предпоследний элемент - камень
        smokeTargetIndex = targets.Count - 1; // Последний элемент - стул
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
        else if (isSmoking && targets[smokeTargetIndex] != null)
        {
            SmokeMove();
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
            animator.SetBool("isRunning", true);
        }
    }

    void PartyMove()
    {
        agent.stoppingDistance = stoneStopDistance;
        GameObject partyTarget = targets[partyTargetIndex]; // Используем правильный индекс для танца
        float distance = Vector3.Distance(transform.position, partyTarget.transform.position);

        if (distance < agent.stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Dance");
            isPartying = false; // Завершаем танец
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(partyTarget.transform.position);
            animator.SetBool("isRunning", true);
        }
    }

    void SmokeMove()
    {
        agent.stoppingDistance = smokeStopDistance;
        GameObject smokeTarget = targets[smokeTargetIndex]; // Используем правильный индекс для курения
        float distance = Vector3.Distance(transform.position, smokeTarget.transform.position);

        if (distance < agent.stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetBool("isRunning", false);

            // Перемещаем и поворачиваем персонажа на стул с учетом смещения по высоте
            Vector3 adjustedPosition = smokeTarget.transform.position;
            adjustedPosition.y += smokeHeightOffset;
            transform.position = adjustedPosition;
            transform.rotation = smokeTarget.transform.rotation;

            animator.SetTrigger("Smoke");
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(smokeTarget.transform.position);
            animator.SetBool("isRunning", true);
        }
    }

    void StartMovement()
    {
        if (!isMoving && !isShooting && !isPartying && !isSmoking)
        {
            isMoving = true;
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            animator.SetBool("isRunning", true);
        }
        else if (isSmoking)
        {
            StopSmoking();
            StartMovement();
        }
    }

    void StartParty()
    {
        if (!isPartying && !isMoving && !isShooting && !isSmoking)
        {
            isPartying = true;
            agent.isStopped = false;
            agent.SetDestination(targets[partyTargetIndex].transform.position);
            animator.SetBool("isRunning", true);
        }
        else if (isSmoking)
        {
            StopSmoking();
            StartParty();
        }
    }

    void StartSmoke()
    {
        if (!isSmoking && !isMoving && !isShooting && !isPartying)
        {
            isSmoking = true;
            agent.isStopped = false;
            agent.SetDestination(targets[smokeTargetIndex].transform.position);
            animator.SetBool("isRunning", true);
        }
    }

    void StopSmoking()
    {
        isSmoking = false;
        animator.SetTrigger("Idle");
        agent.isStopped = true;
        animator.SetBool("isRunning", false);
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
}

