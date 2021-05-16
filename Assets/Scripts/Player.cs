using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;
using System;

public class Player : MonoBehaviour {

    [SerializeField] private float _vitesse = 10f;
    [SerializeField] private InputActionAsset _actionAsset = default;
    [SerializeField] private GameObject _missilePrefab = default;
    [SerializeField] private GameObject _turrent;
    [SerializeField] private float _turnSpeed, _turnTurretSpeed;

    private float _viesJoueur = 3;
    //private UImanager _uiManager;
    private Vector3 _direction;
    private Rigidbody2D _rb;
    private bool isMoving = false, isTurning = false;

    // Start is called before the first frame update
    void Start(){
        _rb = this.GetComponent<Rigidbody2D>();
        //_uiManager = GameObject.Find("UIManager").GetComponent<UImanager>();

        Set_Controls();
    }

    /// <summary>
    /// Cette fonction sert à mettre en place les controles du joueur
    /// Elle s'exécute au début du jeu
    /// </summary>
    private void Set_Controls(){
        InputAction moveAction = _actionAsset.FindAction("Move_Forward");
        moveAction.performed += moveAction_performed;
        moveAction.canceled += moveAction_canceled;
        moveAction.Enable();

        InputAction fireAction = _actionAsset.FindAction("Fire");
        fireAction.performed += fireAction_performed;
        fireAction.Enable();

        InputAction turnAction = _actionAsset.FindAction("Turn_Turret");
        turnAction.performed += turnAction_performed;

        turnAction.Enable();

        InputAction turnBody = _actionAsset.FindAction("Turn_Body");
        turnBody.performed += turnBody_performed;
        turnBody.canceled += turnBody_canceled;
        turnBody.Enable();
    }

    /// <summary>
    /// Cette fonction sert à arrêter la rotation du joueur
    /// Elle s'exécute lorsque l'utilisateur lâche le bouton de rotation
    /// </summary>
    private void turnBody_canceled(InputAction.CallbackContext obj)
    {
        if (isTurning)
        {
            isTurning = false;
            _rb.angularVelocity = 0;
        }
    }

    /// <summary>
    /// Cette fonction sert à faire une rotation du joueur
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton de rotation
    /// </summary>
    private void turnBody_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("test");
        if (!isMoving)
        {
            isTurning = true;
            Vector2 m = obj.ReadValue<Vector2>();
            _rb.AddTorque(_turnSpeed* m.y, ForceMode2D.Force);
        }
    }

    /// <summary>
    /// Cette fonction sert à déplacer le joueur
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton de déplacement
    /// </summary>
    private void moveAction_performed(InputAction.CallbackContext obj){
        if (!isTurning)
        {
            isMoving = true;
            Vector2 m = obj.ReadValue<Vector2>();
            Vector3 r = (-transform.right * _vitesse) * m.y;
            _rb.velocity = r;
        }
    }

    /// <summary>
    /// Cette fonction sert à arrêter le déplacement du joueur
    /// Elle s'exécute lorsque l'utilisateur lâche le bouton de déplacement
    /// </summary>
    private void moveAction_canceled(InputAction.CallbackContext obj){
        if (isMoving)
        {
            isMoving = false;
            _direction = new Vector3(0f, 0f, 0f);

            _rb.velocity = _direction * _vitesse;
        }
    }

    /// <summary>
    /// Cette fonction sert à instancier un gameobject de type missile
    /// Elle s'exécute lorsque l'utilisateur appuie sur le bouton de tir
    /// </summary>
    private void fireAction_performed(InputAction.CallbackContext obj){
        Instantiate(_missilePrefab, transform.position, Quaternion.identity);
    }

    /*
    *
    *   À FAIRE
    *
    */
    private void turnAction_performed(InputAction.CallbackContext obj){
        Vector2 m = obj.ReadValue<Vector2>();
        Vector3 diff = new Vector3(m.x, m.y, 0) - this.transform.position;
        _turrent.transform.right = diff* _turnTurretSpeed;
    }

    /*
    *
    *   À FAIRE
    *
    */
    private void turnAction_canceled(InputAction.CallbackContext obj){
        
    }

    /// <summary>
    /// Cette fonction sert à mettre à jour la vie du joueur
    /// Cette fonction s'exécute lorsque le joueur prend du dégat
    /// </summary>
    public void Take_Damage() {
        _viesJoueur--;
        //_uiManager.RemovelifeDisplay();

        if(_viesJoueur < 1) {
            Destroy(this.gameObject);
        }
    }
}
