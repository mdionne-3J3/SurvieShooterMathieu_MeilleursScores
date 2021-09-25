using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NavigationMeilleurPointage : MonoBehaviour
{

    public void Start()
    {
        //On définit la variable static derniereScene
        Global.derniereScene = SceneManager.GetActiveScene().name;
    }

    public void ChargementMeilleursPointage()
    {
        //Fonction appelée lorsqu'on clique sur l'image du trophée. Pour consulter le tableau des meilleurs scores.
        SceneManager.LoadScene("Fin");
    }
}
