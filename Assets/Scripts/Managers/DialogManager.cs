using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class DialogueManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject portrait;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;

    [Header("Globals inkJSON")]
    [SerializeField] TextAsset globals;

    //[SerializeField] private GameObject pauseButton;

    private Story currentStory;
    private bool dialogueIsPlaying;
    private bool canContinueToNextLine = false;
    private bool canSkip = false;
    private string lineDisplayed;
    private Coroutine displayLineCoroutine;

    private static DialogueManager instace;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string SFX_TAG = "sfx";

    private static DialogueVariables dialogueVariables;

    private void Awake()
    {
        if (instace != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instace = this;
        layoutAnimator = dialog.GetComponent<Animator>();
        dialogueVariables = new DialogueVariables(globals);
    }
    
    public static DialogueManager GetInstance()
    {
        return instace;
    }

    private void Start()
    {
        // Start playing the dialog
        dialogueIsPlaying = false;
        dialog.SetActive(false);
        HideChoices();
        
        // Get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // continue to the next line in the dialogue
        if (canContinueToNextLine && InputManager.GetInstance().GetSubmitPressed() && currentStory.currentChoices.Count == 0)
        {
            audioManager.PlayNextLine();
            ContinueStory();
        }
        else if (!canContinueToNextLine && canSkip && InputManager.GetInstance().GetSubmitPressed() && currentStory.currentChoices.Count == 0)
        {
            audioManager.PlayNextLine();
            dialogText.maxVisibleCharacters = lineDisplayed.Length;
            StopCoroutine(displayLineCoroutine);
            canContinueToNextLine = true;
        }
    }
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialog.SetActive(true);
        // This is  because the animation "centre" leaves the portrait object inactive after playing.
        // Therefore if you play "l"eft" or "right" animations, the portrait animator couldn't be played because the portrait object was inactive
        portrait.SetActive(true);
        dialogueIsPlaying = true;

        dialogueVariables.StartListening(currentStory);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialog.SetActive(false);
        dialogText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            HandleTags(currentStory.currentTags);
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogText.text = line;
        lineDisplayed = line;
        dialogText.maxVisibleCharacters = 0;
        canSkip = false;
        canContinueToNextLine = false;

        for (int i = 1; i < line.Length; i++)
        {
            yield return new WaitForSeconds(typingSpeed);
            dialogText.maxVisibleCharacters = i;
            if (i > 3)
            {
                canSkip = true;
            }
        }

        canContinueToNextLine = true;
    }
    private void HandleTags(List<string> currenTags)
    {
        foreach (string tag in currenTags)
        {
            string[] splitTag = tag.Split(":");
            if (splitTag.Length != 2 )
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue; 
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                case SFX_TAG:
                    int sfxIndex = int.Parse(tagValue.Trim());
                    audioManager.PlaySFX(sfxIndex);
                    break;
                default:
                    Debug.LogWarning("Invalid tag");
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than the UI can support were given");
        }

        if (currentChoices.Count != 0)
        {
            dialogText.text = "";
        }

        //Display the current number of choices given
        int index = 0;

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstchoice());
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private IEnumerator SelectFirstchoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            InputManager.GetInstance().RegisterSubmitPressed();
            ContinueStory();
        }
    }

    public bool DialogueIsPlaying()
    {
        return dialogueIsPlaying;
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    public void SetVariableState(string variableName, string variableValue)
    {
        if (GetVariableState(variableName) != null)
        {
            Ink.Runtime.StringValue value = new Ink.Runtime.StringValue(variableValue);
            dialogueVariables.variables[variableName] = value;
            dialogueVariables.SaveVariables();
        }
        else
        {
            Debug.LogWarning("A value can't be set because Ink Variable was found to be null: " + variableName);
        }
    }
}
