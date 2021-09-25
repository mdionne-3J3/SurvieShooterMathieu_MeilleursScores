using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
   
    
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AfficheConsole(string message)
    {
        GetComponent<Text>().text = message;
    }
}
