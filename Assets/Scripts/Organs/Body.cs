using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Body : MonoBehaviour
{
    [SerializeField, Tooltip("list of organs inside body by their game object")]
    public List<GameObject> organsInsideMeList;

    [SerializeField, Tooltip("the TMP object that will contain count of organs in body")]
    private TMP_Text organCountText;

    [SerializeField, Tooltip("Text to load before number of organs in body")]
    private string frontText;

    //deserialized constant used for tracking value of organs in the body
    private int _organCount = 0;

    //public interface of organ count, changes count text when updated
    [Tooltip("number of organs in body")]
    public int organCount
    {
        //define action for setting 
        set
        {
            _organCount = value;
            if(organCountText != null)
            {
                organCountText.text = frontText + _organCount;
            }
        }
        get
        { 
            return _organCount; 
        }
    }

    private void Start()
    {
        organCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if object entering body is organ
        if (collision.gameObject.tag == "Organ")
        {
            //print("hello choom");
            organsInsideMeList.Add(collision.gameObject);
            organCount++;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if object leaving body is tagged with organ
        if (collision.gameObject.tag == "Organ")
        {
            //print("goodbye choom");
            //finds the index of organ exiting body in list of organs
            int index = organsInsideMeList.FindIndex(g => g.gameObject == collision.gameObject);
            //if organ is found in list remove it
            if (index != -1)
            {
                //remove the organ that was found at that index
                organsInsideMeList.RemoveAt(index);
                organCount--;
            }
            else
            {
                //organ was not already on list of organs but was attempted to be removed
                print("something went wrong");
            }
        }
    }
}
