using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    [SerializeField]
    private int _minHealth = 1;
    [SerializeField]
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if(_currentHealth < _minHealth)
        {
            Destroy(gameObject);
        }
    }

}
