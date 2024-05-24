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
        // Отключаем автоматическое воспроизведение анимации
        animator.enabled = false;
        // Добавляем обработчик события нажатия на кнопку
        button.onClick.AddListener(PlayAnimation);
    }

    void PlayAnimation()
    {
        // Включаем анимацию
        animator.enabled = true;
        // Запускаем анимацию
        animator.Play("KillButton"); // Замените "YourAnimationName" на имя вашей анимации
        // Можно добавить дополнительные действия по окончании анимации, если нужно
    }
}
