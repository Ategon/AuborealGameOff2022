using UnityEngine;

namespace Utilities.Startup
{
    public static class StartupController
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize() => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("StartupSystems")));
    }
}