using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

  [SerializeField] private int maxHealth = 100;
  private int currentHealth;

  public int MaxHealth => maxHealth;
  public int CurrentHealth => currentHealth;

  public delegate void _OnHealthChanged(int health);
  public event _OnHealthChanged OnHealthChanged;

  public delegate void _OnHealthZero();
  public event _OnHealthZero OnHealthZero;

  void Start()
  {
    currentHealth = maxHealth;
  }

  public void Damage( int points )
  {
    if (currentHealth > 0)
    {
      currentHealth -= points;
      if (currentHealth < 0) currentHealth = 0;
      if (currentHealth == 0) OnHealthZero?.Invoke();
      OnHealthChanged?.Invoke(currentHealth);
    }
  }

  public void Heal( int points )
  {
    currentHealth += points;
    if (currentHealth > maxHealth) currentHealth = maxHealth;
    OnHealthChanged?.Invoke(currentHealth);
  }

}