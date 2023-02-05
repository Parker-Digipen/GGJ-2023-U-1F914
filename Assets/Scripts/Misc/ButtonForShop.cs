using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonForShop : MonoBehaviour
{
    SimpleButton myBut;
    Shop myShop;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
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
