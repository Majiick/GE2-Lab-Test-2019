using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillMe", 10);
    }

    public void KillMe()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.name);
        if (collider.GetComponent<Base>() != null) {
            collider.GetComponent<Base>().TakeDamage(0.5f);
            KillMe();
        }
    }

    public static void SpawnBullet(Vector3 spawnPos, GameObject targetBase, Boid parentBoid) {
        Debug.Assert(targetBase.GetComponent<Base>() != null);
        GameObject bulletPrefab = Resources.Load("bullet") as GameObject;

        GameObject go = GameObject.Instantiate(bulletPrefab);
        go.transform.position = spawnPos;
        go.transform.LookAt(targetBase.transform.position);

        go.GetComponent<Renderer>().material.color = parentBoid.GetComponent<Renderer>().material.color;
    }
}
