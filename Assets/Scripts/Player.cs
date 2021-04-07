using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour{

    [SerializeField] private float _vitesse = 10f;
    [SerializeField] private InputActionAsset _actionAsset = default;
    [SerializeField] private GameObject _missilePrefab = default;

    private Vector3 _direction;
    private float _rotation;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start(){
        _rb = this.GetComponent<Rigidbody2D>();

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
        _direction = new Vector3(0, direction2D.y, 0);
        _direction.Normalize();
        _rb.velocity = _direction * _vitesse;

        _rotation = direction2D.x;
        _rb.AddTorque(_rotation * _vitesse);
    }

    private void moveAction_canceled(InputAction.CallbackContext obj){
        _direction = new Vector3(0f, 0f, 0f);
        _rb.velocity = _direction * _vitesse;

        _rotation = 0f;
    }

    private void fireAction_performed(InputAction.CallbackContext obj){
        Instantiate(_missilePrefab, transform.position, Quaternion.identity);
    }

    private void turnAction_performed(InputAction.CallbackContext obj){

    }

    private void turnAction_canceled(InputAction.CallbackContext obj){

        
    }
}
