using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour{

    /// <summary>
    /// Cette fonction sert à charger la scène principale
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton Jouer
    /// </summary>
    public void Go_To_Next_Scence() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Cette fonction sert à quitter le programme
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton Quitter
    /// </summary>
    public void Quit_Game() {
        Application.Quit();
    }
}
