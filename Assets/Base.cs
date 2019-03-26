using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Base : MonoBehaviour {
    public float tiberium = 0;

    public TextMeshPro text;

    public GameObject fighterPrefab;


    // Start is called before the first frame update
    void Start() {
        foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
            r.material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
        }

        Boid.SpawnAndArrive(transform.position, null);
    }

    // Update is called once per frame
    void Update() {
        text.text = "" + Mathf.RoundToInt(tiberium);
        tiberium += Time.deltaTime * 5.0f;

        if (tiberium >= 10) {
            SpawnFighter();
            tiberium -= 10;
        }
    }

    void SpawnFighter() {
        Debug.Assert(tiberium >= 10);
    }
}
