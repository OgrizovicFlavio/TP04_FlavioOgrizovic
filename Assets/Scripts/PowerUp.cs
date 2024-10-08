using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private AudioClip audioClip;
    private AudioSource powerUpAudioSource;

    private void Awake()
    {
        powerUpAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Utilites.CheckLayerInMask(playerLayerMask, other.gameObject.layer))
        {
            powerUpAudioSource.PlayOneShot(audioClip);
        }
    }
}
