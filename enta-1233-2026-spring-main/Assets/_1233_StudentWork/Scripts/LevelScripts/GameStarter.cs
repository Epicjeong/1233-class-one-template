using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartWhenReady());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartWhenReady()
    {
        Debug.Log("Requesting level load");
        LevelMgr.Instance.LoadCurrentLevel();

        Debug.Log("Waiting for loading");
        yield return new WaitUntil(() => LevelMgr.Instance.IsLevelLoaded);

        Debug.Log("Spawning player");
        PlayerSpawn spawnPoint = PlayerSpawn.Instance;
        if (spawnPoint == null)
            Debug.LogError("No spawn found");
        else
            PlayerMgr.Instance.SpawnPlayer(spawnPoint.transform.position, spawnPoint.transform.rotation);

        Debug.Log("Waiting for loading");
        yield return new WaitUntil(() => PlayerMgr.Instance.HasSpawnedPlayer);

        //Debug.Log("THE WORLD WILL EXPLODE IN 3 SECONDS");
        //yield return new WaitForSeconds(3f);

        //Debug.Log("explosion dot jay peg");
    }
}
