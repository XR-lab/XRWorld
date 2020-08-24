using UnityEngine;
using UnityEngine.UI;

namespace XRWorld.Users
{
    public class NickNameSetter : MonoBehaviour
    {
        [SerializeField] private string _nickName;

        public string GetNickName()
        {
            return _nickName;
        }
        
        public void SetNickname(Text txt)
        {
            _nickName = txt.text;
        }
    }
}

