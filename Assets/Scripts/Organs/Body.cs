using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> organsInsideMeList;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if object entering body is organ
        if (collision.gameObject.tag == "Organ")
        {
            //print("hello choom");
            organsInsideMeList.Add(collision.gameObject);
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
            }
            else
            {
                //organ was not already on list of organs but was attempted to be removed
                print("something went wrong");
            }
        }
    }
}
