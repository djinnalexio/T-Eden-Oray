using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;// need it for the function 'bool Array.contains()'
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls all musics and the sound effect
/// </summary>

public class MusicManagement : MonoBehaviour
{
    // Serialize all passages that trigger sound effects and changes and the sound sources

    [Header("Trigger Scenes")]
    [SerializeField] Passage Chap1P;
    [SerializeField] Passage Chap2DayP;
    [SerializeField] Passage Chap2NightP;
    [SerializeField] Passage Chap3AltarP;
    [SerializeField] Passage[] Chap3SnakeP;
    [SerializeField] Passage[] SunriseP;
    [SerializeField] Passage[] Deaths;
    [SerializeField] Passage LighterSpot;
    [SerializeField] Passage BranchSpot;
    [SerializeField] Passage TorchSpot;
    [SerializeField] Passage SiblingGem;
    [SerializeField] Passage SiblingTool;
    [SerializeField] Passage[] menuScenes;

    [Space(10)]
    [Header("Sound Objects")]
    [SerializeField] AudioClip introduction;
    [SerializeField] AudioClip chap1;
    [SerializeField] AudioClip chap2Day;
    [SerializeField] AudioClip chap2Night;
    [SerializeField] AudioClip chap3Altar;
    [SerializeField] AudioClip chap3Snake;
    [SerializeField] AudioClip sunrise;
    [SerializeField] AudioClip objectGet;
    [SerializeField] AudioClip death;

    AudioSource player;

    Passage currentPassage;

    Passage previousPassage = null;

    private void Awake()
    {
       if (FindObjectsOfType<MusicManagement>().Length > 1) 
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
       else { DontDestroyOnLoad(gameObject); player = GetComponent<AudioSource>();}
    }

    void Start()
    {
        player.clip = introduction;
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {

        currentPassage = PassageController.CurrentPassage;//get the current passage from PassageController

        SoundScene(Chap1P, chap1);
        SoundScene(Chap2DayP, chap2Day);
        SoundScene(Chap2NightP, chap2Night);
        SoundScene(Chap3AltarP, chap3Altar);
        MultipleSoundScenes(Chap3SnakeP, chap3Snake);
        MultipleSoundScenes(Deaths, death);
        MultipleSoundScenes(SunriseP, sunrise);

        //sound effects when picking up objects
        if (currentPassage == LighterSpot && Input.GetKeyDown(KeyCode.P)) { AudioSource.PlayClipAtPoint(objectGet, Camera.main.transform.position); }

        if (currentPassage == BranchSpot && Input.GetKeyDown(KeyCode.P)) { AudioSource.PlayClipAtPoint(objectGet, Camera.main.transform.position); }

        if (currentPassage == TorchSpot && Input.GetKeyDown(KeyCode.C)) { AudioSource.PlayClipAtPoint(objectGet, Camera.main.transform.position); }

        if (currentPassage == SiblingGem && previousPassage != SiblingGem) 
        {
            AudioSource.PlayClipAtPoint(objectGet, Camera.main.transform.position);
            previousPassage = SiblingGem;
        }

        if (currentPassage == SiblingTool && previousPassage != SiblingTool) 
        {
            AudioSource.PlayClipAtPoint(objectGet, Camera.main.transform.position);
            previousPassage = SiblingTool;
        }
    }


    void SoundScene(Passage TriggerPassage, AudioClip Theme)//Create a function that takes the trigger passage and the sound
    {
        if (TriggerPassage == currentPassage && previousPassage != currentPassage)
        {
            player.Stop();
            player.clip = Theme;
            player.Play();
            previousPassage = currentPassage;
        }
    }

    void MultipleSoundScenes(Passage[] TriggerPassages, AudioClip Theme)//Create a function that takes the trigger passages and the sound
    {
        if (TriggerPassages.Contains(currentPassage) && previousPassage != currentPassage)
        {
            player.Stop();
            player.clip = Theme;
            player.Play();
            previousPassage = currentPassage;
        }
    }
}
