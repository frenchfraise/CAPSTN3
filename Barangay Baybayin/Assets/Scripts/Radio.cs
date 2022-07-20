using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RadioFirstTimeEvent : UnityEvent { }
public class Radio : InteractibleObject
{
    [SerializeField]
    private string id;

    private bool isFirstTime;
    public static RadioFirstTimeEvent onRadioFirstTimeEvent = new RadioFirstTimeEvent();
    //  public CharacterSpokenToEvent onRadioSpokenToEvent = new CharacterSpokenToEvent();
    private void Awake()
    {
        onRadioFirstTimeEvent.AddListener(FirstTime);
        isFirstTime = true;
    }
    protected override void OnEnable()
    {
        base.OnEnable();

       // onRadioSpokenToEvent.AddListener(characterDialogueUI.OnCharacterSpokenTo);


    }
    protected override void OnDisable()
    {
        base.OnDisable();
       // onRadioSpokenToEvent.RemoveListener(characterDialogueUI.OnCharacterSpokenTo);

    }

    public void FirstTime()
    {
        isFirstTime = false;
        onRadioFirstTimeEvent.RemoveListener(FirstTime);
        OnInteract();
    }
    protected override void OnInteract()
    {
        if (!isFirstTime)
        {
            CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, WeatherManager.instance.currentWeatherDialogue);
        }
        else
        {
            TutorialUI.onRemindTutorialEvent.Invoke("weatherRadio");
        }
        
    }
}
