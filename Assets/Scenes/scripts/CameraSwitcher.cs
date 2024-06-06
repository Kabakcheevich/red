using UnityEngine;
using UnityEngine.UI;

public class CameraSwitcher : MonoBehaviour
{
    public Camera introCamera; // ������ ��� ��������� �����
    public Camera mainCamera; // �������� ������, ������� ������� �� ����������
    public Camera bearKillCamera; // ������ ��� ���������� �� ��������� �������
    public Button[] actionButtons; // ������ ������ ��������

    private bool isIntroActive = true;
    private bool isBearKillCameraActive = false;

    void Start()
    {
        // ��������, ��� ��������� ������ �������, � �������� � ������ �������� ������� ���������
        introCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        bearKillCamera.gameObject.SetActive(false);

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
