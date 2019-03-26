using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour {
    public float tiberium = 0;

    public TextMeshPro text;

    public GameObject fighterPrefab;
    bool spawned = false;


    // Start is called before the first frame update
    void Start() {
        foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
            r.material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        }
    }

    // Update is called once per frame
    void Update() {
        text.text = "" + Mathf.RoundToInt(tiberium);
        tiberium += Time.deltaTime * 5.0f;

        if (tiberium >= 10 && !spawned) {
            SpawnFighter();
            tiberium -= 10;
            spawned = true;
        }
    }

    public bool TryRefuel() {
        if (tiberium >= 7) {
            tiberium -= 7;
            return true;
        } else {
            return false;
        }
    }

    void SpawnFighter() {
        Debug.Assert(tiberium >= 10);

        Boid.SpawnAndArrive(transform.position, ChooseTargetBase(), this);
    }

    GameObject ChooseTargetBase() {
        List<Base> otherBases = new List<Base>();
        
        foreach (var base_ in GameObject.FindObjectsOfType<Base>()) {
            if (base_ != this) {
                otherBases.Add(base_);
            }
        }

        return otherBases[Random.Range(0, otherBases.Count)].gameObject;
    }
}
