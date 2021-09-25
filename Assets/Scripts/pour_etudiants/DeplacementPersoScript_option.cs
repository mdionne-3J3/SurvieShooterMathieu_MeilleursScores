using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeplacementPersoScript_option : MonoBehaviour
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
    public AudioClip sonBlesse;
    public GameObject[] coeurs;
    public GameObject joueur;
    public GameObject fusil;
    public int nbVies;
    public bool curseurLock;

    private int decompteInvincibilite;

    

    void Start()
    {
        // On définit la variable static derniereScene
        Global.derniereScene = SceneManager.GetActiveScene().name;
        if (curseurLock)Cursor.lockState = CursorLockMode.Confined;
       //Cursor.visible = false;
        DeplacementPersoScript_option.pointage = 0;
        DeplacementPersoScript_option.mort = false;
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
        if(!mort && infosCollisions.gameObject.tag == "ennemi" && decompteInvincibilite ==0)
        {
            nbVies--;
            if (nbVies == 0)
            {
                coeurs[2].SetActive(false);
                mort = true;
                GetComponent<Animator>().SetTrigger("mort");
                GetComponent<AudioSource>().PlayOneShot(sonMort);
                // si le personnae meurt, on appelle la fonction FinDuJeu après un délai de 2 secondes
                Invoke("FinDuJeu", 2f);
            }
            else
            {
                
                GetComponent<AudioSource>().PlayOneShot(sonBlesse);
                decompteInvincibilite = 0;
                InvokeRepeating("Invincible", 0.1f, 0.2f);
                if(nbVies==2)
                {
                    coeurs[0].SetActive(false);
                }
                else
                {
                    coeurs[1].SetActive(false);

                }
            }
            
        }
        
    }

    void Invincible()
    {
        decompteInvincibilite++;
        if(joueur.activeSelf)
        {
            joueur.SetActive(false);
        }
        else
        {
            joueur.SetActive(true);
        }
        if (fusil.activeSelf)
        {
            fusil.SetActive(false);
        }
        else
        {
            fusil.SetActive(true);
        }

        if (decompteInvincibilite==20)
        {
            decompteInvincibilite = 0;
            joueur.SetActive(true);
            fusil.SetActive(true);
            CancelInvoke("Invincible");
        }
    }

    void FinDuJeu()
    {
        // Ici, on applle la fonction satique qui vérifie le pointage du joueur. Si le joueur a réussi un
        // meilleur pointage, on charge la scène qui permet de s'enregistrer. Sinon, on va à la scène finale.

        if(GestionScores.Verification())
        {
            SceneManager.LoadScene("Enregistrement");
        }
        else
        {
            SceneManager.LoadScene("Fin");
        }
        
    }
}
