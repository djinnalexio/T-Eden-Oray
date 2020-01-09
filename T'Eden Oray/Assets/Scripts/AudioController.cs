using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;// need it for the function 'bool Array.contains()'

/// <summary>
/// This script controls all musics and the sound effect
/// </summary>

public class AudioController : MonoBehaviour
{
    // Serialize all passages that trigger sound effects and changes and the sound sources

    [Header("Sound Objects")]
    [SerializeField] AudioSource Introduction;
    [SerializeField] AudioSource Chap1;
    [SerializeField] AudioSource Chap2Day;
    [SerializeField] AudioSource Chap2Night;
    [SerializeField] AudioSource Chap3Altar;
    [SerializeField] AudioSource Chap3Snake;
    [SerializeField] AudioSource Sunrise;
    [SerializeField] AudioSource ObjectGet;
    [SerializeField] AudioSource Death;

    [Header("Trigger Scenes")]
    [Space(20)]
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


    Passage CurrentPassage;

    void Start()
    {
        Introduction.Play();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPassage = PassageController.CurrentPassage;//get the current passage from PassageController

        SoundScene(Chap1P, Chap1);
        SoundScene(Chap2DayP, Chap2Day);
        SoundScene(Chap2NightP, Chap2Night);
        SoundScene(Chap3AltarP, Chap3Altar);
        MultipleSoundScenes(Chap3SnakeP, Chap3Snake);
        MultipleSoundScenes(Deaths, Death);
        MultipleSoundScenes(SunriseP, Sunrise);

        //sound effects when picking up objects
        if (CurrentPassage == LighterSpot && Input.GetKeyDown(KeyCode.P)) { ObjectGet.Play(); }

        if (CurrentPassage == BranchSpot && Input.GetKeyDown(KeyCode.P)) { ObjectGet.Play(); }

        if (CurrentPassage == TorchSpot && Input.GetKeyDown(KeyCode.C)) { ObjectGet.Play(); }
        
        if (CurrentPassage == SiblingGem) { ObjectGet.Play(); }
        
        if (CurrentPassage == SiblingTool) { ObjectGet.Play(); }     
    }


    void SoundScene(Passage TriggerPassage, AudioSource Theme)//Create a function that takes the trigger passage and the sound
    {
        if (TriggerPassage == CurrentPassage)
        {
            Silence(Theme);
            if (Theme.isPlaying == false)
            { Theme.Play(); }
        }
    }

    void MultipleSoundScenes(Passage[] TriggerPassage, AudioSource Theme)//Create a function that takes the trigger passages and the sound
    {
        if (TriggerPassage.Contains(CurrentPassage))
        {
            Silence(Theme);
            if (Theme.isPlaying == false)
            { Theme.Play(); }
        }
    }

    void Silence(AudioSource ButThis = null)//function to turn off all tracks BUT the one that is supposed to be active
    {
        AudioSource[] AllThemes = new AudioSource[] { Introduction, Chap1, Chap2Day, Chap2Night, Chap3Altar, Chap3Snake, Sunrise, ObjectGet, Death };
        for (int i = 0; i < AllThemes.GetLength(0); i++)
        {//use the for loop to check each item of the array and turn them off if they are not the 'butthis'
            AudioSource ThisTheme = AllThemes[i];
            if ( ThisTheme != ButThis) { ThisTheme.Stop(); }
        }
    }
}
