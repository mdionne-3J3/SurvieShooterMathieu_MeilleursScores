using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfficheMeilleursScores : MonoBehaviour
{
    public Text txtNoms;
    public Text txtPointages;

    void Start()
    {
        //On vide les deux champs texte dans lequel on va afficher les noms et les meilleurs pointages.
        txtNoms.text = "";
        txtPointages.text = "";

        //Deux variables de type string dans lequel on va construire l'affichage des noms et l'affichages des scores
        string lesNoms = "";
        string lesScores = "";

        //Affichage des informations contenu dans les deux listes (noms et pointage) à l'aide d'une boucle for
        //On affiche tout le contenu du tableau,sauf le dernier éléments

        for (int posTableau = 0; posTableau < GestionScores.lstScores.Length-1; posTableau++)
        {
            //Si le pointage = 0, cela signifie que ce n'est pas une entrée valide. Se produit dans le cas ou il n'y a
            // pas de meilleurs pointages d'enregistrés ou lorsque les 5 positions de ne sont pas occupées.
            if (GestionScores.lstScores[posTableau] == 0)
            {
               break; // on sort du for prématurément
            }
            else
            {
                //On ajoute sucessivement tous les pointages du tableau et un retour à la ligne
                lesNoms += GestionScores.lstNoms[posTableau]+ "\n";
                //On ajoute sucessivement tous les noms du tableau et un retour à la ligne
                lesScores += GestionScores.lstScores[posTableau]+ "\n";
            }
        }
        // une fois la boucle terminée, on se sert des variables pour définir le contenu des zones de texte
        txtNoms.text = lesNoms;
        txtPointages.text = lesScores;
    }

    
}
