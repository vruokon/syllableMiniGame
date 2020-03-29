//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour 
{
    public void LoadLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void _ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}