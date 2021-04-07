using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour{

    [SerializeField] private float _vitesse = 10f;
    [SerializeField] private InputActionAsset _actionAsset = default;
    [SerializeField] private GameObject _missilePrefab = default;

    private Vector3 _direction;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start(){
        _rb = this.GetComponent<Rigidbody>();

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
}
