using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100;

    public float armorRating = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageDealt(float damage)
    {


        if (gameObject.GetComponent<Animator>() != null)
        {
            gameObject.GetComponent<Animator>().Play("Enemy_Hurt", -1, 0);
            // Damage = 1 + AR/10, armor rating of 10 = 50% damage reduction.
            // Calculate damage reduction with 
            health -= damage / (1 + (armorRating / 10));
        }
        else
        {
            health -= damage / (1 + (armorRating / 10));
        }

        CheckDeath();
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
