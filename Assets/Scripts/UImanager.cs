using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UImanager : MonoBehaviour{
    private bool _isPaused = false; 

    [SerializeField] private InputActionAsset _actionAsset = default;

    void Start(){
        InputAction pauseMenu = _actionAsset.FindAction("PauseMenu");
        pauseMenu.performed += pauseMenu_performed;
        pauseMenu.Enable();
    }

    void pauseMenu_performed(InputAction.CallbackContext obj){
        if(!_isPaused){
            _isPaused = true;
            Time.timeScale = 0;
        }
        else{
            _isPaused = false;
            Time.timeScale = 1;
        }
    }
}
