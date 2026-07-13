using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip musica;
    [SerializeField] [Range(0f, 1f)] private float volume = 0.5f;
    [SerializeField] private bool repetir = true;

    private AudioSource audioSource;

    private static MusicPlayer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = musica;
        audioSource.volume = volume;
        audioSource.loop = repetir;
        audioSource.playOnAwake = false;

        audioSource.Play();
    }
    
}