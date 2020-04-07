using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnerPoint : MonoBehaviour {
    public BehaviorGrowth Parent;
    public GameObject FruitPrefab;
    public GameObject FruitInstance;
    public int index;

    public bool growing = false;
    public bool watered = false;
    public bool ripe = false;

    public float timer = 0f;
    public float GrowthPeriod = 60;

    public Globals.Species Species;

    public float FruitMass = .125f;

    public bool Ready {
        get { return timer >= GrowthPeriod; }
    }


    // Start is called before the first frame update
    void Start() {

    }

    public void Setup(BehaviorGrowth parent, GameObject prefab, Vector3 offset, Globals.Species species, float growthPeriod, int index) {
        Parent = parent;
        gameObject.transform.parent = parent.transform;
        FruitPrefab = prefab;
        this.index = index;
        transform.localPosition = offset;
        Species = species;
        GrowthPeriod = growthPeriod;
    }

    // Update is called once per frame
    void Update() {
        if(growing && watered) {
            timer += Time.deltaTime;
            float percent = timer / GrowthPeriod;
            FruitInstance.transform.localScale = percent * Vector3.one;
            FruitInstance.GetComponent<BehaviorFruit>().Spawner = this;
            if(Ready) {
                Rigidbody rb = FruitInstance.AddComponent<Rigidbody>();
                rb.mass = FruitMass;
                rb.isKinematic = true;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                growing = false;
                watered = false;
                ripe = true;
                timer = 0f;
            }
        }
        if(ripe) {
            //check if thing has been picked
            if(transform.childCount == 0) {
                FruitPicked();
            }
        }
    }
    public void Watered() {
        watered = true;
        StartGrowing();
    }

    public void StartGrowing() {
        if(!ripe && !growing) {
            growing = true;
            FruitInstance = Instantiate(FruitPrefab, transform);
            FruitInstance.transform.localScale = Vector3.zero;
            FruitInstance.GetComponent<BehaviorFruit>().Species = Species;
        }
    }

    public void FruitPicked() {
        ripe = false;
        FruitInstance = null;
    }
}
