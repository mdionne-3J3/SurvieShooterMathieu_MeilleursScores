using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnnemiScriptOption_versionProf : MonoBehaviour
{
    public GameObject Personnage;
    public int vies;
    public AudioClip sonTouche;
    public AudioClip sonMort;

    public int valeurPoints;
    public Text textePointage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!DeplacementPersoScript_option.mort)
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
        vies--;
        if(vies==0)
        {
            GetComponent<AudioSource>().PlayOneShot(sonMort);
            GetComponent<Animator>().SetBool("Mort", true);
            GetComponent<NavMeshAgent>().enabled = false;
            gameObject.tag = "Untagged";
            GetComponent<Collider>().enabled = false;
            Invoke("Disparition", 2f);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(sonTouche);
        }
    }
    void Disparition()
    {
        transform.Find("DeathParticles").gameObject.SetActive(true);
        transform.Find("DeathParticles").parent = null;
        GestionPointage.pointageaAtteindre += valeurPoints;
        Destroy(gameObject);
        
    }

    
}
