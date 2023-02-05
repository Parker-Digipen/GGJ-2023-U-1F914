using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits : MonoBehaviour
{
    public enum OrganType
    {
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
        Smelly,
        Sweaty,
        Greasy,
        Moldy,
        Sick,
        Wet
    }
    public enum Material
    {
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

    public OrganType myOrgan = OrganType.Kidney;
    public Funk myFunk = Funk.None;
    public Material myMaterial = Material.Metal;

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

    public void generateRandom()
    {
        myOrgan = (OrganType)Random.Range(0, 5);
        myFunk = (Funk)Random.Range(0, 8);
        myMaterial = (Traits.Material)Random.Range(0, 12);

        squish = Random.Range(1, 10);
        cuteness = Random.Range(1, 10);

        quality = Random.Range(1, 5);

        desctiption = "By the gods, what is this?";
        //output.name = output.myMaterial.ToString() + " " + output.myOrgan.ToString();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
