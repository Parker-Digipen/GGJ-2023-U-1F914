using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int orderNum = 1;

    [SerializeField]
    private static List<OrderManager.Order> completedOrders;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

}
