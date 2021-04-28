using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour{

    void Start(){
        
    }

    void Update(){
        transform.Translate(Vector2.up * Time.deltaTime * 12f);
    }
}
