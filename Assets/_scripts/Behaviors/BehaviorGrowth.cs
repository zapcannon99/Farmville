using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BehaviorGrowth : MonoBehaviour {
    public float timer = 0;
    public float GrowthPeriod = 30f;
    public float SeedlingPeriod = 30f;
    public float SeedPeriod = 30f;

    //Prefabs
    public GameObject Plant;
    public GameObject Fruit;

    public GameObject CurrentForm;

    public bool watered = false;

    public List<Vector3> FruitSpawnPositionOffsets;
    public List<FruitSpawnerPoint> FruitSpawnPoints;
    public float SeedlingSizeRatio = .1f;

    public bool Ready {
        get {
            switch(phase) {
                case Phase.Seed:
                    return timer >= SeedPeriod;
                case Phase.Seedling:
                    return timer >= SeedlingPeriod;
                default:
                    return timer >= GrowthPeriod;
            }
        }
    }

    public enum Phase {
        Empty,
        Seed,
        Seedling,
        Growing,
        Parent,
        Fruit
    }

    public Phase phase = Phase.Seed;
    public Globals.Species species = Globals.Species.Unknown;

    // Start is called before the first frame update
    void Start() {
        FruitSpawnPoints.Capacity = FruitSpawnPositionOffsets.Count;

        CurrentForm = Instantiate(Plant, transform);
        CurrentForm.transform.localScale = Vector3.zero;
        CurrentForm.transform.localPosition = Vector3.zero;

        //Should never need this part, but just in case
        for(int k = 0; k < FruitSpawnPositionOffsets.Count; k++) {
            //If there is no gameobject in the slot, that means the fruit has been picked
            //So we need to regrow it if watered.
            Vector3 offset = FruitSpawnPositionOffsets[k];
            //Parent plant that grows populates the fruit when gameobject no longer there
            GameObject go = new GameObject("Fruit Spawn Point");
            FruitSpawnerPoint fsp = go.AddComponent<FruitSpawnerPoint>();
            fsp.Setup(this, Fruit, offset, species, GrowthPeriod, k);
            FruitSpawnPoints[k] = fsp;
        }
    }

    // Update is called once per frame
    void Update() {
        switch(phase) {
            case Phase.Parent:
                if(watered) {
                    for(int k = 0; k < FruitSpawnPoints.Count; k++) {
                        //So we need to regrow it if watered.
                        //Parent plant that grows populates the fruit when gameobject no longer there
                        FruitSpawnPoints[k].Watered();
                    }
                    watered = false;
                }
                break;
            case Phase.Empty:
                break;
            default:
                if(Ready) {
                    growUp();
                } else {
                    if(watered) {
                        timer += Time.deltaTime;

                        float percent;
                        float heightScalar;
                        switch(phase) {
                            case Phase.Seedling:
                                percent = timer / SeedlingPeriod;
                                heightScalar = SeedlingSizeRatio * percent;
                                CurrentForm.transform.localScale = heightScalar * Vector3.one;
                                break;
                            case Phase.Growing:
                                percent = timer / GrowthPeriod;
                                heightScalar = .1f + .9f * percent;
                                CurrentForm.transform.localScale = heightScalar * Vector3.one;
                                break;
                        }
                    }
                }
                break;
        }
    }

    private void growUp() {
        switch(phase) {
            case Phase.Seed:
                //from seed to seedling
                CurrentForm = Instantiate(Plant, transform);
                CurrentForm.transform.localScale = Vector3.zero;
                phase = Phase.Seedling;
                timer = 0f;
                break;
            case Phase.Seedling:
                phase = Phase.Growing;
                timer = 0f;
                watered = false;
                break;
            case Phase.Growing:
                phase = Phase.Parent;
                timer = 0f;
                watered = false;
                break;
            //case Phase.Parent:
            //    //Do something when cut with scythe or something
            //    break;
            default:
                //if it gets here, we have a problem
                break;
        }
    }

    private void OnDestroy() {
        // We'll figure something out later
    }

    public void Watered() {
        watered = true;
    }
}

