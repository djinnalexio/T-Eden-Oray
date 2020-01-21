using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newSection", menuName = "New Section")]

public class Section : ScriptableObject
{
    [TextArea(15, 19)]
    [SerializeField]
    [Tooltip("Write the text here if it contains variable text")]
    public string sectionText;//Textbox for passages that contain gender-based text

    [Space(10)]
    [SerializeField] Section[] nextSections;//array that contains the possible passages that directly follow the current passage

    [Space(10)]
    [SerializeField] char[] nextSectionCharacters;//array that contains the possible passages that directly follow the current passage

    public string GetCurrentText()
    {
        string adjustedText = GenderBasedText.AssignGenderText(sectionText);        
        return adjustedText;
    }
    

    public Section[] GeNextSections() { return nextSections; }
    public char[] GetNextSectionCharacters() { return nextSectionCharacters; }
}