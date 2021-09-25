using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenererEnnemisScript_versionProf : MonoBehaviour
{
    public GameObject lapin;
    public GameObject ours;
    public GameObject elephant;
    void Start()
    {
        InvokeRepeating("EnnemisFaibles", 0f,2f);
        InvokeRepeating("EnnemiFort", 2f,5f);
    }


    void EnnemisFaibles()
    {
        GameObject nouveauMonstre;
        if (Random.RandomRange(0,2)==1)
        {
            nouveauMonstre = Instantiate(lapin);
        }
        else
        {
            nouveauMonstre = Instantiate(ours);
        }

        nouveauMonstre.SetActive(true);
        
    }

    void EnnemiFort()
    {
        GameObject nouveauElephant = Instantiate(elephant);
        nouveauElephant.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
