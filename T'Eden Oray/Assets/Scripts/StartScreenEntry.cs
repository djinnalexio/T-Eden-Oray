using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls which passage becomes active when loading the Game Scene from the Start Scene
/// </summary>

public class StartScreenEntry : MonoBehaviour
{
    [Header("Starting States")]// get the possible states the Game Scene can start in.
    [SerializeField] Passage FirstPassage;
    [SerializeField] Passage Instructions;
    [SerializeField] Passage Credits;
    [SerializeField] Passage Secret;

    Dictionary<KeyCode, Passage> gameKeys = new Dictionary<KeyCode, Passage> { };
    
    public static Passage EntryPoint;//variable to determine which passage the game scene will start with
                                     //The PassageController script will use it as the first passage

    public static void GoToStartScreen() { SceneManager.LoadScene("Start Screen"); }
    
    private void Start()
    {
        gameKeys.Add(KeyCode.B, FirstPassage);
        gameKeys.Add(KeyCode.I, Instructions);
        gameKeys.Add(KeyCode.C, Credits);
        gameKeys.Add(KeyCode.K, Secret);
        gameKeys.Add(KeyCode.Q, null);
    }

    void Update()
    {
        foreach (KeyValuePair<KeyCode,Passage> i in gameKeys)
        {
            if (Input.GetKeyDown(i.Key))
            {
                if (i.Value != null)
                {
                    EntryPoint = i.Value;
                    SceneManager.LoadScene("Game Screen");
                }
                else Application.Quit();
            }
        }
    }
}
