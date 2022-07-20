using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : InteractibleObject
{
    [SerializeField]
    private string id;


   //  public CharacterSpokenToEvent onRadioSpokenToEvent = new CharacterSpokenToEvent();
    private void Awake()
    {

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

   
    protected override void OnInteract()
    {
       
        CharacterDialogueUI.onCharacterSpokenToEvent.Invoke(id, WeatherManager.instance.currentWeatherDialogue);
      
        
    }
}
