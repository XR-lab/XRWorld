using UnityEngine;
using UnityEngine.UI;

namespace XRWorld.Cam
{
    public class NickNamePointer : MonoBehaviour
    {
        public void SetNickname(string nick)
        {
            GetComponent<Text>().text = nick;
        }
    
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}

