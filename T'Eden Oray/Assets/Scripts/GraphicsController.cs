using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;// need it for the function 'bool Array.contains()'

/// <summary>
/// This script controls all the images: backgrounds, items, item boxes, death screens, snake phases, and which textbox is being used
/// The images are turned on and off based on current passage and input
/// </summary>

public class GraphicsController : MonoBehaviour
{
    //Serialize all passages where visual changes will be triggered
    // then all the images on the scene
    [Header("Graphic Changes Chapter 1")]
    [SerializeField] Passage CharacterScreen;
    [SerializeField] Passage Chapter1;
    [SerializeField] Passage SiblingFall;
    [Space(10)]
    [SerializeField] Passage[] FallDeaths;//use arrays where several passages trigger the same change
    [Space(10)]
    [SerializeField] Passage[] CaveDeaths;
    [Space(10)]
    [SerializeField] Passage CliffEarthquake;

    [Header("Graphic Changes Chapter 2")]
    [Space(20)]
    [SerializeField] Passage Chapter2;
    [SerializeField] Passage LighterSpot;
    [SerializeField] Passage BranchSpot;
    [SerializeField] Passage TorchSpot;
    [SerializeField] Passage NightFall;
    [Space(10)]
    [SerializeField] Passage[] PantherDeaths;

    [Header("Graphic Changes Chapter 3")]
    [Space(20)]
    [SerializeField] Passage Chapter3;
    [SerializeField] Passage FoundSibling;
    [SerializeField] Passage SiblingGem;
    [SerializeField] Passage SiblingTool;
    [SerializeField] Passage DropGlove;
    [SerializeField] Passage[] DropKnife;
    [Space(10)]
    [SerializeField] Passage[] LoseDiamond;
    [Space(10)]
    [SerializeField] Passage[] CalmSerpentFrames;
    [Space(10)]
    [SerializeField] Passage[] AngrySerpentFrames;
    [Space(10)]
    [SerializeField] Passage[] CloseSerpentFrames;
    [Space(10)]
    [SerializeField] Passage[] WoundedSerpentFrames;
    [Space(10)]
    [SerializeField] Passage ColoredSerpentFrame;
    
    [Header("Graphic Changes Ending")]
    [Space(20)]
    [SerializeField] Passage[] Endings;
    
    [Header("Backgrounds")]
    [Space(20)]
    [SerializeField] GameObject Cliff;
    [SerializeField] GameObject Clearing;
    [SerializeField] GameObject ClearingDark;
    [SerializeField] GameObject Altar;
    [SerializeField] GameObject Sunrise;

    [Header("Outfits")]
    [Space(20)]
    [SerializeField] GameObject BoyOutfit;
    [SerializeField] GameObject GirlOutfit;

    [Header("Player Inventory")]
    [Space(20)]
    [SerializeField] GameObject PlayerBox;
    [SerializeField] GameObject PlayerSlot1;
    [SerializeField] GameObject PlayerSlot2;
    [SerializeField] GameObject PlayerBoyBag;
    [SerializeField] GameObject PlayerGirlBag;
    [SerializeField] GameObject Lighter;
    [SerializeField] GameObject Branch;
    [SerializeField] GameObject Torch;
    public static bool LighterOn;//I use these variables that can be shared across scripts
    public static bool BranchOn;
    public static bool TorchOn;

    [Header("Sibling Inventory")]
    [Space(20)]
    [SerializeField] GameObject SiblingBox;
    [SerializeField] GameObject SiblingSlot;
    [SerializeField] GameObject SiblingBagBoy;
    [SerializeField] GameObject SiblingBagGirl;
    [SerializeField] GameObject RedDiamond;
    [SerializeField] GameObject BlueDiamond;
    [SerializeField] GameObject Knife;
    [SerializeField] GameObject Gloves;
    
    [Header("Phases")]
    [Space(20)]
    [SerializeField] GameObject CliffEnd;
    [SerializeField] GameObject CaveEnd;
    [SerializeField] GameObject ClawEnd;
    [SerializeField] GameObject SnakeDefault;
    [SerializeField] GameObject SnakeAngry;
    [SerializeField] GameObject SnakeCloser;
    [SerializeField] GameObject SnakeWounded;
    [SerializeField] GameObject SnakeBlue;
    [SerializeField] GameObject SnakeRed;

    [Header("TextBoxes")]
    [Space(20)]
    [SerializeField] GameObject MainBox;
    [SerializeField] GameObject EndBox;
    public static bool EndBoxOn;
    public static bool MainBoxOn;
    
    //Group images by category
    GameObject[] Backgrounds;
    GameObject[] PlayerInventory;
    GameObject[] SiblingInventory;
    GameObject[] Phases;
    GameObject[] TextBoxes;
    GameObject[] Outfits;

    void Blackout()// a function that deactivates all image objects
    {
        foreach (GameObject i in Backgrounds) { i.SetActive(false); }
        foreach (GameObject i in PlayerInventory) { i.SetActive(false); }
        foreach (GameObject i in SiblingInventory) { i.SetActive(false); }
        foreach (GameObject i in Phases) { i.SetActive(false); }
        foreach (GameObject i in TextBoxes) { i.SetActive(false); }
        foreach (GameObject i in Outfits) { i.SetActive(false); }
    }

    void Start()
    {//set up the categories, turn everything off, start with the clearing background and the main textbox
        Backgrounds = new GameObject[] { Cliff, Clearing, ClearingDark, Altar, Sunrise };
        PlayerInventory = new GameObject[] { PlayerBox, PlayerSlot1, PlayerSlot2, PlayerBoyBag, PlayerGirlBag, Lighter, Branch, Torch };
        SiblingInventory = new GameObject[] { SiblingBox, SiblingSlot, SiblingBagBoy, SiblingBagGirl, RedDiamond, BlueDiamond, Knife, Gloves };
        Phases = new GameObject[] { CliffEnd, CaveEnd, ClawEnd, SnakeDefault, SnakeAngry, SnakeCloser, SnakeWounded, SnakeBlue, SnakeRed };
        TextBoxes = new GameObject[] { MainBox, EndBox };
        Outfits = new GameObject[] { BoyOutfit, GirlOutfit };

        Blackout();
        
        MainBox.SetActive(true);
        Clearing.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //get the current passage and the current POV from the PassageController
        Passage CurrentPassage = PassageController.CurrentPassage;
        int POV = PassageController.POV;
        int ManuelPOV = PassageController.ManuelPOV;
        int FaisaPOV = PassageController.FaisaPOV;

        GameObject DiamondColor()//function to determine the gem color
        {
            if (POV == ManuelPOV) { return RedDiamond; }
            else { return BlueDiamond; }                
        }

        GameObject SnakeColor()//function to determine the snake color
        {
            if (POV == ManuelPOV) { return SnakeRed; }
            else { return SnakeBlue; }
        }
        //I define those here in order to access POV and CurrentPassage
        GameObject Diamond = DiamondColor();
        GameObject SnakeColored = SnakeColor();

        if (CurrentPassage == CharacterScreen) { foreach (GameObject i in Outfits) { i.SetActive(true); } }//shows the outfits during the character selection screen
        else { foreach (GameObject i in Outfits) { i.SetActive(false); } }// if not in character selection, keep them off

        if (CurrentPassage == Chapter1)//set and reset the graphics at the beginning of chapter 1
        {
            Clearing.SetActive(false);
            Cliff.SetActive(true);
            PlayerBox.SetActive(true);
            PlayerSlot1.SetActive(true);
            PlayerSlot2.SetActive(true);
            if (POV == ManuelPOV) { PlayerBoyBag.SetActive(true); }
            else if (POV == FaisaPOV) { PlayerGirlBag.SetActive(true); }
            SiblingBox.SetActive(true);
            SiblingSlot.SetActive(true);
            if (POV == ManuelPOV) { SiblingBagGirl.SetActive(true); }
            else if (POV == FaisaPOV) { SiblingBagBoy.SetActive(true); }
        }

        if (CurrentPassage == SiblingFall) { SiblingBox.SetActive(false); }

        if (FallDeaths.Contains(CurrentPassage)) { CliffEnd.SetActive(true); }
        else { CliffEnd.SetActive(false); }

        if (CaveDeaths.Contains(CurrentPassage)) { CaveEnd.SetActive(true); }
        else { CaveEnd.SetActive(false); }

        if (CurrentPassage == CliffEarthquake)
        {
            PlayerBoyBag.SetActive(false);
            PlayerGirlBag.SetActive(false);
        }

        if (CurrentPassage == Chapter2)
        {
            Cliff.SetActive(false);
            ClearingDark.SetActive(false);
            Clearing.SetActive(true);
            Lighter.SetActive(false);
            Branch.SetActive(false);
            Torch.SetActive(false);
            LighterOn = Lighter.activeSelf;
            BranchOn = Branch.activeSelf;
            TorchOn = Torch.activeSelf;
        }

        if (CurrentPassage == LighterSpot)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Lighter.SetActive(true);
                LighterOn = Lighter.activeSelf;
            }

        }

        if (CurrentPassage == BranchSpot)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Branch.SetActive(true);
                BranchOn = Branch.activeSelf;
            }

        }

        if (CurrentPassage == TorchSpot)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Torch.SetActive(true);
                TorchOn = Torch.activeSelf;
            }

        }

        if (CurrentPassage == NightFall)
        {
            Clearing.SetActive(false);
            ClearingDark.SetActive(true);
        }

        if (PantherDeaths.Contains(CurrentPassage)) { ClawEnd.SetActive(true); }
        else { ClawEnd.SetActive(false); }

        if (CurrentPassage == Chapter3)
        {
            ClearingDark.SetActive(false);
            Altar.SetActive(true);
            foreach (GameObject i in SiblingInventory) { i.SetActive(false); }                        
        }

        if (CurrentPassage == FoundSibling)
        {
            SiblingBox.SetActive(true);
            SiblingSlot.SetActive(true);
        }

        if (CurrentPassage == SiblingGem)
        {
            Diamond.SetActive(true);
        }

        if (CurrentPassage == SiblingTool)
        {
            if (POV == ManuelPOV)
            {
                Knife.SetActive(true);
            }

            else if (POV == FaisaPOV)
            {
                Gloves.SetActive(true);
            }
        }

        if (CurrentPassage == DropGlove) { Gloves.SetActive(false); }

        if (DropKnife.Contains(CurrentPassage)) { Knife.SetActive(false); }

        if (LoseDiamond.Contains(CurrentPassage)) { Diamond.SetActive(false); }

        if (CalmSerpentFrames.Contains(CurrentPassage)) { SnakeDefault.SetActive(true); }
        else { SnakeDefault.SetActive(false); }

        if (AngrySerpentFrames.Contains(CurrentPassage)) { SnakeAngry.SetActive(true); }
        else { SnakeAngry.SetActive(false); }

        if (CloseSerpentFrames.Contains(CurrentPassage)) { SnakeCloser.SetActive(true); }
        else { SnakeCloser.SetActive(false); }
        
        if (WoundedSerpentFrames.Contains(CurrentPassage)) { SnakeWounded.SetActive(true); }
        else { SnakeWounded.SetActive(false); }

        if (ColoredSerpentFrame == CurrentPassage) { SnakeColored.SetActive(true); }
        else { SnakeColored.SetActive(false); }

        if (Endings.Contains(CurrentPassage) && MainBox.activeSelf)//if an ending has been reached, the main textbox will be turned off and the ending textbox will be turned on and used
        {
            Blackout();
            Sunrise.SetActive(true);
            EndBox.SetActive(true);
            EndBoxOn = EndBox.activeSelf;
            MainBoxOn = MainBox.activeSelf;
        }
        
    }
}
