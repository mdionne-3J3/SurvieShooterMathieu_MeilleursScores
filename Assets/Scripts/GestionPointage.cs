using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionPointage : MonoBehaviour
{
    // Start is called before the first frame update
    private int pointageAffiche;
    public static int pointageaAtteindre;
    public Text textePointage;


    void Start()
    {
        InvokeRepeating("AffichagePointage", 0.1f, 0.1f);
        GestionPointage.pointageaAtteindre = 0;
    }

   

     void AffichagePointage()
    {
        if(DeplacementPersoScript_option.pointage < pointageaAtteindre)
        {
            DeplacementPersoScript_option.pointage++;
            textePointage.text = DeplacementPersoScript_option.pointage.ToString();
        }
        
    }
}
