using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorRock : MonoBehaviour
{
    public GameObject Prefab;
    public bool test = false;

    public int Tier = 1;
    public int HP = 9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Testing fragment, REMOVE when complete
        if(test && Input.GetKey(KeyCode.T))
            Break();
    }

    public void Break() {
        Destroy(gameObject);
        GameObject o1 = Instantiate(Prefab, transform.position + Vector3.right, transform.localRotation, transform.parent);
        o1.transform.localScale /= 2;
        GameObject o2 = Instantiate(Prefab, transform.position + Vector3.left, transform.localRotation, transform.parent);
        o2.transform.localScale /= 2;
    }
}
