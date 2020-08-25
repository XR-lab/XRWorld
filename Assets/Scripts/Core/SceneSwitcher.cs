using UnityEngine;
using UnityEngine.SceneManagement;

namespace XRWorld.Core
{
    public class SceneSwitcher : MonoBehaviour
    {
        public void SwitchScenes(string _sceneToSwitchToo)
        {
            SceneManager.LoadScene(_sceneToSwitchToo);
        }
    }
}