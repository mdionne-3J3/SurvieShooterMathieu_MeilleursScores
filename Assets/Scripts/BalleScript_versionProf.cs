using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleScript_versionProf : MonoBehaviour
{
    public GameObject impactTir; // Référence au Prefab à instancier lorsque le tir frappe un objet. (Prefab ParticulesHit)
    public GameObject personnage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision infoCollisions)
    {
        GameObject particuleCopie = Instantiate(impactTir);
        particuleCopie.transform.position = infoCollisions.GetContact(0).point;
        particuleCopie.SetActive(true);
        particuleCopie.transform.LookAt(personnage.transform);
        particuleCopie.transform.Translate(0f, 0.5f, 0.5f);
        Destroy(particuleCopie, 1f);
        Destroy(gameObject);

        // si c'est un ennemi
        if(infoCollisions.gameObject.tag == "ennemi")
        {
            if(infoCollisions.gameObject.GetComponent<EnnemiScript_versionProf>().enabled)
            {
                infoCollisions.gameObject.GetComponent<EnnemiScript_versionProf>().Touche();
            }
            else
            {
                infoCollisions.gameObject.GetComponent<EnnemiScriptOption_versionProf>().Touche();
            }
        }
    }
}
