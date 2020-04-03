// Picks up values from components on click
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClickControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Captures clicked syllable in a text field
    void OnMouseDown()
    {
        Game1Manager.currentWord+=GetComponent<Text>().text;   
        // This statement is for testing purposes
        Debug.Log(Game1Manager.currentWord);     
    }
}
