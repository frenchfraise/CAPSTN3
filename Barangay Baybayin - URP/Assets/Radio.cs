using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Radio : InteractibleObject
{
    [SerializeField]
    private string id;
    private CharacterDialogueUI characterDialogueUI;

    public CharacterSpokenToEvent onRadioSpokenToEvent = new CharacterSpokenToEvent();
    protected override void OnEnable()
    {
        base.OnEnable();
        characterDialogueUI = UIManager.instance.characterDialogueUI ? UIManager.instance.characterDialogueUI
            : FindObjectOfType<CharacterDialogueUI>();
        onRadioSpokenToEvent.AddListener(characterDialogueUI.OnCharacterSpokenTo);


    }
    protected override void OnDisable()
    {
        base.OnDisable();
        onRadioSpokenToEvent.RemoveListener(characterDialogueUI.OnCharacterSpokenTo);

    }
    protected override void OnInteract()
    {
        Debug.Log("ITS CALLED");
        int chosenIndex = 0;
        SO_Dialogues chosenDialogue = null;
        switch (WeatherManager.instance.CurrentWeather)
        {

            case Weather.Sunny:
                Debug.Log("RA");
                chosenIndex = Random.Range(0, WeatherManager.instance.sunnyDialogues.Count);
                chosenDialogue = WeatherManager.instance.sunnyDialogues[chosenIndex];
                onRadioSpokenToEvent.Invoke(id,chosenDialogue);
                break;
            case Weather.Cloudy:
                chosenIndex = Random.Range(0, WeatherManager.instance.cloudyDialogues.Count);
                chosenDialogue = WeatherManager.instance.cloudyDialogues[chosenIndex];
                onRadioSpokenToEvent.Invoke(id,chosenDialogue);
                break;
            case Weather.Rainy:
                chosenIndex = Random.Range(0, WeatherManager.instance.rainyDialogues.Count);
                chosenDialogue = WeatherManager.instance.rainyDialogues[chosenIndex];
                onRadioSpokenToEvent.Invoke(id,chosenDialogue);
                break;
            case Weather.Stormy:
                chosenIndex = Random.Range(0, WeatherManager.instance.stormyDialogues.Count);
                chosenDialogue = WeatherManager.instance.stormyDialogues[chosenIndex];
                onRadioSpokenToEvent.Invoke(id,chosenDialogue);
                break;
        }
        
    }
}
