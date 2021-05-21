using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UImanager : MonoBehaviour{
    private bool _isPaused = false; 

    [SerializeField] private InputActionAsset _actionAsset = default;
    [SerializeField] private GameObject _pausePanel = default;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private GameObject _GamePanel = default;
    [SerializeField] private List<Image> _liveSprites = default;
    [SerializeField] private AudioSource audioSource = default;

    static public int _score { get; private set; }
    public bool IsPaused { get => _isPaused; set => _isPaused = value; }

    // Start is called before the first frame update
    void Start(){
        InputAction pauseMenu = _actionAsset.FindAction("PauseMenu");
        pauseMenu.performed += pauseMenu_performed;

        pauseMenu.Enable();
        _pausePanel.SetActive(false);
    }

    /// <summary>
    /// Cette fonction sert à arrêter le jeu et à afficher le menu
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton pause
    /// </summary>
    private void pauseMenu_performed(InputAction.CallbackContext obj){
        if(!_isPaused){
            _isPaused = true;
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
            _GamePanel.SetActive(false);
        }
        else if(_isPaused){
           Resume_Game();
        }
    }

    private void GamerOverSequence() {
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Cette fonction sert à mettre à jour le score du joueur
    /// Elle s'exécute lorsqu'un ennemi est détruit
    /// </summary>
    private void UpdateScore() {
        _txtScore.text = "Score: " + _score.ToString();
    }

    /// <summary>
    /// Cette fonction sert à résumer le déroulement du jeu
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton pause alors que le jeu est arrêté
    /// </summary>
    public void Resume_Game(){
        _isPaused = false;
        _GamePanel.SetActive(true);
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Cette fonction sert à mettre à jour le nombre de vie affiché dans le ui 
    /// Elle s'exécute lorsque le joueur perd une vie
    /// </summary>
    public void RemovelifeDisplay() {
        _liveSprites.Remove(_liveSprites[0]);
        if(_liveSprites.Count == 0){
            GamerOverSequence();
        }
    }

    /// <summary>
    /// Cette fonction sert à mettre à jour le score du joueur dans le ui
    /// Elle s'exécute lorsqu'un ennemi est détruit
    /// </summary>
    /// <param name="points">nombre de points à ajouter</param>
    public void AjouterScore(int points) {
        _score += points;
        UpdateScore();
    }

    /// <summary>
    /// Cette fonction sert à retourner au menu principal
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton menu principal
    /// </summary>
    public void GoMainMenu() {
        SceneManager.LoadScene(0);
    }
    
    public void Toggle_sound() {
        audioSource.mute = !audioSource.mute;
    }
}
