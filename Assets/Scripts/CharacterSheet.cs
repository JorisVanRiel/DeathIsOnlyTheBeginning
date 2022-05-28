using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Sheet")]
public class CharacterSheet : ScriptableObject
{
    [SerializeField] int startingAttack = 10;
    [SerializeField] int startingDefence = 2;

    [SerializeField] int experiencePoints;
    [SerializeField] int attack;
    [SerializeField] int defence;

    public int ExperiencePoints { get { return experiencePoints; } }
    public int Attack { get { return attack; } }
    public int Defence { get { return defence; } }

    public void AddExperiencePoints(int amount)
    {
        experiencePoints += amount;
    }

    public void Reset()
    {
        experiencePoints = 0;
        attack = startingAttack;
        defence = startingDefence;
    }

    public void AttackUp()
    {
        if (experiencePoints < attack) return;
        experiencePoints -= attack;
        attack++;
    }

    public void DefenceUp()
    {
        if(experiencePoints < defence) return;
        experiencePoints -= defence;
        defence++;
    }
}
