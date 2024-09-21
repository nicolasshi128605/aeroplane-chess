using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        // AudioSource for background music
        private AudioSource bgmSource;

        // Dictionary to cache audio clips
        private Dictionary<string, AudioClip> bgmClips;
        private Dictionary<string, AudioClip> sfxClips;
        
        public class SoundConfig
        {
            public string name;
            public float volume;
        }

        void Awake()
        {
            // Initialize audio source for BGM
            bgmSource = gameObject.AddComponent<AudioSource>();
            // Set background music to loop
            bgmSource.loop = true;
            // Initialize dictionaries to store audio clips
            bgmClips = new Dictionary<string, AudioClip>();
            sfxClips = new Dictionary<string, AudioClip>();
            EventCenter.GetInstance().AddEventListener<SoundConfig>(Events.PlaySound, PlaySFX);
            EventCenter.GetInstance().AddEventListener(Events.PlayerTurnStart, () =>
            {
                EventCenter.GetInstance().EventTrigger(Events.PlaySound, new SoundConfig
                {
                    name = "TurnStart",
                    volume = 1f
                });
            });
        }

        private void Start()
        {
            PlayBGM("BGM");
        }

        // Play background music by name
        public void PlayBGM(string bgmName)
        {
            if (!bgmClips.ContainsKey(bgmName))
            {
                // Load BGM from Resources/BGM folder
                AudioClip clip = Resources.Load<AudioClip>("BGM/" + bgmName);
                if (clip != null)
                {
                    bgmClips[bgmName] = clip;
                }
                else
                {
                    Debug.LogWarning("BGM not found: " + bgmName);
                    return;
                }
            }

            // Play the BGM
            bgmSource.clip = bgmClips[bgmName];
            bgmSource.volume = 0.3f;
            bgmSource.Play();
        }

        // Stop the currently playing BGM
        public void StopBGM()
        {
            bgmSource.Stop();
        }

        // Play sound effect by name (with independent AudioSource)
        public void PlaySFX(SoundConfig soundConfig)
        {
            var sfxName = soundConfig.name;
            if (!sfxClips.ContainsKey(sfxName))
            {
                // Load SFX from Resources/SFX folder
                AudioClip clip = Resources.Load<AudioClip>("SFX/" + sfxName);
                if (clip != null)
                {
                    sfxClips[sfxName] = clip;
                }
                else
                {
                    Debug.LogWarning("SFX not found: " + sfxName);
                    return;
                }
            }

            // Create a new AudioSource for each SFX to avoid overlap issues
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.volume = soundConfig.volume;
            sfxSource.PlayOneShot(sfxClips[sfxName]);

            // Destroy the AudioSource component after the clip finishes playing
            Destroy(sfxSource, sfxClips[sfxName].length);
        }
    }
}