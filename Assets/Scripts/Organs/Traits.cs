using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    public enum OrganType
    {
        None,
        Kidney,
        Lung,
        Heart,
        Stomach,
        Liver,
        Brain,
    }
    public enum Funk
    {
        None,
        Suburban,
        Groovy,
        Tropical,
        Smelly,
        Sweaty,
        Greadsy,
        Moldy,
        Sick,
        Wet
    }
    public enum Material
    {
        None,
        Metal,
        Glass,
        Paper,
        Wood,
        Flesh,
        Stone,
        Ceramic,
        Burlap,
        Velvet,
        Denim,
        Cotton,
        Slime,
        Ice
    }

    public OrganType myOrgan = OrganType.None;
    public Funk myFunk = Funk.None;
    public Material myMaterial = Material.None;

    [Tooltip("Overall quality, this is not generated and must be assigned")]
    public int quality;
    //squish
    [Tooltip("How squishy the organ is")]
    public int squish;
    //Cuteness
    [Tooltip("How cute the organ is")]
    public int cuteness;
    //text desctiption of object
    [TextArea(5, 5), Tooltip("Description item will have when hovered over")]
    public string desctiption;

    //format text box will appear in as seen is ascii art
    /*
    --------------------------------------
    |Name     *******  Squish   *********|
    |Type     *******  Funk     *********|
    |Material *******  Cuteness *********|
    |desc:                               |
    | Acordint to all known laws of      |
    | aviation a bee.                    |
    --------------------------------------
    */


    //Calls when program starts
    void Awake()
    {
        //warns if no trait is selected for enums
        if(myOrgan == OrganType.None)
        {
            print(this.name + " has no Organ Type");
        }
        if (myFunk == Funk.None)
        {
            print(this.name + " has no funk Type");
        }
        if (myMaterial == Material.None)
        {
            print(this.name + " has no Material Type");
        }
    }
}
