// Файл: Scripts/MusicManager.cs
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic; // Источник фоновой музыки
    public AudioSource locationMusic;   // Источник музыки для локации

    private void Start()
    {
        // Запускаем фоновую музыку при старте игры
        backgroundMusic.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект, вошедший в триггер, игроком
        if (other.CompareTag("Skull"))
        {
            // Останавливаем фоновую музыку и запускаем музыку локации
            backgroundMusic.Stop();
            locationMusic.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, является ли объект, вышедший из триггера, игроком
        if (other.CompareTag("Skull"))
        {
            // Останавливаем музыку локации и запускаем фоновую музыку
            locationMusic.Stop();
            backgroundMusic.Play();
        }
    }
}

