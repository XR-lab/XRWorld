using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XRWorld.Users
{
    public class MoneySystem : MonoBehaviour
    {
        public Text moneytxt;
        public int playerMoney = 500; // Start with $500

        private void Awake()
        {
            moneytxt.text = "Money:" + playerMoney;
        }

        public void AddPlayerMoney(int addAmount)
        {
            playerMoney += addAmount;
            moneytxt.text = "Money:" + playerMoney;
        
        }
        public void RemovePlayerMoney(int removeAmount)
        {
            playerMoney -= removeAmount;
            moneytxt.text = "Money:" + playerMoney;
        }
    }
}

