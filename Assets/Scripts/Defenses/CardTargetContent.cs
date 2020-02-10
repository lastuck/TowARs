using System;
using UnityEngine;

namespace Defenses
{
    public class CardTargetContent : MonoBehaviour
    {
        public GameObject turretObject;

        [SerializeField] 
        public CardBehavior cardBehavior;

        [SerializeField]
        public GameObject turretObjectParent;
        
    }
}
