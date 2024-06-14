using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public AudioClip shootSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
}
