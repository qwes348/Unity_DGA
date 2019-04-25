using System;

// delegate void GameEvent();

class LivingEntity
{
  public float health = 100;
  public virtual void TakeDamage(float damage)
  {
    health -= damage;
    Console.WriteLine("HP: " + health);
  }
}

class Enemy : LivingEntity
{
  public event Action OnDeath;
  // public event GameEvent OnDeath;  // 똑같음

  public override void TakeDamage(float damage)
  {
    if(damage >= health)
    {
      OnDeath();
      health = 0;
    }
    else
    {
      base.TakeDamage(damage);
    }
  }

  public void DrawEffect()
  {
    Console.WriteLine("Death Effect!");
  }
}

class Player : LivingEntity
{
  public int XP { get; private set; } = 100;
  public override void TakeDamage(float damage)
  {
    if(damage >= health)
    {
      Console.WriteLine("Revive");
      health = 100;
    }
    else
    {
      base.TakeDamage(damage);
    }
  }

  public void IncreaseExp()
  {
    XP += 50;
    Console.WriteLine("Add user's XP: " + XP);
  }
}

class MainClass 
{
  public static void Main (string[] args) 
  {
    Enemy e = new Enemy();
    Player p = new Player();

    e.OnDeath += e.DrawEffect;
    e.OnDeath += p.IncreaseExp;

    Console.WriteLine(p.XP == 100);
    e.TakeDamage(120);
    Console.WriteLine(p.XP == 150);

    // e.OnDeath();  // event
  }
}