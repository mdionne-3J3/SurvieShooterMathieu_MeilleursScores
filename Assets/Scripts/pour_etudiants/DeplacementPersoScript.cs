using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeplacementPersoScript : MonoBehaviour
{
    //variable statique
    public static int pointage;
    public static bool mort;

    // varibables publiques

    public GameObject cameraPerso;
    public Vector3 distanceCamera;
    public float vitesseDeplacementPerso;
    public float vitesseRotationPerso;
    public AudioClip sonMort;
    public bool curseurLock;


    

    void Start()
    {
        if(curseurLock)Cursor.lockState = CursorLockMode.Locked;
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!mort)
        {
            //déplacement du perso
            float axeH = Input.GetAxisRaw("Horizontal");
            float axeV = Input.GetAxisRaw("Vertical");
            GetComponent<Rigidbody>().velocity = new Vector3(axeH, 0f, axeV).normalized * vitesseDeplacementPerso;

            //rotation du perso
            //float tourne = Input.GetAxis("Mouse X") * vitesseRotationPerso;
            //transform.Rotate(0f, tourne,0f);
            TournePersonnage();

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

    private void OnCollisionEnter(Collision infosCollisions)
    {
        if(!mort && infosCollisions.gameObject.tag == "ennemi")
        {
            mort = true;
            GetComponent<Animator>().SetTrigger("mort");
            GetComponent<AudioSource>().PlayOneShot(sonMort);
        }
        
    }
}
