using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that takes the raw gender-based text and corrects it with the right terms depending on the current avatar
/// If the passage is written in the variable text box, take the passage.
/// Replace any keyword (#keyword#) by using the 2D array
/// The corresponding words are on the same row and the column is determined by the current Point of View of the player
/// </summary>

public class GenderBasedText : MonoBehaviour
{
    // all longer variable pieces of text
    static string WallMessageFromFaisa = @"
<b><i>The cliff is not safe
Beware of rock slides
Manuel, if you read this,
let's meet at the altar
                    F.</i></b>

You would recognize that impeccably carved calligraphy anywhere; your sister made it this far!
She must be warning you about the earthquake from earlier.You hope that her taking the time to leave a note means that she is still alright.

You now know that she wants to meet you at the ruins. You cannot blame her for wanting to get away as far as possible from this cliff. You want to get moving as well.
";

    static string WallMessageFromManuel = @"
<b><i>CLIF_ N_T _AFE
ROC_ SLI_ES
G_ING TO THE A_TAR
               M.</i></b>

You would recognize that horrible cavemen's penmanship anywhere; your brother made it this far! 
You guess he is warning you about the part of the cliff that fell  earlier. You hope that him taking the time to leave a note means that he is still alright.

You now know that he wants to meet you at the ruins. You cannot blame him for wanting to get away as far as possible from this cliff. You want to get moving as well.
";

    static string ManuelWallAssumption = @"You assume by the condition of the stone and the vines that have grown over it that the site has not been maintained in maybe centuries.";

    static string FaisaWallAssumption = @"It looks old and worn out. This place was obviously left behind by some civilization.";

    static string ManuelWallInspection = @"You immediately bring your light closer to the wall and try to understand.You do not recognize this language.You look at the drawings.";

    static string FaisaWallInspection = @"You bring your light closer to the wall and try to get something out of it. You skip over the unreadable scribblings and immediately bring your attention to the drawings.";

    static string ManuelGloveDrawing = @"You recognize those patterns! You have the exact same on your favorite pair of gloves you got from your sister last Christmas! Where did she get them...";

    static string FaisaGloveDrawing = @"The design on these hands looks familiar to you...";

    static string FaisaReflect = @"Honestly, I was lucky to escape death <b>twice</b> with only scratches; I was not going to complain about a bag.
Fortunately, I still had the knife you gave me for my birthday. I always keep on my belt.";

    static string ManuelReflect = @"I had almost died <b>twice</b> and then just lost all I was carrying.
The only thing I still had on me was the pair of protective gloves you got me last Christmas.";

    static string FaisaStory = @"I passed out after the fall but was later woken up by the earthquake.

Miraculously, my ropes got tangled in the foliage before I could reach the ground.

As I was being shaken around in the trees by the earthquake, I saw rocks falling down the cliff! Luck must have been on my side for those rocks to fall way off of where I was.";

    static string ManuelStory = @"The first thing I remember after falling is being violently woken up by an earthquake.

As I was struggling to get any bearings, I realized that my ropes got tangled in the foliage before I could reach the ground!

Then, I heard a louder noise, and looked to the side just in time to witness a huge rock falling from the top of the cliff! Luckily they were off by a lot, or else I would not be here!";

    static string FaisaGem = @"I could not pass up the opportunity of becoming an adventurer who actually found a treasure while exploring!

I got the diamond!..........but I broke my knife while using it to carve the gem out... Sorry.";

    static string ManuelGem = @"I could not simply walk by a treasure like that and ignore it. I tried to grab it at first, but the rocks it was stuck on had ridiculously sharp edges! I had the use my gloves to hold onto the rocks and force the gemstone out. My gloves even started to rip!

I did end getting the diamond!..........but it cost me my favorite pair of gloves! Oh well...";

    static string ManuelResponse = "... <color=\"red\">S</color>" + @"cold her to be more careful next time.
... " + "<color=\"red\">I</color>" + @"nform her about your hardships.
... " + "<color=\"red\">L</color>" + @"et her know it's okay now.";

    static string FaisaResponse = "... <color=\"red\">T</color>" + @"ell him not to worry about it.
... " + "<color=\"red\">I</color>" + @"nform him about the dangers of the area.
... " + "<color=\"red\">L</color>" + @"et him know it's fine. Everything is alright now.";

    static string Secret5 = "Secret 5: The name <i>Faisa</i>, derived from the Arabic name 'Faizah', means 'Successful' or 'Victorious'.";

    static string Secret2 = "Secret 2: The name <i>Manuel</i>, derived from the Arabic name 'Manual', means 'Achievement'.";

    static string ManuelResponse2 = "... <color=\"red\">T</color>" + @"ell her it's okay now.
... " + "<color=\"red\">I</color>" + @"nform her about your hardships.";

    static string FaisaResponse2 = "... <color=\"red\">T</color>" + @"ell him it's fine. Everything is alright.
... " + "<color=\"red\">I</color>" + @"nform him about the dangers of the area.";

    static string ManuelSnakeReaction = "fear.";

    static string FaisaSnakeReaction = "annoyance that this valley seems to have thrown logic out of the window just to get you.";

    //The 2D array that contains all keywords and the corresponding terms:a keyword on each row, and a gender per column
    public static string[,] GenderVariables = new string[,]//creates an array with 2 coordinates
    {
    {"#my name#", "Manuel", "Faisa" },//name of the selected character
    {"#sibling Name#", "Faisa", "Manuel" },//the name of the sibling
    {"#sibling#", "sister","brother"},//the type of sibling they are
    {"#they#", "she","he" },//lowercase pronoun of the sibling
    {"#They#", "She","He" },//Capitalized pronoun of the sibling
    {"#relation#", "little sister", "big brother" },//age relation of the sibling to the character
    {"#their#", "her","his"},//sibling's possessive article
    {"#them#", "her", "him" },//object pronouns for sibling
    {"#DescribeSibling#" , "your trustworthy  and optimistic sister was by your side.", "your reliable and clever brother was there to keep you on the right track."},// see passage ch1BA
    {"#short#", "sis", "bro" },//short title
    {"#self#", "herself", "himself" }, // more pronouns
    {"#Wall Message#", WallMessageFromFaisa, WallMessageFromManuel }, // see passage ch2F
    {"#siblinghalfname#", "sa" , "uel"}, // see passage ch2JAA
    {"#look#","admire","observe" },// how they look at the wall, see passage ch3BA
    {"#assumption#" , ManuelWallAssumption, FaisaWallAssumption}, // see passage ch3BA
    {"#inspection#" , ManuelWallInspection, FaisaWallInspection},// see passage ch3BA
    {"#glovedrawing#" , ManuelGloveDrawing, FaisaGloveDrawing},// see passage ch3BB
    {"#reflect#" , FaisaReflect, ManuelReflect}, // see passage ch3DA
    {"#snakeColor#", "red" , "blue"}, // color of the eyes of the snake and the diamond
    {"#fallstory#" , FaisaStory, ManuelStory}, //see passage ch3D
    {"#gemstory#", FaisaGem, ManuelGem},//see passage ch3E
    {"#ExpositionResponse#", ManuelResponse, FaisaResponse }, //see passage ch3E
    {"#ExpositionResponse2#", ManuelResponse2, FaisaResponse2 }, //see passage ch3E
    {"#fightOpinion#", "your sister argues that it", "it" }, //see passage ch3GA
    {"#flightReaction#", "You take your sister's hand and ", "Your brother takes your hand and you" },// see passage ch3GB
    {"#ball#", "basketball", "soccer ball" },//for size comparison to the gem
    {"snakefear", ManuelSnakeReaction, FaisaSnakeReaction }, // see ch3G
    {"#DiamondReaction#" , "\"Hold on Manuel! The diamond is warming up! It's glowing!\"" , "\"Huh... Faisa? I think my diamond is glowing!\""},//see passage ch3I
    {"#takeDiamond#", "Your sister hands you the diamond", "You take the diamond from your brother's hands" },// see passage ch3J
    {"#GemEndSecret#", Secret5 , Secret2},//the secret at the end of a gem route depending on gender
    {"#really#", "obviously", "evidently" },//see passage ch3LA
    {"#scream#","MANUEEELLLL","FAISAAAAAAA" },//sibling calling your name
    };

    static int columnlength = GenderVariables.GetLength(0);// get the number of rows (keywords) of the array

    public static string AssignGenderText(string VariableText)// function that takes the text from the passages to modify it
    {
        for (int row = 0; row < columnlength; row++)
        {//for each index 'row', check the text for that keyword and replace it with the right term,
            // then add 1 to the index to repeat with the new keyword until all keywords have been checked and replaced in the text
            VariableText = VariableText.Replace(GenderVariables[row, 0], GenderVariables[row, PassageController.POV]);//The current gender is assigned in PassageController
            //find the keyword at this 'row' and replace it with the term at the same row and from the column of the current gender
        }
        return VariableText;//This function creates a new text, different from the original base text
    }
}
