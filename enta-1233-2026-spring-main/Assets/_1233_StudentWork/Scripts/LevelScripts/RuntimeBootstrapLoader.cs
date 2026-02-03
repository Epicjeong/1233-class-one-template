using UnityEngine;

public class RuntimeBootstrapLoader : MonoBehaviour
{
    /// <summary>
    /// Creates game globals once
    /// regardless of where the game is run
    /// </summary>

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (GlobalsMgr.Instance)
            return;

        var prefab = Resources.Load<GameObject>("GameGlobals");
        Object.Instantiate(prefab);
    }
}
