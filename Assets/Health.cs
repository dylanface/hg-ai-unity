using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startingHealth = 100f;

    public float currentHealth
    {
        get
        { return _currentHealth; }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0f, startingHealth);

            if (_currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    [SerializeField]
    private float _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
