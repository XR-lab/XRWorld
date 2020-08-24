using UnityEngine;

namespace XRWorld.Core.Cameras
{
    public class CamData
    {
        public string sesionId;
        public string name;
        public Vector3Int pos, rot;
        public CamData(string sesionId, string name, Vector3 pos, Vector3 rot)
        {
            this.sesionId = sesionId;
            this.name = name;
            SetPosRot(pos, rot);
        }
        public void SetPosRot(Vector3 pos, Vector3 rot)
        {
            this.pos = new Vector3Int(Mathf.FloorToInt(pos.x * 100),
                Mathf.FloorToInt(pos.y * 100),
                Mathf.FloorToInt(pos.z * 100));
            this.rot = new Vector3Int((Mathf.FloorToInt(rot.x * 1000)) / 1000,
                (Mathf.FloorToInt(rot.y * 1000)) / 1000,
                (Mathf.FloorToInt(rot.z * 1000)) / 1000);
        }
    }
}


