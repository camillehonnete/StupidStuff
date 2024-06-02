using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diaglogueText;
    public List<Scenario> scenarios;

    public static DialogueSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(PlayScenario(0));
    }

    public IEnumerator PlayScenario(int scenarioIndex)
    {
        foreach (var dialogue in scenarios[scenarioIndex].dialogues)
        {
            diaglogueText.text = dialogue.text;
            yield return new WaitForSeconds(dialogue.timeDisplayed);
        }
        diaglogueText.text = String.Empty;
        UI.Instance.EnableEndingUI();
    }
}


[System.Serializable]
public class Scenario
{
    public List<Dialogue> dialogues;
}

[System.Serializable]
public class Dialogue
{
    [HorizontalLine(color: EColor.Red)]
    [ResizableTextArea]
    public string text;
    public float timeDisplayed;
}
