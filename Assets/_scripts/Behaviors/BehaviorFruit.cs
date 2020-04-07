using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorFruit : MonoBehaviour
{
    public FruitSpawnerPoint Spawner;
    public Globals.Species Species;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Call this when a fruit is picked off its branch
    public void Pick() {
        Spawner.FruitPicked();
        Destroy(this);
    }
}
