using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    public Animator animator;
    public float detectionRange = 2.0f;
    public string BuffAnimation = "Buff";
    public GameObject Skull;

    // ��������� ��������, ������� ����� ��������� ���������� ��������
    private bool isBuff = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Skull != null)
        {
            float distance = Vector3.Distance(transform.position, Skull.transform.position);

            // ��������� ���������� � ������������� �������� "isBuff"
            if (distance < detectionRange)
            {
                isBuff = true;
            }
            else
            {
                isBuff = false;
            }

            // ������������� �������� "IsBuff" � ���������
            animator.SetBool("isBuff", isBuff);

            // ����������� �������� "Buff" ��� �������, ��� "isBuff" == false
            if (!isBuff)
            {
                animator.Play(BuffAnimation);
            }
        }
    }
}
