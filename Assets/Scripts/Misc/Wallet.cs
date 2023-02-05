using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private GameObject Instance;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private TMP_Text walletBox;

    [SerializeField]
    public List<GameObject> organs;

    [SerializeField]
    private int startingCash = 100;

    private int _balance;
    public int balance
    {
        set
        {
            _balance = value;
            if(walletBox)
            {
                walletBox.text = "$" + _balance;
            }
        }
        get
        {
            return _balance;
        }
    }
    private void Awake()
    {
        balance = startingCash;
        if (Instance == null)
        {
            Instance = this.gameObject;
            DontDestroyOnLoad(this);
        }
        else
        {
            /*
            for(int i = organs.Count; i >= 0; --i)
            {
                Instantiate(organs[i], spawnPoint.position, transform.rotation);
            }
            */
            Destroy(this);
        }
    }
    

}
