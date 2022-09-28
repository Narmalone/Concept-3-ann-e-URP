using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootingScript : MonoBehaviour
{
    public Loot loot;
    [SerializeField] private Event m_events;
    private Animation m_motionToPlay;
    private float moneyEarned;

    private void Awake()
    {
        m_motionToPlay = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_events?.VectorEvent(other.transform.position);
        m_events?.DoSomething(OnLoot());
    }

    public object OnLoot()
    {
        moneyEarned += loot.Money;
        Debug.Log(moneyEarned);
        m_motionToPlay.Play();
        AnimationManager.instance.PlayAnim(m_motionToPlay, false, "ChestAnim");
        return moneyEarned;
    }
}
