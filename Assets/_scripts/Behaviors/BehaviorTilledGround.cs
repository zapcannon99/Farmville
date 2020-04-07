using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTilledGround : MonoBehaviour {
    public BehaviorGrowth growth;
    bool pressed = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //TESTING PURPOSES ONLY
        if(!pressed && Input.GetKey(KeyCode.H)) {
            pressed = true;
            SowSeed(Globals.PrefabTomato);
        }

        //TESTING PURPOSES ONLY
        if(Input.GetKey(KeyCode.Y)) {
            Watered();
        }
    }

    public void SowSeed(GameObject prefab) {
        growth = Instantiate(prefab, transform).GetComponent<BehaviorGrowth>();
    }

    public void Watered() {
        growth.Watered();
    }
}
