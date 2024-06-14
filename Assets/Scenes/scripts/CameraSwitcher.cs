using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public static CameraSwitcher Instance { get; private set; } // Singleton Instance

    public Camera introCamera; // ������ ��� ��������� �����
    public Camera mainCamera; // �������� ������, ������� ������� �� ����������
    public Camera bearKillCamera; // ������ ��� ���������� �� ��������� �������
    public Camera danceCamera; // ������ ��� ���������� �� ������
    public Camera smokeCamera; // ������ ��� ���������� �� ��������
    public Camera stoneCamera; // ������ ��� ���������� �� ������
    public Button[] actionButtons; // ������ ������ ��������

    private bool isIntroActive = true;
    private bool isBearKillCameraActive = false;
    private bool isDanceCameraActive = false;
    private bool isSmokeCameraActive = false;
    private bool isStoneCameraActive = false;

    void Awake()
    {
        // Singleton Pattern Implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // ��������, ��� ��������� ������ �������, � ��� ��������� ���������
        introCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        bearKillCamera.gameObject.SetActive(false);
        danceCamera.gameObject.SetActive(false);
        smokeCamera.gameObject.SetActive(false);
        stoneCamera.gameObject.SetActive(false);

        // ����������� ����� SwitchToMainCamera � ������� ������� �� ������ ������ ��������
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
            DeactivateAllActionCameras();
        }
        else if (isBearKillCameraActive)
        {
            mainCamera.gameObject.SetActive(true);
            bearKillCamera.gameObject.SetActive(false);
            isBearKillCameraActive = false;
        }
        else if (isDanceCameraActive)
        {
            mainCamera.gameObject.SetActive(true);
            danceCamera.gameObject.SetActive(false);
            isDanceCameraActive = false;
        }
        else if (isSmokeCameraActive)
        {
            mainCamera.gameObject.SetActive(true);
            smokeCamera.gameObject.SetActive(false);
            isSmokeCameraActive = false;
        }
        else if (isStoneCameraActive)
        {
            mainCamera.gameObject.SetActive(true);
            stoneCamera.gameObject.SetActive(false);
            isStoneCameraActive = false;
        }
    }

    public void SwitchToBearKillCamera()
    {
        mainCamera.gameObject.SetActive(false);
        bearKillCamera.gameObject.SetActive(true);
        isBearKillCameraActive = true;
    }

    public void SwitchToDanceCamera()
    {
        mainCamera.gameObject.SetActive(false);
        danceCamera.gameObject.SetActive(true);
        isDanceCameraActive = true;
    }

    public void SwitchToSmokeCamera()
    {
        mainCamera.gameObject.SetActive(false);
        smokeCamera.gameObject.SetActive(true);
        isSmokeCameraActive = true;
    }

    public void SwitchToStoneCamera()
    {
        mainCamera.gameObject.SetActive(false);
        stoneCamera.gameObject.SetActive(true);
        isStoneCameraActive = true;
    }

    private void DeactivateAllActionCameras()
    {
        bearKillCamera.gameObject.SetActive(false);
        danceCamera.gameObject.SetActive(false);
        smokeCamera.gameObject.SetActive(false);
        stoneCamera.gameObject.SetActive(false);
    }
}
