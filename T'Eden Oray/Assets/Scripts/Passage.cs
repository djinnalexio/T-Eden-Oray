using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This scripts creates the template for the scriptable objects passage.
/// They each contain a passage of the story as well as which other passages they can lead to.
/// It also contains the functions 
/// </summary>

[CreateAssetMenu(fileName = "Passage", menuName ="New Passage")]
//this will add the scriptable object template to the drop down menu.
//the option is called "New Passage" and the default name is "Passage"
public class Passage : ScriptableObject//The scripts creates the template to make custom assets 
{
    [Space(10)]
    [TextArea(15,19)] [SerializeField] [Tooltip("Write the text here if it does not contain any variable text")]
    string NeutralPassage;//Textbox for passages that do not contain gender-based text

    [TextArea(15,19)] [SerializeField] [Tooltip("Write the text here if it contains variable text")]
    string VariablePassage;//Textbox for passages that contain gender-based text

    [Space(10)]
    [SerializeField] Passage[] NextPassages;//array that contains the possible passages that directly follow the current passage

    public string GetCurrentText()
    {//function to get the text of the current passage
        if (NeutralPassage != "") { return NeutralPassage; }//get the text from the neutral box if it is there
        else
        {//if the neutral box is empty, use the text from the gender-based text after replacing the placeholders
            string ModifiedPassage = GenderBasedText.AssignGenderText(VariablePassage);
            //use the function from the GenderBasedText script to replace keywords with the right terms depending on the gender
            return ModifiedPassage;//return the corrected text
            //Creating a new different variable for the new text is necessary so that it does not overwrite the original raw passage
        }
    }

    public Passage[] GetNextPassages()
    {//function to obtain the array containing the set of connected passages
        return NextPassages;
    }

}