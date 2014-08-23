using UnityEngine;
using System.Collections;

public class Enemy_PlayerAttacking_ParticleTest : MonoBehaviour
{

    public delegate void PlayerAttackingHandler();
    public event PlayerAttackingHandler OnAttackingPlayer = delegate { };
    public ParticleSystem PsOne;
    public ParticleSystem PsTwo;

    private Player_Health playerHealth;

    public int damagePercent = 40;
    public float damageCooldown = 1;

    private bool isDamaging = true;

    void Start()
    {
        playerHealth = GameObject.FindObjectOfType<Player_Health>();
        //PsOne = GetComponent<ParticleSystem>();
        //PsTwo = GetComponent<ParticleSystem>();
        Debug.Log(PsOne);
        Debug.Log(PsTwo);
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("TestIt");
        //Debug.Log(other.tag);

        if (other.tag == "Player" && isDamaging)
        {
            StartCoroutine(DamagePlayer());
            StartCoroutine(PlayEmission());
        }
    }

    IEnumerator PlayEmission()
    {

        PsOne.Play();
        PsTwo.Play();

        yield return new WaitForSeconds(damageCooldown);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter...Yeah!");
    }


    IEnumerator DamagePlayer()
    {
        playerHealth.health -= damagePercent;
        OnAttackingPlayer();
        isDamaging = false;

        yield return new WaitForSeconds(damageCooldown);

        isDamaging = true;
    }
}
