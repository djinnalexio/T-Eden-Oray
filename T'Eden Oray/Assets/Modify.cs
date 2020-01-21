using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Modify : MonoBehaviour
{
    [SerializeField] Passage[] allPassages;

    [SerializeField] Passage[] odd;

    // Start is called before the first frame update
    void Start()
    {

    }
}


/*
        foreach (Passage i in allPassages)
        {
            if (!(i.NeutralPassage == null || i.NeutralPassage == ""))
            {
                Section thisSection = ScriptableObject.CreateInstance<Section>();

                string path = "Assets/Sections/Neutral/" + i.name.ToUpper() + ".asset";

                thisSection.sectionText = i.NeutralPassage;

                AssetDatabase.CreateAsset(thisSection, path);
            }
        }
        */
