using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UImanager : MonoBehaviour{
    private bool _isPaused = false; 

    [SerializeField] private InputActionAsset _actionAsset = default;
    [SerializeField] private GameObject _pausePanel = default;

    void Start(){
        InputAction pauseMenu = _actionAsset.FindAction("PauseMenu");
        pauseMenu.performed += pauseMenu_performed;

        pauseMenu.Enable();
        _pausePanel.SetActive(false);
    }

    void pauseMenu_performed(InputAction.CallbackContext obj){
        if(!_isPaused){
            _isPaused = true;
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
        }
        else if(_isPaused){
           Resume_Game();
        }
    }

    public void Resume_Game(){
            _isPaused = false;
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
    }
}
