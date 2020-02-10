using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GarbageRoyale.Scripts;
using UnityEngine;
using Random = System.Random;

namespace Defenses
{
    public class DeckListReader : MonoBehaviour
    {
        public List<int> cardsInDeck;
        public List<int> currentDeckState;
        
        [SerializeField]
        public CardTargetContent[] cardTargets;

        [SerializeField] 
        private ObjectPooler objectPoolerTurrets;
        
        void Start()
        {
            cardsInDeck = new List<int>();
            currentDeckState = new List<int>();
            if (SceneParams.newDeck == false)
            {
                try
                {
                    string path = Application.persistentDataPath + "/" + SceneParams.deckName;
                    string line;
                    using (StreamReader sr = new StreamReader(path))
                    {

                        line = sr.ReadLine();
                        if (line != null)
                        {
                            cardsInDeck = line.Split(';').Select(Int32.Parse).ToList();
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("The file could not be read:");
                    Debug.Log(e.Message);
                }
            }


            currentDeckState = ShuffleList(cardsInDeck);
            FillHand();
        }
        
        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }

            return randomList;
        }

        public void FillHand()
        {
            foreach (var target in cardTargets)
            {
                if (target.turretObjectParent.transform.childCount == 0 && currentDeckState.Count>0)
                {
                    target.turretObject = objectPoolerTurrets.GetPooledObject(currentDeckState[0]);
                    target.turretObject.transform.SetParent(target.turretObjectParent.transform);
                    target.turretObject.transform.localPosition = new Vector3(0,1,0);
                    target.turretObject.SetActive(true);
                    target.cardBehavior.type = currentDeckState[0];
                    target.cardBehavior.SetCard();
                    currentDeckState.RemoveAt(0);
                }
                else if(currentDeckState.Count == 0)
                {
                    target.cardBehavior.gameObject.SetActive(false);
                }
            }
        }
    }
}
