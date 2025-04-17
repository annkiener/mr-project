using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioSourceConfig
    {
        public AudioSource audioSource;
        public string voiceGroup; // identifier
    }

    public List<AudioSourceConfig> audioSourceConfigs;
    private Dictionary<string, AudioSourceConfig> audioSourcesDict;

    void Awake()
    {
        audioSourcesDict = new Dictionary<string, AudioSourceConfig>();
        foreach (var config in audioSourceConfigs)
        {
            if (config.audioSource != null && !audioSourcesDict.ContainsKey(config.voiceGroup))
            {
                audioSourcesDict[config.voiceGroup] = config;
            }
        }
    }

    public void PlaySound(string voiceGroup)
    {
        if (audioSourcesDict.TryGetValue(voiceGroup, out AudioSourceConfig config))
        {
            if (!config.audioSource.isPlaying) // Prevent overlapping
            {
                config.audioSource.Play();
                Debug.Log($"Playing sound: {voiceGroup}");
            }
        }
        else
        {
            Debug.LogError($"No AudioSource found for group: {voiceGroup}");
        }
    }
}
