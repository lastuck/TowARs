using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour
{
    [SerializeField] 
    public int type;

    [SerializeField] 
    private Text nameText;
    
    [SerializeField] 
    private Text costText;
    
    [SerializeField] 
    private Text effectText;

    [SerializeField] 
    private Button button;

    [SerializeField] 
    private DeckListExposer deckListExposer;

    private InitDecklist initDecklist;
    
    private CardsDescription.Card card;

    private void OnEnable()
    {
        SetCard();
    }

    public void SetCard()
    {
        card = CardsDescription.GetCardStats(type);
        nameText.text = card.name;
        costText.text = card.cost + " RP";
        effectText.text = card.text;

        button.onClick.AddListener(AddCardToList);
        if (SceneParams.buildingDeck)
        {
            initDecklist = deckListExposer.initDecklist;
        }
    }

    void AddCardToList()
    {
        bool canAdd = false;
        var numberOfCardsByType = initDecklist.cards.GroupBy(i => i);
        if (initDecklist.cardTypesInDeck.Contains(type))
        {
            foreach (var cardType in numberOfCardsByType)
            {
                if (cardType.Key == type && cardType.Count() < 4)
                {
                    canAdd = true;
                }
            } 
        }
        else
        {
            canAdd = true;
        }
        
        if (canAdd)
        {
            initDecklist.cards.Add(type);
            initDecklist.ShowCardInList(type);
        }
    }
}
