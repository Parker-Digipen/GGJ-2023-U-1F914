using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class OrganController : MonoBehaviour
{
    //all text boxes
    [SerializeField]
    private TMP_Text NameBox;
    [SerializeField]
    private TMP_Text DescBox;

    [SerializeField]
    private TMP_Text SquishBox;
    [SerializeField]
    private TMP_Text FunkBox;
    [SerializeField]
    private TMP_Text CuteBox;

    [SerializeField]
    private TMP_Text TypeBox;
    [SerializeField]
    private TMP_Text MatBox;

    //the body I'm checking
    [SerializeField]
    private Body myBody;

    //actual stored current organ
    private Traits _currentOrgan;

    [SerializeField]
    public Traits currentOrgan
    {
        set
        {
            _currentOrgan = value;
            
            if(_currentOrgan != null)
            {
                NameBox.text    = ""            + _currentOrgan.name;
                DescBox.text    = ""            + _currentOrgan.desctiption;

                SquishBox.text  = "Squish:\n"   + _currentOrgan.squish;
                FunkBox.text    = "Funk:\n"     + _currentOrgan.myFunk;
                CuteBox.text    = "Cuteness:\n" + _currentOrgan.cuteness;

                TypeBox.text    = "Type:\n"     + _currentOrgan.myOrgan;
                MatBox.text     = "Material:\n" + _currentOrgan.myMaterial;
            }
        }
        get
        {
            return _currentOrgan;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //if I don't have a body assigned, find one
        if(myBody == null)
        {
            myBody = FindObjectOfType<Body>();
        }
    }
}
