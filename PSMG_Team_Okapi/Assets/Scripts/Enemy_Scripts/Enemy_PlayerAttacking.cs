using UnityEngine;
using System.Collections;

public class Enemy_PlayerAttacking : MonoBehaviour {

    private Player_Status playerStatus;

    public int damagePercent = 40;
    public float damageCooldown = 1;

    private bool isDamaging = true;

    void Start()
    {
        playerStatus = GameObject.FindObjectOfType<Player_Status>();
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Player" && isDamaging)
        {
            StartCoroutine(DamagePlayer());        
        }
    }

    IEnumerator DamagePlayer() 
    {
        playerStatus.health -= damagePercent;
        isDamaging = false;

        yield return new WaitForSeconds(damageCooldown);

        isDamaging = true;
    }
}
