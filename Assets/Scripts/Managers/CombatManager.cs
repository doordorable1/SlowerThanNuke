using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager
{
    List<GameObject> _enemyTypes = new List<GameObject>();
    GameObject bossPrefab;
    List<EnemyController> _enemiesOnField = new List<EnemyController>();
    GameObject[] spawnAreas;

    public void Init()
    {
        spawnAreas = GameObject.FindGameObjectsWithTag("SpawnArea");
        GameObject[] enemies = Resources.LoadAll<GameObject>("Enemy");
        bossPrefab = Resources.Load<GameObject>("BossEnemy/Boss");
        foreach (GameObject enemy in enemies)
        {
            _enemyTypes.Add(enemy);
        }
        Debug.Log(_enemyTypes.Count);
    }

    public void SpawnEnemy()
    {
        int enemyCount = Random.Range(1, 4);
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyPrefab = Object.Instantiate(_enemyTypes[Random.Range(0, _enemyTypes.Count)]);
            enemyPrefab.transform.position = spawnAreas[i].transform.position;
            _enemiesOnField.Add(enemyPrefab.GetComponent<EnemyController>());
        }
    }

    public void SpawnBoss()
    {
        GameObject boss = Object.Instantiate(bossPrefab);
        boss.transform.position = spawnAreas[1].transform.position;
        _enemiesOnField.Add(boss.GetComponent<EnemyController>());
    }

    public void RemoveEnemy(EnemyController enemy) { 
        _enemiesOnField.Remove(enemy);
        if (_enemiesOnField.Count <= 0)
        {
            EndCombat();
        }
    }

    void EndCombat()
    {
        if(GameManager.Instance.CurrentState == State.BossFight)
        {
            GameManager.Instance.GameOver(0);
            return;
        }
        GameManager.Instance.ChangeState(State.Idle);
        GameManager.UI.ToggleEndCombatCanvas(true);
    }
}
