using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganSelection : MonoBehaviour
{
    [SerializeField]
    private OrganController myController;

    //the traits connected to this selector
    private Traits me;

    private void Awake()
    {
        //on start up find fields that are empty
        if(myController == null)
        {
            myController = FindObjectOfType<OrganController>();
        }
        me = GetComponent<Traits>();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            myController.currentOrgan = me;
        }
    }
}
