using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    StateMachine stateMachine;
    public GameObject targetBase;
    public GameObject parentBase;
    public float tiberium;
    public float timeLastFiredShot = 0;

    /*
    public enum FighterState { ArrivingToAttackPosition, Shooting, GoingBackToBase };
    FighterState fighterState = FighterState.ArrivingToAttackPosition;
    */

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = gameObject.AddComponent<StateMachine>();
        stateMachine.ChangeState(new ArrivingToAttackPos(this));
        tiberium = 7;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        switch (fighterState) {
            case FighterState.ArrivingToAttackPosition:
                GetComponent<Arrive>().targetPosition = Vector3.Lerp(parentBase.transform.position, targetBase.transform.position, 0.8f);
                if (Vector3.Distance(transform.position, GetComponent<Arrive>().targetPosition) < 10f) {
                    fighterState = FighterState.Shooting;
                    Debug.Log("Switching state to shooting.");
                }
                break;
            case FighterState.GoingBackToBase:
                GetComponent<Arrive>().targetPosition = parentBase.transform.position;
                if (Vector3.Distance(transform.position, parentBase.transform.position) <= 5f) {
                    if (parentBase.GetComponent<Base>().TryRefuel()) {
                        fighterState = FighterState.ArrivingToAttackPosition;
                        tiberium = 7;
                        Debug.Log("Switching state to arriving to attack positon.");
                    }
                }
                break;
            case FighterState.Shooting:
                if (Time.time >= timeLastFiredShot + 0.6f) {
                    if (tiberium >= 1) {
                        FireShot();
                        timeLastFiredShot = Time.time;
                        tiberium -= 1.0f;
                    } else {
                        Debug.Log("Switching state to going back to base.");
                        fighterState = FighterState.GoingBackToBase;
                    }
                }
                break;
        }
        */
    }

    void FireShot() {
        Bullet.SpawnBullet(transform.position, targetBase, this);
    }

    public static void Spawn(Vector3 spawnPos, GameObject targetBase, Base parentBase) {
        Debug.Assert(targetBase.GetComponent<Base>() != null);
        Debug.Assert(parentBase.GetComponent<Base>() != null);
        GameObject fighterPrefab = Resources.Load("fighter") as GameObject;

        // Instantiate fighter and set its public variables, position and color
        GameObject go = GameObject.Instantiate(fighterPrefab);
        go.transform.position = spawnPos;
        go.GetComponent<Fighter>().targetBase = targetBase;
        go.GetComponent<Fighter>().parentBase = parentBase.gameObject;
        go.GetComponent<Renderer>().material.color = parentBase.GetComponent<Renderer>().material.color;
        // Set children colors as well
        foreach (Transform child in go.transform) {
            if (child.GetComponent<Renderer>() != null) {
                child.GetComponent<Renderer>().material.color = parentBase.GetComponent<Renderer>().material.color;
            }
        }

        // Add arrive to fighter
        var arr = go.AddComponent<Arrive>();
        arr.targetPosition = Vector3.Lerp(spawnPos, targetBase.transform.position, 0.8f);
    }
}
