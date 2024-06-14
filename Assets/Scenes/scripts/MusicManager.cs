// ����: Scripts/MusicManager.cs
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // �������� ������� ������
    public AudioSource locationMusic;   // �������� ������ ��� �������

    private void Start()
    {
        // ��������� ������� ������ ��� ������ ����
        backgroundMusic.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������, �������� � �������, �������
        if (other.CompareTag("Skull"))
        {
            // ������������� ������� ������ � ��������� ������ �������
            backgroundMusic.Stop();
            locationMusic.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ���������, �������� �� ������, �������� �� ��������, �������
        if (other.CompareTag("Skull"))
        {
            // ������������� ������ ������� � ��������� ������� ������
            locationMusic.Stop();
            backgroundMusic.Play();
        }
    }
}

