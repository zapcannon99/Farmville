using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorLog : MonoBehaviour
{
    public GameObject destroyedVersion;
    public float mass;

    private void Start() {
        destroyedVersion = gameObject;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.G)) {
            split();
        }
    }

    void split()
    {
        mass = destroyedVersion.GetComponent<Rigidbody>().mass;
        for (int x = 0; x < 2; x++)
        {
            createPiece(x);
        }

        Destroy(destroyedVersion);
    }

    void createPiece(int x)
    {
        //create piece
        GameObject o1 = Instantiate(destroyedVersion, transform.position + Vector3.right, transform.localRotation, transform.parent);
        o1.transform.localScale /= 2;

        //add rigidbody and set mass
        o1.AddComponent<Rigidbody>();
        o1.GetComponent<Rigidbody>().mass = mass / 2;
    }
}
