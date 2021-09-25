using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnageBalleScript : MonoBehaviour
{
    // varibables publiques

    public GameObject cameraPerso;
    public Vector3 distanceCamera;
    public float vitesseDeplacementPerso;
    public float vitesseRotationPerso;
    public GameObject balle;
    public GameObject particuleBalle;
    public GameObject boutDuFusil;
    public GameObject impactTir;
    public float vitesseBalle;
    public bool curseurLock;


    private bool peutTirer;

    void Start()
    {
        
        if (curseurLock) Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && peutTirer)
        {
            Tir();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //déplacement du perso
        float axeH = Input.GetAxisRaw("Horizontal");
        float axeV = Input.GetAxisRaw("Vertical");
        GetComponent<Rigidbody>().velocity = new Vector3(axeH, 0f, axeV).normalized * vitesseDeplacementPerso;

        //rotation du perso
        float tourne = Input.GetAxis("Mouse X") * vitesseRotationPerso;
        transform.Rotate(0f, tourne, 0f);
        //TournePersonnage();

        //positionnement de la caméra
        cameraPerso.transform.position = transform.position + distanceCamera;
        cameraPerso.transform.LookAt(transform.position);

        //gestion des animations
        if (GetComponent<Rigidbody>().velocity.magnitude > 0f)
        {
            GetComponent<Animator>().SetBool("marche", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("marche", false);
        }

    }

    void Tir()
    {
        peutTirer = false;
        Invoke("ActiveTir", 0.1f);

        particuleBalle.SetActive(true);
        GetComponent<AudioSource>().Play();

        GameObject nouvelleBalle = Instantiate(balle, balle.transform.position,balle.transform.rotation);
        

        nouvelleBalle.SetActive(true);
        nouvelleBalle.GetComponent<Rigidbody>().velocity = nouvelleBalle.transform.forward * vitesseBalle;

        /*
            GameObject objetImpact = Instantiate(impactTir, infosCollision.point, Quaternion.identity);
            objetImpact.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            //objetImpact.transform.eulerAngles = new Vector3(-90f, objetImpact.transform.eulerAngles.y, objetImpact.transform.eulerAngles.x);

            //activer la ligne
            boutDuFusil.GetComponent<LineRenderer>().enabled = true;
            boutDuFusil.GetComponent<LineRenderer>().SetPosition(0, boutDuFusil.transform.position);
            boutDuFusil.GetComponent<LineRenderer>().SetPosition(1, infosCollision.point);
            Destroy(objetImpact, 3f);
        
         */


    }

    void ActiveTir()
    {
        peutTirer = true;
        particuleBalle.SetActive(false);
        boutDuFusil.GetComponent<LineRenderer>().enabled = false;
    }

    void TournePersonnage()
    {
        //crée un rayon à partir de la caméra vers l’avant à la position de la souris
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // variable locale : contiendra les infos retournées par le Raycast sur l’objet touché 
        RaycastHit infoCollision;

        // lance un rayon de 5000 unités à partir du rayon crée, vérifie seulement la collision avec le plancher
        // Le plancher doit avoir un layerMask (exemple:“Plancher”) assigné dans l’inspecteur 
        if (Physics.Raycast(camRay.origin, camRay.direction, out infoCollision, 5000, LayerMask.GetMask("plancher")))
        {
            //.... // Le personnage regarde vers le point de contact avec LookAt()
            transform.LookAt(infoCollision.point);
            //transform.localEulerAngles = .... //met les rotaion en x et z à 0 et garde la rotation en y
            Vector3 rotationActuelle = transform.localEulerAngles;
            rotationActuelle.x = 0f;
            rotationActuelle.z = 0f;
            transform.localEulerAngles = rotationActuelle;
        }

        Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.yellow);   //outils pour visualiser le rayon dans 
                                                                              // l'onglet scene
    }
}
