using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class OrderManager : MonoBehaviour
{
    public struct Order
    {
        public string Customer;
        public int organsNeeded;
        public int payout;
        public int[] materialPref;  //scale 0-3 for preferance on material

        public int[] organPref;     //scale 0-3 for preferance on type of organ
        
        public int[] funkPref;      //scale 0-3 for preference on funk

        public Vector2 cutenessRange;
        public Vector2 squishynessRange;
        public int targetQuality;
        public string OrderRequest;
        public string OrderWin;
        public string OrderLose;

        //not defined by file
        public string filePath;
        public int orderNumber;

        //populated on completion
        public bool completed;
        public float grade;
        public List<GameObject> solution;

    }

    Order currentOrder;

    [SerializeField]
    private string orderFolderPath;

    [SerializeField]
    private List<string> orderFileNames = new List<string>();

    [SerializeField]
    private string orderFileType = ".txt";

    [SerializeField]
    private Wallet moneyBox;

    [SerializeField]
    private Body myBody;

    [SerializeField]
    private SimpleButton phone;

    [SerializeField]
    private DialogueBox box;

    [SerializeField]
    private float lengthBeforeClosingDialogueBox;


    private Vector2 readCSV(string input)
    {
        Vector2 output = new Vector2();
        string[] separated = input.Split(",");
        
        output.x = int.Parse(separated[0]);
        output.y = int.Parse(separated[1]);


        return output;
    }
    
    private int readHyphen(string input)
    {
        int output = 0;
        string[] separated = input.Split("-");
        
        if(!input.Contains("-"))
        {
            return 420;
        }

        output = int.Parse(separated[1]);

        return output;
    }

    //reads very specifically formatted file
    private Order takeOrderFromFile(string path)
    {
        Order output = new Order();
        //file being read
        StreamReader f = new StreamReader(path);
        
        //conotent of the line
        string line;
        //current state/line number being read ish
        int lineBeingRead = 0;

        const int numberOfOrgans = 6;
        const int numberOfFunks = 9;
        const int numberOfMaterial = 13;

        output.organPref = new int[numberOfOrgans];
        output.funkPref = new int[numberOfFunks];
        output.materialPref = new int[numberOfMaterial];

        while ((line = f.ReadLine()) != null)
        {
            //state machine for reading
            switch (lineBeingRead)
            {
                //set max payout
                case 0:
                    output.payout = int.Parse(line);
                    break;
                //next line is name
                case 1:
                    output.Customer = line; break;
                case 2:
                //next line is number of organs for order
                    output.organsNeeded = int.Parse(line); break;
                //populates organ preferances
                case 3:
                    for(int i = 0; i < numberOfOrgans; i++)
                    {
                        output.organPref[i] = readHyphen(line);
                        line = f.ReadLine();
                    }
                    break;
                //populates funk preferances
                case 4:
                    for (int i = 0; i < numberOfFunks; i++)
                    {
                        output.funkPref[i] = readHyphen(line);
                        line = f.ReadLine();
                    }
                    break;
                //populates material prefs
                case 5:
                    for (int i = 0; i < numberOfMaterial; i++)
                    {
                        output.materialPref[i] = readHyphen(line);
                        line = f.ReadLine();
                    }
                    break;
                case 6:
                    output.cutenessRange = readCSV(line);
                    break;
                case 7:
                    output.squishynessRange = readCSV(line);
                    break;
                case 8:
                    output.targetQuality = int.Parse(line);
                    break;
                case 9:
                    output.OrderRequest = line.Replace("\n", "").Replace("\t", "");
                    break;
                case 10:
                    output.OrderWin = line.Replace("\n", "").Replace("\t", "");
                    break;
                case 11:
                    output.OrderLose = line.Replace("\n", "").Replace("\t", "");
                    break;
            }
            lineBeingRead++;
        }
        return output;
    }

    public void nextOrder()
    {
        int tempRandIndexOfFile;
        
        tempRandIndexOfFile = Random.Range(0, orderFileNames.Count);

        //populate from file
        currentOrder = takeOrderFromFile(orderFolderPath + orderFileNames[tempRandIndexOfFile] + orderFileType);

        //populate non file fields
        currentOrder.orderNumber = GameManager.orderNum;
        currentOrder.filePath = orderFolderPath + orderFileNames[tempRandIndexOfFile] + orderFileType;

        //tell gm that I have taken an order
        GameManager.orderNum++;

        //populates text box
        box.boxName = currentOrder.Customer;
        box.boxDescription = currentOrder.OrderRequest;

    }

    private float evaluateOrder()
    {
        float netQuality = 0;
        float[] organQualities = new float[myBody.organCount];

        //print(myBody.organsInsideMeList.Count);

        //averages the quality of the organ relative to the order for each loop and element in list
        for(int i = myBody.organCount - 1; i >= 0; --i)
        {
            Traits organBeingRead = myBody.organsInsideMeList[i].GetComponent<Traits>();
            
            //print("Material prefs len: " + currentOrder.materialPref[0]);
            //evaluates value of material based on has code (where they are in the enum) of body parts material
            int materialValue = currentOrder.materialPref[organBeingRead.myMaterial.GetHashCode()];
            //print("Material prefs len: " + currentOrder.materialPref.Length + " code: " + organBeingRead.myMaterial.GetHashCode() + " val: " + materialValue);

            //evaluates value of material based on has code (where they are in the enum) of body parts organ
            int organValue = currentOrder.organPref[organBeingRead.myOrgan.GetHashCode()];
            //print("organ prefs len: " + currentOrder.organPref.Length + " code: " + organBeingRead.myOrgan.GetHashCode() + " val: " + organValue);

            //evaluates value of material based on has code (where they are in the enum) of body parts fnuk
            int funkValue = currentOrder.funkPref[organBeingRead.myFunk.GetHashCode()];
            //print("funk prefs len: " + currentOrder.funkPref.Length + " code: " + organBeingRead.myFunk.GetHashCode() + " val: " + funkValue);

            //averages value of organ values
            netQuality += (materialValue + organValue + funkValue) / 9;

            //if cuteness of organ being read is in the range of order
            if ((organBeingRead.cuteness >= currentOrder.cutenessRange.x) 
                && (organBeingRead.cuteness <= currentOrder.cutenessRange.y))
            {
                //gets the difference between 
                float cutenessPool = currentOrder.cutenessRange.y - currentOrder.cutenessRange.x;
                //gets amount of cuteness being taken up in the pool
                //add vaule to net quantity
                netQuality += (organBeingRead.cuteness - currentOrder.cutenessRange.x) / cutenessPool;
            }
            //if squish of organ being read is in the range of order
            if ((organBeingRead.squish >= currentOrder.squishynessRange.x)
                && (organBeingRead.squish <= currentOrder.squishynessRange.y))
            {
                //gets the difference between 
                float squishPool = currentOrder.squishynessRange.y - currentOrder.squishynessRange.x;
                //gets amount of squish being taken up in the pool
                //add vaule to net quantity
                netQuality += (organBeingRead.squish - currentOrder.squishynessRange.x) / squishPool;
            }

            //find the difference between organ quality and target qulity
            if (organBeingRead.quality > currentOrder.targetQuality)
            {
                // average value to get percentage of differance
                netQuality += (organBeingRead.quality - currentOrder.targetQuality) / 5.0f;
            }
            else if (organBeingRead.quality < currentOrder.targetQuality)
            {
                netQuality += (currentOrder.targetQuality - organBeingRead.quality) / 5.0f;
            }
            else
            {
                netQuality += 1;
            }

            //dividing by number of traits being compared
            organQualities[i] = netQuality / 4;
        }

        netQuality = 0;

        for(int i = myBody.organCount - 1; i >= 0; --i)
        {
            netQuality += organQualities[i];
        }

        if (myBody.organCount > currentOrder.organsNeeded)
            if (netQuality / myBody.organCount >= 1)
                return 1;
            else
                return netQuality / myBody.organCount;
        else
            return netQuality / myBody.organCount;
    }


    public void orderComplete()
    {
        currentOrder.completed = true;
        currentOrder.solution = myBody.organsInsideMeList;
        currentOrder.grade = evaluateOrder();

        //GameManager.completedOrders.Add(currentOrder);

        //add money to wallet
        //print("payout: " + currentOrder.payout + "grade: " + currentOrder.grade + "balance: " + moneyBox.balance);
        moneyBox.balance += (int)(currentOrder.payout * currentOrder.grade);

        for(int i = myBody.organCount - 1; i >= 0; --i)
        {
            Destroy(myBody.organsInsideMeList[i].gameObject);
        }

        box.boxDescription = currentOrder.OrderWin;
        timer = lengthBeforeClosingDialogueBox;

        Invoke("nextOrder", lengthBeforeClosingDialogueBox);
    }

    public void orderFail()
    {
        currentOrder.completed = false;
        currentOrder.grade = -1;


        for (int i = myBody.organCount - 1; i >= 0; --i)
        {
            Destroy(myBody.organsInsideMeList[i].gameObject);
        }

        box.boxDescription = currentOrder.OrderLose;
        timer = lengthBeforeClosingDialogueBox;

        Invoke("nextOrder", lengthBeforeClosingDialogueBox);
    }


    private void Awake()
    {
        if (!myBody)
        {
            myBody = FindObjectOfType<Body>();
        }
        if (!moneyBox)
        {
            moneyBox = FindObjectOfType<Wallet>();
        }

        nextOrder();
    }

    private float timer = 0;


    // Update is called once per frame
    void Update()
    {
        //handles text box pop up
        if (phone.hover)
        {
            box.visible = true;
            timer = lengthBeforeClosingDialogueBox;
        }
        else
        {
            box.visible = false;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            box.visible = true;
        }

        //if button is pressed
        if (phone.pressed && UnityEngine.Input.GetMouseButtonDown(0))
        {
            if(myBody.organCount > 0)
            {
                orderComplete();
            }
            else
            {
                orderFail();
            }
        }
    }
}
