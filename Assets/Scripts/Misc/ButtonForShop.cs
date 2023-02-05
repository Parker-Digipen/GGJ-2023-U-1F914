using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonForShop : MonoBehaviour
{
    SimpleButton myBut;
    Shop myShop;
    private void OnMouseDown()
    {
        if(myBut.pressed)
        {
            myShop.restock();
        }
    }
    private void Start()
    {
        myBut = GetComponent<SimpleButton>();
        myShop = FindObjectOfType<Shop>();
    }
}
