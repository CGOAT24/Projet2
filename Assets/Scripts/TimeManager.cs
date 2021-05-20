using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour{

    [SerializeField] private TextMeshProUGUI _txtTime = default;
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        _txtTime.text = ((int)Time.time).ToString();
    }
}
