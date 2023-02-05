using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class Product : MonoBehaviour
{
    Rigidbody2D myRB;
    Wallet moeny;
    public TMP_Text priceListing;

    [SerializeField]
    private int _price = 100;
    public int price
    {
        set
        {
            _price = value;
            if (priceListing)
                priceListing.text = "$" + _price;
        }
        get { return _price; }
    }


    // Start is called before the first frame update
    void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        moeny = FindObjectOfType<Wallet>();
        if(priceListing)
            priceListing.text = "$" + price;
    }

    private void OnMouseDown()
    {
        if ((moeny.balance - price) >= 0)
        {
            moeny.balance -= price;
            
            myRB.isKinematic = false;
            
            gameObject.layer = 6;

            moeny.organs.Add(gameObject);

            Destroy(priceListing);
            Destroy(this);
        }
    }

}
