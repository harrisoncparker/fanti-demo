using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneManager : MonoBehaviour
{
    bool fantisLoaded = false;

    [SerializeField] Fanti _fantiPrefab;
    [SerializeField] HomeSpawnPoint[] _spawnPoints;

    void Start()
    {
        if (!fantisLoaded) LoadFantis();
    }

    public void LoadFantis()
    {
        List<FantiModel> fantis = SaveDataManager.Instance.CurrentPlayerModel.fantis;

        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned in the HomeSceneManager.");
            // Eventually these should be more like spawn surfaces than points
            return;
        }

        if (fantis == null || fantis.Count == 0)
        {
            Debug.LogWarning("No Fantis found in the player's save data.");
            // This could be an opportunity to point players back to the Fantui shelter
            return;
        }

        IEnumerator spawnPointsEnumerator = _spawnPoints.GetEnumerator();

        foreach (FantiModel model in fantis)
        {
            if (!spawnPointsEnumerator.MoveNext())
            {
                Debug.LogWarning("Not enough spawn points for all Fantis.");
                break;
            }

            HomeSpawnPoint spawnPoint = (HomeSpawnPoint)spawnPointsEnumerator.Current;

            Vector2 spawnPosition = spawnPoint.transform.position + new Vector3(Random.Range(-3.0f, 3.0f),0,0);

            Fanti newFanti = Instantiate(_fantiPrefab, spawnPosition, spawnPoint.transform.rotation);
            newFanti.Model = model;

            newFanti.transform.SetParent(spawnPoint.transform);
        }

        fantisLoaded = true;
    }
}
