using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour{

    /// <summary>
    /// Sert à charger une scène en fonction de son index
    /// </summary>
    /// <param name="index">index de la scène à charger</param>
    public void Load_Scene(int index) {
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Cette fonction sert à quitter le programme
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton Quitter
    /// </summary>
    public void Quit_Game() {
        Application.Quit();
    }
}
