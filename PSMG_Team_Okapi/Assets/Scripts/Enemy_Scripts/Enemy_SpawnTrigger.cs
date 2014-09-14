using UnityEngine;
using System.Collections;

public class Enemy_SpawnTrigger : MonoBehaviour {

    // wird an ein leeres Objekt angehängt, dessen Collider der Trigger ist; 
    // wenn kein spawnpunkt spezifisch festgelegt wird, ist das leere Obekt = spawnpunkt
    // delay-time: zeit zwischen trigger-aktivierung und gegner-spawn
    // waypoints: falls nicht spezifiziert, waypoint = spawnpunkt, d.h. enemy bleibt dort stehen.

    public int delay = 5;
    public bool triggerActive = true;

    public Transform spawnpoint;
    public Transform[] patrolpoints;

    public Transform enemy;

	// Use this for initialization
	void Start () 
    {
        if(spawnpoint == null)
        {
            spawnpoint = transform;
        }

        if (patrolpoints.Length == 0)
        {
            patrolpoints = new Transform[1] { spawnpoint };
        }
	}

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("TestIt");
        // Debug.Log(other.tag);

        if (other.tag == "Player" && triggerActive)
        {
            StartCoroutine(StartSpawnSequence());
        }
    }

    IEnumerator StartSpawnSequence()
    {
        triggerActive = false;
        yield return new WaitForSeconds(delay);
        Spawn();
    }

    private void Spawn()
    {
        // Debug.Log("Spawning");
        Transform newEnemy = (Transform) Instantiate(enemy, spawnpoint.position, Quaternion.identity);
        Transform[] patrolPointsTemp = newEnemy.GetComponent<Enemy_Behaviour>().patrolpointsIdle;
        patrolPointsTemp = patrolpoints;
        newEnemy.GetComponent<Enemy_Behaviour>().patrolpointsIdle = patrolPointsTemp;
    }
}
