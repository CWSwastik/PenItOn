using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicAudioSource;
    private static float musicTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;
    }

    // Update is called once per frame
    void Update()
    {
        musicTime = musicAudioSource.time;
    }
}
