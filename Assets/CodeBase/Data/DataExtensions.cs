using CodeBase.Data;
using UnityEngine;

namespace Assets.CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVector3Data(this Vector3 vector) =>
            new Vector3Data(vector.x, vector.y, vector.z);
    }
}