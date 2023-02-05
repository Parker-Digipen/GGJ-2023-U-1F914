using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField]
    private TMP_Text walletBox;
    private int _balance;
    public int balance
    {
        set
        {
            _balance = value;
            if(walletBox)
            {
                walletBox.text = "" + _balance;
            }
        }
        get
        {
            return _balance;
        }
    }
}
