using System;
using System.Collections.Generic;

class MainClass 
{
  public static void Main (string[] args) 
  {

    Unit knight = new Unit("knight", "Knight is tough warrior and ...");
    Console.WriteLine(knight.KindofStat());
    knight.level = 6;
    knight.elixirCost = 3;
    knight.hp = 960;
    knight.damage = 120;
    knight.damageSec = 109;
    knight.upgradeCount = 18;

    knight.spawnTime = 1.0;
    knight.akSpeed = 1.1;

    knight.rarity = Unit.Rarity.Common;  // enum

  }
}

public class Unit
{
  public string name; //Unit
  public string description; //Unit

  public int level; //stat
  public int elixirCost; // info
  public int hp;    //stat
  public int damage; //stat
  public int damageSec; //stat
  public int upgradeCount; //stat

  public double spawnTime; //stat
  public double akSpeed;  //stat

  public enum Rarity {Common, Rare, Epic, Legendary}; // info
  public Rarity rarity;
  public enum Type {Troop, Building, Spell}; // info
  public Type type;
  public enum WkSpeed {Slow, Medium, Fast, VFast};  //stat
  public WkSpeed wkspeed;
  public enum Targeting {Ground, Building, GRnAR}; // stat
  public Targeting targeting;
  public enum AkRange {Melee, Ranged};  // stat
  public AkRange akRange;


  public Unit(string name, string description)
  {
    this.name = name;
    this.description = description;
  }


  public string KindofStat()
  {
    return "int: " +"level, hp, damage, damageSec, spawnTime, upgradeCount," + "double: " + "akSpeed," + "enum" + "wkSpeed, targeting, akRange";
  }
  // enum = 희귀도;, 유형;, 이동속도;, 공격대상;, 사정거리;
  // int = 레벨;, 소환값;, 업그레이드 진행도, hp;, 피해량;, 초당피해량;, 배치시간;
  // double = 공격속도;
  // string = 이름;, 설명;
}

// 1.기사클래스 만들어오기
// 2.버블정렬