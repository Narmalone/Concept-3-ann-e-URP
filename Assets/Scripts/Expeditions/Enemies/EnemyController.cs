using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Character enemy;
    private Character enemy2;
    private Character enemy3;
    private List<Character> mobGroup;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private string[] enemiesName;

    public void CreateEnemy()
    {
        enemy = new Character(enemiesName[Random.Range(0, enemiesName.Length)], Random.Range(10, 15), Random.Range(5, 10), Random.Range(5, 10), 0, SpellsManager.instance.RandomSpell);
        enemy2 = new Character(enemiesName[Random.Range(0, enemiesName.Length)], Random.Range(10, 15), Random.Range(5, 10), Random.Range(5, 10), 0, SpellsManager.instance.RandomSpell);
        enemy3 = new Character(enemiesName[Random.Range(0, enemiesName.Length)], Random.Range(10, 15), Random.Range(5, 10), Random.Range(5, 10), 0, SpellsManager.instance.RandomSpell);

        mobGroup.Add(enemy);
        mobGroup.Add(enemy2);
        mobGroup.Add(enemy3);
    }

    private void Awake()
    {
        mobGroup = new List<Character>();
    }

    private void Start()
    {
        CreateEnemy();

        foreach (Character chara in mobGroup)
        {
            Debug.Log(chara.CharactersName);
        }
    }
}
