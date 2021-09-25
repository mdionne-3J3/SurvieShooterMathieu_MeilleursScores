using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AffichePointage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
        if (Global.derniereScene == "Intro")
        {
            GetComponent<Text>().text = "";
        }
        else
        {
            GetComponent<Text>().text = "Tu as " + DeplacementPersoScript_option.pointage + " points.";
        }

        Global.derniereScene = SceneManager.GetActiveScene().name;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
