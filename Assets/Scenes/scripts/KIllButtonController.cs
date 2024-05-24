using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimationController : MonoBehaviour
{
    private Animator animator;
    private Button button;

    void Start()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        // ��������� �������������� ��������������� ��������
        animator.enabled = false;
        // ��������� ���������� ������� ������� �� ������
        button.onClick.AddListener(PlayAnimation);
    }

    void PlayAnimation()
    {
        // �������� ��������
        animator.enabled = true;
        // ��������� ��������
        animator.Play("KillButton"); // �������� "YourAnimationName" �� ��� ����� ��������
        // ����� �������� �������������� �������� �� ��������� ��������, ���� �����
    }
}
