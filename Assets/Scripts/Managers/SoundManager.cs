using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource UISFXSource;
    [SerializeField] private AudioSource mouseSFXSource;
    [SerializeField] private AudioSource playerSFXSource;
    [SerializeField] private AudioSource footstepsSFXSource;
    [SerializeField] private AudioSource worldSFXSource;
    [SerializeField] private AudioSource ambienceSource;
    
    //music 
    public AudioClip cityMusic;
    public AudioClip forestMusic;
    
    //ui
    public AudioClip buttonSelectUISFX;

    //player
    public AudioClip playerJumpSFX;
    public AudioClip footstepConcreteSFX;
    public AudioClip footstepDirtSFX;
    public AudioClip footstepCarpetSFX;
    public AudioClip meowSFX1;
    public AudioClip meowSFX2;
    public AudioClip meowSFX3;
    
    //mouse
    public GameObject mouse;
    public AudioClip mouseSqueak1;
    public AudioClip mouseSqueak2;
    public AudioClip mouseSqueak3;
    
    //boss 
    public GameObject boss;
    public AudioClip bossRoar1;
    public AudioClip bossRoar2;
    public AudioClip bossSpawnSFX;
    
    //world
    public AudioClip worldSwapSFX;
    public AudioClip leverSFX;
    public AudioClip drawbridgeSFX;
    
    //ambience
    public AudioClip cityAmbience;
    public AudioClip forestAmbience;
    public AudioClip bossAmbience;
    
    private void Start()
    {
    
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCityMusic()
    {
        musicSource.clip = cityMusic;
        musicSource.Play();
    }
    
    public void PlayForestMusic()
    {
        musicSource.clip = forestMusic;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void ApplyNightmareFilter()
    {
        musicSource.GetComponent<AudioReverbFilter>().enabled = true;
        musicSource.GetComponent<AudioLowPassFilter>().enabled = true;
        musicSource.pitch = 0.85f;
        musicSource.volume = 0.425f;
        ambienceSource.GetComponent<AudioReverbFilter>().enabled = true;
        ambienceSource.GetComponent<AudioLowPassFilter>().enabled = true;
        
        
        boss.GetComponent<AudioReverbFilter>().enabled = true;
        boss.GetComponent<AudioLowPassFilter>().enabled = true;
    }

    public void LowerAmbienceVolume()
    {
        ambienceSource.volume = .535f;
    }
    public void RemoveNightMareFilter()
    {
        musicSource.GetComponent<AudioReverbFilter>().enabled = false;
        musicSource.GetComponent<AudioLowPassFilter>().enabled = false;
        musicSource.pitch = 1;
        musicSource.volume = 0.5f;
        
        ambienceSource.GetComponent<AudioReverbFilter>().enabled = false;
        ambienceSource.GetComponent<AudioLowPassFilter>().enabled = false;
        
        
        boss.GetComponent<AudioReverbFilter>().enabled = false;
        boss.GetComponent<AudioLowPassFilter>().enabled = false;
    }
   public void PlayButtonSelectSound()
    {
        UISFXSource.clip = buttonSelectUISFX;
        UISFXSource.Play();
    }
   public void PlayJumpSound()
    {
        playerSFXSource.clip = playerJumpSFX;
        playerSFXSource.PlayOneShot(playerJumpSFX);
    }

    public void PlayMeowSound(int x)
    {
        Debug.Log("meow");
        playerSFXSource.volume = 0.5f;
        switch (x)
        {
            case 1:
                playerSFXSource.clip = meowSFX1;
                break;
            case 2:
                playerSFXSource.clip = meowSFX2;
                break;
            case 3:
                playerSFXSource.clip = meowSFX3;
                break;
        }

        playerSFXSource.Play();
    }
    

    public void PlayFootstepConcreteSound()
    {
        playerSFXSource.clip = footstepConcreteSFX;
        playerSFXSource.PlayOneShot(footstepConcreteSFX);
    }
    public void PlayFootstepDirtSound()
    {
        playerSFXSource.clip = footstepDirtSFX;
        playerSFXSource.PlayOneShot(footstepDirtSFX);
    }
    public void PlayFootstepCarpetSound()
    {
        playerSFXSource.clip = footstepCarpetSFX;
        playerSFXSource.PlayOneShot(footstepCarpetSFX);
    }

    public void PlayInitialBossSound()
    {
        mouseSFXSource.clip = bossSpawnSFX;
        mouseSFXSource.PlayOneShot(bossSpawnSFX);
    }

    public void PlayWorldSwapSound()
    {
        worldSFXSource.clip = worldSwapSFX;
        worldSFXSource.PlayOneShot(worldSwapSFX);
    }

    public void PlayLeverSound()
    {
        worldSFXSource.clip = leverSFX;
        worldSFXSource.PlayOneShot(leverSFX);
    }

    public void PlayDrawBridgeSound()
    {
        worldSFXSource.clip = drawbridgeSFX;
        worldSFXSource.PlayOneShot(drawbridgeSFX);
    }
    public void PlayCityAmbience()
    {
        ambienceSource.clip = cityAmbience;
        ambienceSource.Play();
    }

    public void PlayForestAmbience()
    {
        ambienceSource.clip = forestAmbience;
        ambienceSource.Play();
    }

    public void PlayBossFightAmbience()
    {
        ambienceSource.clip = bossAmbience;
        ambienceSource.Play();
    }
    public static class FadeAudioSource {
        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }
    public void FadeSounds()
    {
        StartCoroutine(FadeAudioSource.StartFade(ambienceSource, 3.5f,  0));
    }
    
    
    
}
