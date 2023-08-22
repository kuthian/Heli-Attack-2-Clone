using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

  [field:SerializeField]
  public int MaxHealth { get; set; } = 100;

  [field:SerializeField]
  public int CurrentHealth { get; set; }

  [field:SerializeField]
  public bool Invincible { get; set; }

  public delegate void _OnHealthChanged(int health);
  public event _OnHealthChanged OnHealthChanged; 

  public delegate void _OnHealthZero();
  public event _OnHealthZero OnHealthZero;

  void Start()
  {
    CurrentHealth = MaxHealth;
  }

  public void Damage( int points )
  {
    if ( Invincible ) return;

    if (CurrentHealth > 0)
    {
      CurrentHealth -= points;
      if (CurrentHealth < 0) CurrentHealth = 0;
      if (CurrentHealth == 0) OnHealthZero?.Invoke();
      OnHealthChanged?.Invoke(CurrentHealth);
    }
  }

  public void Heal( int points )
  {
    CurrentHealth += points;
    if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    OnHealthChanged?.Invoke(CurrentHealth);
  }

}