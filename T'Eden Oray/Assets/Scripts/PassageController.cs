using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//Necessary to control TextMeshPro objects
using UnityEngine.SceneManagement;
using System.Linq;// need it for the function 'bool Array.contains()'

/// <summary>
/// The script that controls how passages are displayed when the player uses the keyboard
/// 'If the player presses this button, go to that passage'
/// 
/// Because I am not using numerals as my inputs, I need to write commands for each key that can be used during the game
/// Also, because I need to make sure that keys only affect the passages they are related to, I need to list
/// all passages that use a key.
/// 
/// This script also contains the commands for special and random events
/// 
/// </summary>

public class PassageController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text object where the main text will be displayed")]
    TextMeshProUGUI MainDisplay;

    [SerializeField]
    [Tooltip("The text object where the ending passages will be displayed")]
    TextMeshProUGUI EndingDisplay;

    [SerializeField]
    [Tooltip("First passage when launching without the Start Screen Scene")]
    Passage DebugStart;

    [Header("Letter Usage Locations")]
    [Tooltip("Fill in with passages that need the corresponding letter")]
    [Space(10)]
    [SerializeField] Passage[] PassagesUsingA;
    [SerializeField] Passage[] PassagesUsingB;
    [SerializeField] Passage[] PassagesUsingC;
    [SerializeField] Passage[] PassagesUsingD;
    [SerializeField] Passage[] PassagesUsingE;
    [SerializeField] Passage[] PassagesUsingF;
    [SerializeField] Passage[] PassagesUsingG;
    [SerializeField] Passage[] PassagesUsingH;
    [SerializeField] Passage[] PassagesUsingI;
    [SerializeField] Passage[] PassagesUsingK;
    [SerializeField] Passage[] PassagesUsingL;
    [SerializeField] Passage[] PassagesUsingN;
    [SerializeField] Passage[] PassagesUsingO;
    [SerializeField] Passage[] PassagesUsingP;
    [SerializeField] Passage[] PassagesUsingR;
    [SerializeField] Passage[] PassagesUsingS;
    [SerializeField] Passage[] PassagesUsingT;
    [SerializeField] Passage[] PassagesUsingW;

    [Header("Special Passages")]
    [Space(20)]
    [Tooltip("Passages with special Events")]
    [SerializeField] Passage CharacterScreen;
    [SerializeField] Passage CaveEntrance;
    [SerializeField] Passage ClearingLighterCheck;
    [SerializeField] Passage ClearingToolSplit;
    [SerializeField] Passage LighterChallenge;
    [SerializeField] Passage SiblingItemDiverge;
    [SerializeField] Passage SiblingScenario;
    [SerializeField] Passage PointelessFight;

    public static Passage CurrentPassage;

    Passage FirstPassage = StartScreenEntry.EntryPoint;//variable that gets the EntryPoint from the Start 
    
    public static int POV = 0;// neutral Point of View
    public static int ManuelPOV = 1;// Value for the brother storyline
    public static int FaisaPOV = 2;// Value for the sister storyline
    void Start()
    {
        if(FirstPassage == null) {CurrentPassage = DebugStart;}//If the game was launched directly from the game scene, use the DebugStart passage as the first passage
        else { CurrentPassage = FirstPassage; }//if an EntryPoint is defined, use it as the first passage
        
        MainDisplay.text = CurrentPassage.GetCurrentText();//get the text from the current passage
    }


    void Update()
    {
        ManagePassage();
    }

    private void ManagePassage()
    {
        var Options = CurrentPassage.GetNextPassages();//update the possible next passages for the current passage
        /*If you press a key and the current passage uses that key:
         * the current passage will be updated to the corresponding option selected from the key press
         * 
         * The keys can also be grouped by what default option they lead to. There are the 0 or first option letters, the 1 or second option letters, up till 3
         * This limits the use of letters since a letters will be hardwired to the ordering of the options.
         * For example, the key 'T' will always trigger the first option of a passage until written overwise.
        */
        if (Input.GetKeyDown(KeyCode.A) && (PassagesUsingA.Contains(CurrentPassage)))
        {
            if (CurrentPassage == ClearingToolSplit)//special event where the ending of chapter 2 depends on the item the player is holding
            {
                if (GraphicsController.TorchOn) { CurrentPassage = Options[3]; }
                else if (GraphicsController.LighterOn) { CurrentPassage = Options[2]; }
                else if (GraphicsController.BranchOn) { CurrentPassage = Options[1]; }
                else { CurrentPassage = Options[0]; }
            }

            else if ((CurrentPassage == SiblingScenario) && (POV == ManuelPOV))//a choice only available for Manuel
            {
                CurrentPassage = Options[1];
            }
            
            else { CurrentPassage = Options[0]; }
        }

        else if (Input.GetKeyDown(KeyCode.B) && (PassagesUsingB.Contains(CurrentPassage)))
        {
            if (CurrentPassage == CharacterScreen) { POV = ManuelPOV; }//Press B at the beginning to set the POV to Manuel's and move on the Chapter 1
            CurrentPassage = Options[0];
        }

        else if (Input.GetKeyDown(KeyCode.E) && (PassagesUsingE.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[0];
        }

        else if (Input.GetKeyDown(KeyCode.G) && (PassagesUsingG.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[0];
        }

        else if (Input.GetKeyDown(KeyCode.N) && (PassagesUsingN.Contains(CurrentPassage)))
        {
            if (CurrentPassage == PointelessFight)// special event for the knife fight against the snake
            {//have a random number 1, 2, or 3, and if it equals to 1, Faisa will hit the snake.
                int CriticalHit = Random.Range(1, 4);
                if (CriticalHit == 1) { CurrentPassage = Options[1]; }
                else { CurrentPassage = Options[0]; }
            }
            else { CurrentPassage = Options[0]; }

        }

        else if (Input.GetKeyDown(KeyCode.O) && (PassagesUsingO.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[0];
        }

        else if (Input.GetKeyDown(KeyCode.S) && (PassagesUsingS.Contains(CurrentPassage)))
        {
            if (CurrentPassage == CharacterScreen) { POV = FaisaPOV; }//Press S at the beginning to set the POV to Faisa's and move on the Chapter 1
            CurrentPassage = Options[0];
        }

        else if (Input.GetKeyDown(KeyCode.T) && (PassagesUsingT.Contains(CurrentPassage)))
        {
            if (CurrentPassage == SiblingItemDiverge)//special event to pick which event the sibling gets in Chapter 3
            {
                int SiblingItemSplit = Random.Range(1, 3);//1 or 2, basically 50% to get the gem route or the sibling tool route.
                if (SiblingItemSplit == 1) { CurrentPassage = Options[1]; }
                else { CurrentPassage = Options[0]; }
            }
            
            else { CurrentPassage = Options[0]; }           
        }

        else if (Input.GetKeyDown(KeyCode.W) && (PassagesUsingW.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[0];
        }

        else if (Input.GetKeyDown(KeyCode.C) && (PassagesUsingC.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[1];
        }

        else if (Input.GetKeyDown(KeyCode.H) && (PassagesUsingH.Contains(CurrentPassage)))
        {
            if ((CurrentPassage == ClearingLighterCheck) && (GraphicsController.LighterOn == false))
                //check if the player picked up the lighter. If they did not, lead to a scene that hints to backtrack
            { CurrentPassage = Options[2]; }

            else { CurrentPassage = Options[1]; }
        }

        else if (Input.GetKeyDown(KeyCode.I) && (PassagesUsingI.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[1];
        }

        else if (Input.GetKeyDown(KeyCode.K) && (PassagesUsingK.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[1];
        }

        else if (Input.GetKeyDown(KeyCode.P) && (PassagesUsingP.Contains(CurrentPassage)))
        {
            if (CurrentPassage == CaveEntrance)//special event to randomly decide which way the player dies in the cave
            {
                int CaveDanger = Random.Range(1, 4);//option 1, 2, or 3
                CurrentPassage = Options[CaveDanger];
            }

            else if (CurrentPassage == LighterChallenge)//special event at the end of Chapter 2 if the player only has the lighter.
                //60% chance to successfully avoid the panther with only the lighter
            {
                int PantherDiceRoll = Random.Range(1, 6);
                if (PantherDiceRoll >= 3) { CurrentPassage = Options[0]; }
                else { CurrentPassage = Options[1]; }
            }
            else { CurrentPassage = Options[1]; }
        }

        else if (Input.GetKeyDown(KeyCode.D) && (PassagesUsingD.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[2];
        }

        else if (Input.GetKeyDown(KeyCode.L) && (PassagesUsingL.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[2];
        }

        else if (Input.GetKeyDown(KeyCode.R) && (PassagesUsingR.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[2];
        }

        else if (Input.GetKeyDown(KeyCode.F) && (PassagesUsingF.Contains(CurrentPassage)))
        {
            CurrentPassage = Options[3];
        }

        else if (Input.GetKeyDown(KeyCode.M))//At any time on the game, you can press 'M' to go back to the Start Scene
        {
            SceneManager.LoadScene("Start Screen");
        }
        
        if (GraphicsController.EndBoxOn)//If the Ending textbox has been activated, print the current text there
        {
            EndingDisplay.text = CurrentPassage.GetCurrentText();
        }
        
        MainDisplay.text = CurrentPassage.GetCurrentText();//each frame, update the current text on the main box
    }

}
