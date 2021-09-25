using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnnemiScript_versionProf : MonoBehaviour
{
    public GameObject Personnage;
    public int valeurPoints;
    public Text textePointage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!DeplacementPersoScript.mort)
        {
            if (GetComponent<NavMeshAgent>().enabled)
            {
                GetComponent<NavMeshAgent>().SetDestination(Personnage.transform.position);
            }
        }
        else
        {
            GetComponent<Animator>().SetTrigger("PersonnageMort");
            GetComponent<NavMeshAgent>().enabled = false;
        }
        
        
    }

    public void Touche()
    {
        
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().SetBool("Mort",true);
        GetComponent<NavMeshAgent>().enabled = false;
        gameObject.tag = "Untagged";
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2f);
        DeplacementPersoScript.pointage += valeurPoints;
        textePointage.text = DeplacementPersoScript.pointage.ToString();
    }
}
