using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{

    public string nextScene = "";
    private SimpleButton myButton;
    [SerializeField]
    private bool plantGrow = false;
    [SerializeField]
    private CropGrowth[] crops;

    private void Start()
    {
        myButton = GetComponent<SimpleButton>();
        crops = FindObjectsOfType<CropGrowth>();
    }

    private void OnMouseDown()
    {
        if (plantGrow)
        {
            for (int i = crops.Length - 1; i >= 0; --i)
            {
                crops[i].grow();
                crops[i].gameObject.active = false; ;
            }
        }
        SceneManager.LoadScene(nextScene);
    }

}
