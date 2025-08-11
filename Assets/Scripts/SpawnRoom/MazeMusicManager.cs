using UnityEngine;

public class MazeMusicManager : MonoBehaviour
{
    public AudioClip[] tunnelMusicClips; // music for every tunnel - index
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusicForTunnel(int tunnelIndex)
    {
        if (tunnelIndex < 0 || tunnelIndex >= tunnelMusicClips.Length)
        {
            Debug.LogWarning($"No music assigned for tunnel index {tunnelIndex}");
            return;
        }

        AudioClip clipToPlay = tunnelMusicClips[tunnelIndex];

        if (audioSource.clip == clipToPlay && audioSource.isPlaying)
        {
            return;
        }

        audioSource.clip = clipToPlay;
        audioSource.loop = true;
        audioSource.Play();

        Debug.Log($"Playing music for tunnel {tunnelIndex}: {clipToPlay.name}");
    }
}
