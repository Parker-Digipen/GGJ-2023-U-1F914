using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public bool visible = false;

    private string _name;
    private string _description;
    public string boxName
    {
        set 
        { 
            _name = value;
            nameField.text = _name;
        }
        get
        { 
            return _name;
        }
    }

    public string boxDescription
    {
        set
        {
            _description = value;
            speakingField.text = _description;
        }
        get
        {
            return _description;
        }
    }

    [SerializeField]
    private TMP_Text nameField;

    [SerializeField]
    private TMP_Text speakingField;

    private SpriteRenderer mySR;

    // Start is called before the first frame update
    void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!visible)
        {
            mySR.enabled = false;
            nameField.enabled = false;
            speakingField.enabled = false;
        }
        else
        {
            mySR.enabled = true;
            nameField.enabled = true;
            speakingField.enabled = true;
        }
    }



}
