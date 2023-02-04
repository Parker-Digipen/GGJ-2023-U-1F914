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

    public int quality;
    //squish
    public int squish;
    //Cuteness
    public int cuteness;
    //text desctiption of object
    [TextArea(5, 5)]
    public string desctiption;

    //name              //squish
    //type              //funk
    //desc/flavor text  //cuteness

    //[SerializeField]


    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
