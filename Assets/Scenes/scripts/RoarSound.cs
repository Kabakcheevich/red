using UnityEngine;

public class BearRoaring : MonoBehaviour
{
    public AudioClip roarSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayRoarSound()
    {
        audioSource.PlayOneShot(roarSound);
    }
}
