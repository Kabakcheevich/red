using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    // Аудиоклип для проигрывания при нажатии кнопки мыши
    public AudioClip clickSound;

    // Аудио источник для проигрывания звуков
    private AudioSource audioSource;

    void Start()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        if (clickSound != null)
        {
            audioSource.clip = clickSound;
        }
    }

    void Update()
    {
        // Проверка нажатия левой кнопки мыши
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    // Метод для проигрывания звука
    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("Аудиоклип для клика не установлен.");
        }
    }
}