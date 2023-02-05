using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static Traits;

public class Shop : MonoBehaviour
{
    public List<GameObject> preMadeItems = new List<GameObject>();

    public List<GameObject> currentlyAvailable = new List<GameObject>();

    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private GameObject template;

    [SerializeField]
    private List<Sprite> sprites = new List<Sprite>();

    public Vector2 availabilityRange = new Vector2(0, 1);

    public int size = 0;

    public static Traits generateRandomItem()
    {
        Traits output = new Traits();

        output.myOrgan = (OrganType)Random.Range(0, 5);
        output.myFunk = (Funk)Random.Range(0, 8);
        output.myMaterial = (Traits.Material)Random.Range(0, 12);

        output.squish = Random.Range(1, 10);
        output.cuteness = Random.Range(1, 10);

        output.quality = Random.Range(1, 5);

        output.desctiption = "By the gods, what is this?";
        //output.name = output.myMaterial.ToString() + " " + output.myOrgan.ToString();

        return output;
    }

    public void generateInventory()
    {
        if (availabilityRange.y >= spawnPoints.Count)
        {
            availabilityRange.y = spawnPoints.Count - 1;
        }
        size = (int)Random.Range(availabilityRange.x, availabilityRange.y);
        
        for (int i = 0; i <= size; i++)
        {
            GameObject temp = Instantiate(template, spawnPoints[i].position, transform.rotation);

            temp.GetComponent<Rigidbody2D>().isKinematic = true;
            temp.GetComponent<Traits>().generateRandom();

            int value = temp.GetComponent<Traits>().cuteness + temp.GetComponent<Traits>().squish;

            temp.GetComponent<Product>().price = ((value / 10) + 1) * Random.Range(100, 120);

            Traits tempOrg = temp.GetComponent<Traits>();

            temp.GetComponent<SpriteRenderer>().sprite = sprites[(int)tempOrg.myOrgan];

            temp.name = tempOrg.myMaterial.ToString() + " " + tempOrg.myOrgan.ToString();

            currentlyAvailable.Add(temp);
        }
    }

    private void Start()
    {
        generateInventory();
    }
}
