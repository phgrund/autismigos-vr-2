using UnityEngine;

/// <summary>
/// Play a simple sounds using Play with volume, and pitch
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayContinuousSound : MonoBehaviour
{
    [Tooltip("The volume of the sound")]
    public float volume = 1.0f;

    [Tooltip("The range of pitch the sound is played at (-pitch, pitch)")]
    [Range(0, 1)] public float randomPitchVariance = 0.0f;

    private AudioSource audioSource = null;

    private float defaultPitch = 1.0f;

    private bool isPlaying = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip sound)
    {
        if (!sound) return;

        float randomVariance = Random.Range(-randomPitchVariance, randomPitchVariance);
        randomVariance += defaultPitch;

        audioSource.pitch = randomVariance;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.clip = sound;
        audioSource.Play();
        audioSource.pitch = defaultPitch;

        isPlaying = true;
    }

    public void Stop()
    {
        if (!isPlaying) return;
        audioSource.Stop();
        isPlaying = false;
    }
}
