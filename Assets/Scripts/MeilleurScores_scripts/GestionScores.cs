using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionScores : MonoBehaviour
{
    //###############################################################################
    //création de deux arrays. Un pour les noms de type string et un pour les pointage de type int.
    // Pour éviter les erreurs de sauvegarde, la liste des noms doit contenir des chaines de caractères vides.
    //public string[] maListe;
    public static string[] lstNoms  = new string[6] { "", "", "", "", "", "" };
    public static int[] lstScores  = new int[6] {0,0,0,0,0,0};
    //###############################################################################
    public string[] lstNomsCopie = new string[6]; // utile juste pour déboggage
    public int[] lstScoresCopie = new int[6];// utile juste pour déboggage

    public static GameObject laConsole;
    //###############################################################################
    void Start()
    {
        //DontDestroyOnLoad(gameObject); // pour déboggage
        //laConsole = GameObject.Find("Console"); // pour déboggage

        // on vérifie d'abord s'il existe un enregistrement de noms.
        if (PlayerPrefs.HasKey("SuvieShooterNoms"))
        {
            //si oui, on récupère les noms et les pointages qu'on envoie dans les listes.
            lstNoms = PlayerPrefsX.GetStringArray("SuvieShooterNoms");
            lstScores = PlayerPrefsX.GetIntArray("SuvieShooterPoints");
        }
        else
        {
            // Si il n'y a pas d'enregistrement, on appelle la fonction sauvegarde.
            Sauvegarde();
        }

        //InvokeRepeating("AfficheListes", 1f,5f); pour débogage
    }
    //###############################################################################

    //###############################################################################

    //Fonction appelée en fin de partie qui permet de vérifier si le pointage est assez bon
    //pour intégrer le tableau des meilleurs pointages.   
    public static bool Verification()
    {
        // on récupère le pointage du joueur (variable static pointage)
        int lePointage = DeplacementPersoScript_option.pointage;
        //si le pointage est > que le 5e pointage du tableau, on a un nouveau meilleur pointage a intégrer
        if (lePointage > lstScores[4])   
        {
            return true; // renvoie true si on doit intégrer un nouveau pointage
        }
        return false; // renvoie false si on n'a pas a intégrer un nouveau pointage
    }
    //###############################################################################
    //###############################################################################

    //Fonction appelée après que l'utilisateur ait entré son nom. On peut alors intégrer un nouveau meilleur pointage
    public static void EnregistrementPointageListes(string leNom)
    {
        // on récupère le pointage
        int lePointage = DeplacementPersoScript_option.pointage;

        // on définit le 6e élément des listes avec le nouveau nom et le nouveau pointage
        lstScores[5] = lePointage;
        lstNoms[5] = leNom;

        // On tri lles liste. La liste des noms sera triée selon le même ordre que la liste des poitages.
        System.Array.Sort(lstScores,lstNoms);
        // On renverse les deux listes. Pour avoir le pointage le plus haut en premier.
        System.Array.Reverse(lstScores);
        System.Array.Reverse(lstNoms);
        // Appel de la fonction pour sauvegarder les listes.
        Sauvegarde();
    }
    //###############################################################################

    //###############################################################################
    public static void Sauvegarde()
    {
        // Cette fonction sauvegarde les deux listes (noms et pointage)
        bool enregistreNoms = PlayerPrefsX.SetStringArray("SuvieShooterNoms", lstNoms);
        bool enregistrePoints = PlayerPrefsX.SetIntArray("SuvieShooterPoints", lstScores);

       /* if (enregistreNoms && enregistrePoints)
        {
            laConsole.GetComponent<Console>().AfficheConsole("Enregistrement réussi");
        }
        else
        {
            laConsole.GetComponent<Console>().AfficheConsole("Problème lors de l'enregistrement");
        }*/
       
    }
    //###############################################################################

    //fonction de déboggage.
    public void AfficheListes()
    {
        for (int pos = 0; pos < lstScores.Length; pos++)
        {
            
            lstScoresCopie[pos] = lstScores[pos];
            lstNomsCopie[pos] = lstNoms[pos];
        }
    }
    // Pour effacer la liste (code caché... on faite lefCommand+E
    private void Update()
    {
        if(Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.LeftCommand))
        {
            PlayerPrefs.DeleteKey("SuvieShooterNoms");
            PlayerPrefs.DeleteKey("SuvieShooterPoints");
            laConsole.GetComponent<Console>().AfficheConsole("Données effacées");
        }     
    }
}
