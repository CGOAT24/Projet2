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

    public int _score { get; private set; }

    void Start(){
        InputAction pauseMenu = _actionAsset.FindAction("PauseMenu");
        pauseMenu.performed += pauseMenu_performed;

        pauseMenu.Enable();
        _pausePanel.SetActive(false);
    }

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

    }

    private void UpdateScore() {
        _txtScore.text = "Score: " + _score.ToString();
    }

    public void Resume_Game(){
            _isPaused = false;
            _GamePanel.SetActive(true);
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
    }

    public void RemovelifeDisplay() {
        _liveSprites.RemoveAt(_liveSprites.Count);
        if(_liveSprites.Count == 0){
            GamerOverSequence();
        }
        else {

        }
    }

    public void AjouterScore(int points) {
        _score += points;
        UpdateScore();
    }

    public void GoMainMenu() {
        SceneManager.LoadScene(0);
    }

}
