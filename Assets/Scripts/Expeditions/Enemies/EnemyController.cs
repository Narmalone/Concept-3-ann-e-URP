using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance { get; private set; }

    public Character enemy;
    private Character enemy2;
    private Character enemy3;
    [System.NonSerialized] public List<Character> mobGroup;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private string[] enemiesName;

    //Les ennemies ont un ID ce int id correspond au count dans la list des ennemis si le mec raycast et clique sur un ennemi plusieures fonctions
    //Appliquer dégâts(Character target, float value) -> value équivaut aux dégâts du sorts en fonction de l'attaque du mob

    
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
        instance = this;

        mobGroup = new List<Character>();
    }

    private void Start()
    {
        CreateEnemy();
    }
}
