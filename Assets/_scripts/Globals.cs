using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Globals : MonoBehaviour {

    // Static variables for all access

    //Prefabs
    public AssetReference ReferenceTomato;
    public static GameObject PrefabTomato;

    public enum Species {
        Unknown,
        Tomato,
        Eggplant,
        Turnip,
        Carrot,
        Pumpkin,
        Corn
    }

    private void Start() {
        Addressables.LoadAssetAsync<GameObject>(ReferenceTomato).Completed += op => {
            if(op.Status == AsyncOperationStatus.Succeeded) {
                PrefabTomato = op.Result;
            }
        };
    }

    public static Material GetHighlightMaterial() {
        return Resources.Load<Material>("Outlined");
    }

    //public static GameObject GetPrefab(Species species) {
    //    Globals g = GameObject.Find("World").GetComponent<Globals>();
    //    switch(species) {
    //        case Species.Tomato:
    //            return g.PrefabTomato;
    //        default:
    //            return null;
    //    }
    //}
}
