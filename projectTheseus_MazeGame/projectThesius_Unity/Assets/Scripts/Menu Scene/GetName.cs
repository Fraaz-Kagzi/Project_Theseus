using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetName : MonoBehaviour
{
    public static string theName;
    public GameObject inputField;
    public GameObject welcomeMessage;
    
    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        welcomeMessage.GetComponent<Text>().text = "Welcome "+ theName + " , to the maze";
    }
    


}
