using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public Camera introCamera; // Камера для начальной сцены
    public Camera mainCamera; // Основная камера, которая следует за персонажем
    public Camera bearKillCamera; // Камера для наблюдения за убийством медведя
    public Button[] actionButtons; // Массив кнопок действий

    private bool isIntroActive = true;
    private bool isBearKillCameraActive = false;

    void Start()
    {
        // Убедимся, что начальная камера активна, а основная и камера убийства медведя отключены
        introCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        bearKillCamera.gameObject.SetActive(false);

        // Привязываем метод SwitchToMainCamera к событию нажатия на каждую кнопку действия
        foreach (Button button in actionButtons)
        {
            button.onClick.AddListener(SwitchToMainCamera);
        }
    }

    public void SwitchToMainCamera()
    {
        if (introCamera.gameObject.activeSelf)
        {
            introCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            bearKillCamera.gameObject.SetActive(false);
        }
        else if (isBearKillCameraActive)
        {
            mainCamera.gameObject.SetActive(true);
            bearKillCamera.gameObject.SetActive(false);
            isBearKillCameraActive = false;
        }
    }

    public void SwitchToBearKillCamera()
    {
        mainCamera.gameObject.SetActive(false);
        bearKillCamera.gameObject.SetActive(true);
        isBearKillCameraActive = true;
    }
}
