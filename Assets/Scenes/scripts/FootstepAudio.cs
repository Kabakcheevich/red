using UnityEngine;

public class FootstepAudio : MonoBehaviour
{
    // Массив для хранения аудиоклипов шагов
    public AudioClip[] footstepClips;

    // Аудио источник для проигрывания звуков
    private AudioSource audioSource;

    // Счетчик для отслеживания текущего звука
    private int currentStepIndex = 0;

    void Start()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Метод для проигрывания следующего звука шага
    public void PlayFootstep()
    {
        if (footstepClips.Length == 0)
        {
            Debug.LogWarning("Нет аудиоклипов шагов.");
            return;
        }

        // Устанавливаем текущий аудиоклип и проигрываем его
        audioSource.clip = footstepClips[currentStepIndex];
        audioSource.Play();

        // Переходим к следующему индексу, если достигли конца массива, начинаем сначала
        currentStepIndex = (currentStepIndex + 1) % footstepClips.Length;
    }
}