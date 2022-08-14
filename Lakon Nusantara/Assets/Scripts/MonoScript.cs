using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonoScript : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] Sentences;
    private int Index = 0;
    public float DialogueSpeed;
    // public GameObject nextbutton;
    // private bool StartDialogue = true;
    // Start is called before the first frame update
    void Start()
    {
        NextSentences();
    }

  
    void Update()
    {
    //   if(DialogueText.text == Sentences[Index])
    //     {
    //         nextbutton.SetActive(true);
    //     }
    }
    void Next()
    {
        NextSentences();
    }

    void NextSentences()
    {
        if(Index <= Sentences.Length - 1)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentences());
        }
    }

    IEnumerator WriteSentences()
    {
        foreach(char Character in Sentences[Index].ToCharArray())
        {
            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        Index++;
    }
}
