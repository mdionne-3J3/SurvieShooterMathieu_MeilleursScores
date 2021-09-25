using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitreScintille : MonoBehaviour
{
    public float intervalleScintillement;
    public bool CouleursAleatoires;

    // Start is called before the first frame update
    void Start()
    {
        // appelle d'une coroutine qui fera scintiller le texte
        StartCoroutine("Scintillement");
       
    }

    IEnumerator Scintillement()
    {
        // une boucle infinie... mais avec deux pauses à l'intérieur.
        while(true)
        {
            GetComponent<Text>().enabled = false;
            yield return new WaitForSeconds(intervalleScintillement);
            GetComponent<Text>().enabled = true;
            yield return new WaitForSeconds(intervalleScintillement);
            if (CouleursAleatoires) GetComponent<Text>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        }
        
    }
}
