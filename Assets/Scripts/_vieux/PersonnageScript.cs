using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
	Script de gestion de personnage, déplacemnt, rotation, animation, tire des balles, 
	et suivit de la caméra.
	Vahik Toroussian,
	05/04/2020
*/

public class PersonnageScript : MonoBehaviour {

	Animator bebeAnim;
	Rigidbody bebeRigid;
	AudioSource bebeAudio;
	public GameObject cameradistance;     //la caméra qui suit le perso

	public float vitesseDeplacement=4;    //vitesse personnage
    
	//Initialisations des composants de l'objet pesonnage
	void Start () {
		bebeAnim = GetComponent<Animator> ();
		bebeRigid = GetComponent<Rigidbody> ();
		bebeAudio =  GetComponent<AudioSource> ();
	}
    
	//Gestion des déplacements à l'aide des Axis, rotatio à l'aide de la souris,
	//animtion du personnage selon sa vitesse et suivit de la caméra
	void FixedUpdate ()
	{
		//déplacement gauche droite selon l'axe X et avant-arrière selon Z DU MONDE
		var h = Input.GetAxis ("Horizontal"); 
		var v = Input.GetAxis ("Vertical");
		bebeRigid.velocity = new Vector3(h, 0, v).normalized * vitesseDeplacement ;

		//Rotation du personnage avec la souris
	
		var tourne = Input.GetAxis ("Mouse X"); 
		transform.Rotate(0,tourne*4,0);
	
		//TournePersonnage();  //technique de rotation avec RayCast pour mieux viser : OPTIONNELLE
		
		//Gère la caméra à distane fixe du personnage
		cameradistance.transform.position = transform.position + new Vector3(0,3f,-6f);
		cameradistance.transform.LookAt(transform);  //caméra regarde l'enfant

		//Gère les animations selon la velocité de déplacement, 
		if (bebeRigid.velocity.magnitude != 0)
			bebeAnim.SetBool("marche",true);
		else
			bebeAnim.SetBool("marche",false);

	}
	void Update () 
	{
		
		
		
	}


    //PARITE OPTIONNELLE***************************************************************************
	//Crée un rayon de la caméra vers la souris dans le monde 3D
	//tourne le personnage vers la position de conatct de la souris sur le plancher
	void TournePersonnage()
	{
		//crée un rayon à partir de la caméra vers l’avant à la position de la souris
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// variable locale : contiendra les infos retournées par le Raycast sur l’objet touché 
		RaycastHit infoCollision;         
	
		// lance un rayon de 5000 unités à partir du rayon crée, vérifie seulement la collision avec le plancher
		// Le plancher doit avoir un layerMask (exemple:“Plancher”) assigné dans l’inspecteur 
		if( Physics.Raycast(camRay.origin, camRay.direction, out infoCollision , 5000, LayerMask.GetMask("Plancher")))
		{   
			//.... // Le personnage regarde vers le point de contact avec LookAt()

			//transform.localEulerAngles = .... //met les rotaion en x et z à 0 et garde la rotation en y 
		}

		Debug.DrawRay(camRay.origin, camRay.direction * 100, Color.yellow);   //outils pour visualiser le rayon dans 
		// l'onglet scene
	}


}
