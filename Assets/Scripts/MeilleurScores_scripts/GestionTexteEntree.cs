using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GestionTexteEntree : MonoBehaviour
{
    public Text txtInput; //le champs de saisie du nom du joueur
    public Text message; // la zone de texte pour écrire des messages au joueur


    private void Start()
    {
        //On définit la variable static derniereScene
        Global.derniereScene = SceneManager.GetActiveScene().name;
        Cursor.visible = true;
    }

    //Fonction appelée lors le joueur appuie sur retour ou lorqu'il clique à l'extérieur de la zone de saisie
    public void ValidationNom()
    {
        // On récupère le nom entré
        string leNom = txtInput.text;

        // si le nombre de caractères entrée est de 0, on avertie le joueur. Sinon on procède à l'enregistrement
        if (leNom.Length == 0f)
        {
            message.text = "Il faut entrer un nom coco!";
            message.color = new Color(255f, 0f, 0f);

        }
        else
        {
            message.text = "Enregistrement en cours"; // message qui indique que l'enregistrement est en cours.
            message.color = new Color(255f, 0f, 0f); // couleur du message en rouge
            GestionScores.EnregistrementPointageListes(leNom);//appel de la fonction pour enregistrer le nouveau meilleur score
            Invoke("ScenePointage",2f); // on appelle la fonction "ScenePointage" après un délai de 2 secondes
        }
    }

    //Chargement de la scène finale
    void ScenePointage()
    {
        SceneManager.LoadScene("Fin");
    }
    
}
