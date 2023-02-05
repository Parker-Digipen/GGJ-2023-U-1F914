using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleButton : MonoBehaviour
{
    //type of button
    [SerializeField, Tooltip("is this a toggle button")]
    private bool toggle = false;

    //different colors button could be
    [SerializeField, Tooltip("Button active Color to indicate that it is toggled")]
    private Color activeToggleColor;

    [SerializeField, Tooltip("Color of button when not pressed or hovered over")]
    private Color defaultColor;

    [SerializeField, Tooltip("Color when mouse is hovering over button")]
    private Color hoverColor;

    [SerializeField, Tooltip("Color of the button while it is being pressed")]
    private Color clickedColor;

    //actual variables referenced by things
    [Tooltip("is mouse hovering over button")]
    public bool hover = false;
    
    [Tooltip("is the button currently pressed eg: active")]
    public bool pressed = false;

    public int priceToPress = 0;
    public bool destroyOnPress = false;

    SpriteRenderer mySR;

    private void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        mySR.color = hoverColor;
        hover = true;
    }
    
    private void OnMouseExit()
    {
        if(toggle == false || pressed == false)
        {
            mySR.color = defaultColor;
        }
        else if(toggle == true && pressed == true)
        {
            mySR.color = activeToggleColor;
        }
        hover = false;
    }

    //if the mouse is over the button
    private void OnMouseOver()
    {
        //if type is toggle
        if(toggle == true)
        {
            //if mouse is being clicked
            if (Input.GetMouseButtonDown(0))
            {
                //toggle functionality
                if(pressed == true)
                {
                    pressed = false;
                }
                else
                {
                    pressed = true;
                }
            }
            //mouse being held change color to pressed color
            if (Input.GetMouseButton(0))
            {
                mySR.color = clickedColor;
            }
            else
            {
                mySR.color = hoverColor;
            }
        }
        //if type is press button
        else
        {
            //if mouse is held down
            if (Input.GetMouseButton(0))
            {
                if(Input.GetMouseButtonDown(0))
                {
                    FindObjectOfType<Wallet>().balance -= priceToPress;
                }
                //being pressed and setting color
                pressed = true;
                mySR.color = clickedColor;
                if(destroyOnPress)
                {
                    Destroy(this);
                }
            }
            else
            {
                //pressed is false and set to hover color
                pressed = false;
                mySR.color = hoverColor;
            }
        }

    }

}
