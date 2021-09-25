using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirScript_CreationBalle_versionProf : MonoBehaviour
{
    public GameObject balle; // Référence au gameObject de la balle
    public GameObject particuleBalle; // Référence au gameObject à activer lorsque le personnage tir
    public float vitesseBalle; // Vitesse de la balle



    /*#################################################
   -- variables privées
   #################################################*/
    private bool peutTirer; // Est-ce que le personnage peut tirer

    void Start()
    {
        peutTirer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && peutTirer)
        {
            Tir();
        }
    }

    void Tir()
    {
        peutTirer = false;
        Invoke("ActiveTir", 0.1f);

        particuleBalle.SetActive(true);
        GetComponent<AudioSource>().Play();

        GameObject nouvelleBalle = Instantiate(balle, balle.transform.position, balle.transform.rotation);


        nouvelleBalle.SetActive(true);
        nouvelleBalle.GetComponent<Rigidbody>().velocity = nouvelleBalle.transform.forward * vitesseBalle;

    }

    void ActiveTir()
    {
        peutTirer = true;
        particuleBalle.SetActive(false);
    }
}
