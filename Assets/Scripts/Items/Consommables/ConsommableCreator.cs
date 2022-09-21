using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsommableCreator : MonoBehaviour
{
    public static ConsommableCreator instance { get; private set; }

    private List<Consommable> m_consommableList;
    private HealingPotion healingPotion;
    private StrenghtPotion strenghtPotion;
    private void Awake()
    {
        instance = this;

        //10 %
        healingPotion = new HealingPotion(null, "Healing Potion", "A potion that healing a character", 0, 10f);
        strenghtPotion = new StrenghtPotion(null, "Strenght Potion", "A potion that improve the strenght of a character", 1, 10f);

        AddConsommableToList();
    }
    public void AddConsommableToList()
    {
        m_consommableList.Add(healingPotion);
        m_consommableList.Add(strenghtPotion);
    }
}
