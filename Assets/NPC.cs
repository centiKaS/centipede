using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if(dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        if(isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        if (dialogueData == null)
        {
            Debug.LogError("Dialogue data is not assigned to NPC.");
            return;
        }

        isDialogueActive = true;
        dialogueIndex = 0;

        if (nameText != null)
            nameText.SetText(dialogueData.npcName);
        if (portraitImage != null)
            portraitImage.sprite = dialogueData.npcPortrait;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
            //-- skip typing anim show full line
            StopAllCoroutines();
            if (dialogueData.dialogueLines != null && dialogueIndex >= 0 && dialogueIndex < dialogueData.dialogueLines.Length)
            {
                dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            }
            else
        if(isTyping)
        {
            //-- skip typing anim show full line
            StopAllCoroutines();
            if (dialogueData != null && dialogueData.dialogueLines != null && dialogueIndex >= 0 && dialogueIndex < dialogueData.dialogueLines.Length)
                dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (dialogueData != null && dialogueData.dialogueLines != null && dialogueIndex + 1 < dialogueData.dialogueLines.Length)
        {
            dialogueIndex++;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
            dialogueIndex++;
            StartCoroutine(TypeLine());
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        if (dialogueData == null || dialogueData.dialogueLines == null || dialogueIndex < 0 || dialogueIndex >= dialogueData.dialogueLines.Length)
        {
            isTyping = false;
            yield break;
        }

        string line = dialogueData.dialogueLines[dialogueIndex];
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        float timer = 0f;
        int i = 0;

        while (i < line.Length)
        {
            sb.Append(line[i]);
            dialogueText.text = sb.ToString();
            i++;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if(dialogueData.autoProgressLines != null && dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseController.SetPause(false);
    }
}