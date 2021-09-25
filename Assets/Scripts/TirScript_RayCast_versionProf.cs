using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirScript_RayCast_versionProf : MonoBehaviour
{
  
    public GameObject particuleBalle; // Référence au gameObject à activer lorsque le personnage tir
    public GameObject boutDuFusil; // Référence au gameObject qui représente le bout du fusil du personnage. Utile pour le tir.
    public GameObject impactTir; // Référence au gameObject (ou Prefab) à instancier lorsque le tir frappe un objet.

    /*#################################################
   -- variables privées
   #################################################*/
    private bool peutTirer; // Est-ce que le personnage peut tirer

    void Start()
    {
        peutTirer = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && peutTirer)
        {
            Tir();
        }
        if (boutDuFusil.GetComponent<LineRenderer>().enabled) boutDuFusil.GetComponent<LineRenderer>().SetPosition(0, boutDuFusil.transform.position);

    }

    void Tir()
    {
        peutTirer = false;
        Invoke("ActiveTir", 0.1f);

        particuleBalle.SetActive(true);
        GetComponent<AudioSource>().Play();


        RaycastHit infosCollision;

        if (Physics.Raycast(boutDuFusil.transform.position, boutDuFusil.transform.forward, out infosCollision, 50f))
        {
            GameObject objetImpact = Instantiate(impactTir, infosCollision.point, Quaternion.identity);
            objetImpact.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            //objetImpact.transform.eulerAngles = new Vector3(-90f, objetImpact.transform.eulerAngles.y, objetImpact.transform.eulerAngles.x);

            //activer la ligne
            boutDuFusil.GetComponent<LineRenderer>().enabled = true;
            boutDuFusil.GetComponent<LineRenderer>().SetPosition(0, boutDuFusil.transform.position);
            boutDuFusil.GetComponent<LineRenderer>().SetPosition(1, infosCollision.point);
            Destroy(objetImpact, 3f);

            if (infosCollision.collider.gameObject.tag == "ennemi")
            {
                if (infosCollision.collider.gameObject.GetComponent<EnnemiScript_versionProf>().enabled)
                {
                    infosCollision.collider.gameObject.GetComponent<EnnemiScript_versionProf>().Touche();
                }
                else
                {
                    infosCollision.collider.gameObject.GetComponent<EnnemiScriptOption_versionProf>().Touche();
                }
            }
        }

    }

    void ActiveTir()
    {
        peutTirer = true;
        particuleBalle.SetActive(false);
        boutDuFusil.GetComponent<LineRenderer>().enabled = false;
    }
}
