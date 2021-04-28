using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField] private float _vitesse = 10f;
    [SerializeField] private InputActionAsset _actionAsset = default;
    [SerializeField] private GameObject _missilePrefab = default;
    [SerializeField] private GameObject _turrent;
    [SerializeField] private float _turnSpeed;

    private float _viesJoueur = 3;
    private UImanager _uiManager;
    private Vector3 _direction;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start(){
        _rb = this.GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UImanager>();

        Set_Controls();
    }

    private void Set_Controls(){
        InputAction moveAction = _actionAsset.FindAction("Move");
        moveAction.performed += moveAction_performed;
        moveAction.canceled += moveAction_canceled;
        moveAction.Enable();

        InputAction fireAction = _actionAsset.FindAction("Fire");
        fireAction.performed += fireAction_performed;
        fireAction.Enable();

        InputAction turnAction = _actionAsset.FindAction("Turn");
        turnAction.performed += turnAction_performed;
        turnAction.canceled += turnAction_canceled;
        turnAction.Enable();
    }

    private void moveAction_performed(InputAction.CallbackContext obj){
        Vector2 direction2D = obj.ReadValue<Vector2>();
        _direction = new Vector3(direction2D.x, direction2D.y, 0);
        _direction.Normalize();
        _rb.velocity = _direction * _vitesse;
    }

    private void moveAction_canceled(InputAction.CallbackContext obj){
        _direction = new Vector3(0f, 0f, 0f);
        _rb.velocity = _direction * _vitesse;
    }

    private void fireAction_performed(InputAction.CallbackContext obj){
        Instantiate(_missilePrefab, transform.position, Quaternion.identity);
    }

    /*
    *
    *   À FAIRE
    *
    */
    private void turnAction_performed(InputAction.CallbackContext obj){
        Vector2 diff = obj.ReadValue<Vector2>();

        _turrent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (transform.eulerAngles.z + diff.y)*_turnSpeed));
    }

    /*
    *
    *   À FAIRE
    *
    */
    private void turnAction_canceled(InputAction.CallbackContext obj){
        
    }

    public void Take_Damage() {
        _viesJoueur--;
        _uiManager.RemovelifeDisplay();

        if(_viesJoueur < 1) {
            Destroy(this.gameObject);
        }
    }
}
