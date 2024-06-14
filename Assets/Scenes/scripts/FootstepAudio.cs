using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    // ������ ��� �������� ����������� �����
    public AudioClip[] footstepClips;

    // ����� �������� ��� ������������ ������
    private AudioSource audioSource;

    // ������� ��� ������������ �������� �����
    private int currentStepIndex = 0;

    void Start()
    {
        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // ����� ��� ������������ ���������� ����� ����
    public void PlayFootstep()
    {
        if (footstepClips.Length == 0)
        {
            Debug.LogWarning("��� ����������� �����.");
            return;
        }

        // ������������� ������� ��������� � ����������� ���
        audioSource.clip = footstepClips[currentStepIndex];
        audioSource.Play();

        // ��������� � ���������� �������, ���� �������� ����� �������, �������� �������
        currentStepIndex = (currentStepIndex + 1) % footstepClips.Length;
    }
}