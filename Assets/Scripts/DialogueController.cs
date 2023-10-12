using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public AudioSource audioSource;
    public GameObject telephoneUI;
    //public GameObject mission1UI;
    //public GameObject mission2UI;
    //public GameObject mission3UI;
    public bool missionAccomplished = false;
    public bool dialEnd = false;

    public DialogueLine[] lines;
    private Queue<DialogueLine> dialogueQueue;
    private bool dialogueStarted = false;

    void Start()
    {
        dialogueQueue = new Queue<DialogueLine>(lines);
        StartDialogue();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && dialogueStarted)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue()
    {
        dialogueStarted = true;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence(DialogueLine line)
    {
        nameText.text = line.name;
        dialogueText.text = "";
        audioSource.clip = line.audioClip;
        audioSource.Play();
        foreach (char letter in line.sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueStarted = false;
        StartCoroutine(WaitForCloseCommand());
    }
    IEnumerator WaitForCloseCommand()
    {
        while(!Input.GetKeyDown(KeyCode.E))
        {
            yield return null;
        }
        SceneManager.LoadScene("SecondFloor");
        telephoneUI.SetActive(false);
       
        //Mission1();
    }

    //void Mission1()
    //{
    //    mission1UI.SetActive(true);
    //}

    //void Mission2()
    //{
    //    mission1UI.SetActive(false);
    //    mission2UI.SetActive(true);
    //}

    //void Mission3()
    //{
    //    mission2UI.SetActive(false);
    //    mission3UI.SetActive(true);
    //}
}

[System.Serializable]
public class DialogueLine
{
    public string name;
    public string sentence;
    public AudioClip audioClip;

}