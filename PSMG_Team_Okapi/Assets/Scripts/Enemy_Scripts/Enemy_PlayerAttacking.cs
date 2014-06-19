using UnityEngine;
using System.Collections;

public class Enemy_PlayerAttacking : MonoBehaviour {

    private Player_Health playerHealth;

    public int damagePercent = 40;
    public float damageCooldown = 1;

    private bool isDamaging = true;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<Player_Health>();
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
        playerHealth.health -= damagePercent;
        isDamaging = false;

        yield return new WaitForSeconds(damageCooldown);

        isDamaging = true;
    }
}
