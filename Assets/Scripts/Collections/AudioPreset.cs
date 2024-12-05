using UnityEngine;

[CreateAssetMenu(fileName = "AudioPreset", menuName = "AudioPreset")]
public class AudioPreset : ScriptableObject
{
    public AudioClip audioClip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(-3f, 3f)] public float pitch = 1f;
    public bool loop;
}