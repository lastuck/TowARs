using System;
using UnityEngine;
using UnityEngine.UI;

namespace Defenses
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        public int playerMaxHP;
        
        public int playerHP;
        [SerializeField]
        public int playerMaxRP;
        
        public int playerRP;
        

        [SerializeField] 
        private Slider HPSlider;

        [SerializeField] 
        private Text RPText;

        private void Start()
        {
            playerHP = playerMaxHP;
            playerRP = playerMaxRP;
        }

        private void Update()
        {
            HPSlider.value = (float) playerHP / playerMaxHP;
            RPText.text = playerRP.ToString();
        }
    }
}
