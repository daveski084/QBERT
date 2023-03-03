
using UnityEngine;

public static class LoadPersistantObjects 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        var resource = Resources.Load(path: "---Managers---"); 
        if(resource != null)
        {
            Object.DontDestroyOnLoad(Object.Instantiate(resource));
        }
    }
}
