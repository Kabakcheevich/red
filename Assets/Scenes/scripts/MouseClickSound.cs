using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    // ��������� ��� ������������ ��� ������� ������ ����
    public AudioClip clickSound;

    // ����� �������� ��� ������������ ������
    private AudioSource audioSource;

    void Start()
    {
        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
        if (clickSound != null)
        {
            audioSource.clip = clickSound;
        }
    }

    void Update()
    {
        // �������� ������� ����� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    // ����� ��� ������������ �����
    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("��������� ��� ����� �� ����������.");
        }
    }
}