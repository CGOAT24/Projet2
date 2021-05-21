using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float maxDamageDistance;
    private UImanager _uiManager;
    

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UImanager>();
        Destroy(this.gameObject, 5);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject p = Instantiate(explosion, transform.position, Quaternion.identity);
        var ennemy = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject obj in ennemy)
        {
            if (Vector2.Distance(p.transform.position, obj.transform.position) <= maxDamageDistance) {
                Destroy(obj);

            }
                
        }

        Destroy(p, 1);
        Destroy(this.gameObject);
    }
}
