using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropGrowth : MonoBehaviour
{
    public enum growthState
    {
        empty,
        planted,
        growing,
        harvest
    }

    private GameObject Instance;

    public growthState myGrowth;

    [SerializeField]
    private GameObject organTemplate;
    [SerializeField]
    private Transform spawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Traits temp;
        if(myGrowth == growthState.empty && collision.TryGetComponent<Traits>(out temp))
        {
            myGrowth = growthState.planted;
            Destroy(collision.gameObject);
        }
    }

    public void grow()
    {
        if(myGrowth != growthState.empty && myGrowth != growthState.harvest)
        {
            myGrowth++;
        }
    }
    private void OnMouseDown()
    {
        if (myGrowth == growthState.harvest)
        {
            for (int i = Random.Range(1, 5); i >= 0; --i)
            {
                GameObject temp = Instantiate(organTemplate);
                temp.GetComponent<Traits>().generateRandom();
            }
            myGrowth = growthState.empty;
        }

    }
    private void Start()
    {
        print("111");
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this.gameObject;
            DontDestroyOnLoad(this);
        }
        else
        {
            Instance.active = true;
            Destroy(this);
        }
    }


}
