using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.CardSystem;
using Assets.Scripts.CardSystem.CardCommand;
using Assets.Tests;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IPointerClickHandler
{
    public Button CardButton;
    public TextMeshProUGUI TextName;

    // Start is called before the first frame update
    void Awake()
    {
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //OnCardViewClicked?.Invoke();

    }

    public void OnStartPlay(Card card, CardPlayReport cardPlayReport)
    {
    }

    public void OnCommandRun(Card card, CardPlayReport cardPlayReport, CardCommandReport cardCommandReport)
    {
    }

    public void OnFinishPlay(Card card, CardPlayReport cardPlayReport)
    {
    }

    public void OnCardUpdate(Card card)
    {

        gameObject.name = card.Name;
        TextName.text = card.Name;

        switch (card.CardType)
        {
            case 1:
                GetComponent<Image>().color = Color.blue;
                break;

            case 2:
                GetComponent<Image>().color = Color.red;
                break;
        }

    }

    
}
