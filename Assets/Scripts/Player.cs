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
    [SerializeField] private GameObject _turrent, _shootPoint;
    [SerializeField] private float _turnSpeed, _turnTurretSpeed, shootSpeed;

    private float _viesJoueur = 10;
    private UImanager _uiManager;
    private Vector3 _direction;
    private Rigidbody2D _rb;
    private bool isMoving = false, isTurning = false;

    // Start is called before the first frame update
    void Start(){
        _rb = this.GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UImanager>();

        Set_Controls();
    }

    private void Update()
    {
        this.transform.position = new Vector2(Mathf.Clamp(this.transform.position.x, -27f, 27f), Mathf.Clamp(this.transform.position.y, -17.5f, 17.5f));
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
        if (!_uiManager.IsPaused) {
            GameObject gm = Instantiate(_missilePrefab, _shootPoint.transform.position, Quaternion.identity);
            gm.transform.rotation = _turrent.transform.rotation;
            Vector3 r = (-gm.transform.right * shootSpeed);
            gm.GetComponent<Rigidbody2D>().velocity = r;
        }
    }

    private void turnAction_performed(InputAction.CallbackContext obj){
        var mouse = obj.ReadValue<Vector2>();
        var screenPoint = Camera.main.WorldToScreenPoint(_turrent.transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        _turrent.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void turnAction_canceled(InputAction.CallbackContext obj){
        
    }

    /// <summary>
    /// Cette fonction sert à mettre à jour la vie du joueur
    /// Cette fonction s'exécute lorsque le joueur prend du dégat
    /// </summary>
    public void Take_Damage() {
        _viesJoueur--;
        _uiManager.RemovelifeDisplay();

        if(_viesJoueur < 1) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.tag.ToString());
        if (collision.tag == "EnnemyBullet") {
            Take_Damage();
        }
        else if (collision.tag == "Ennemy") {
            Destroy(collision.gameObject);
        }
    }
}
