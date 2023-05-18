using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster", order = 51)]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;

        [Range(1, 100)]
        public int Hp;
        [Range(1f, 30f)]
        public float Damage;

        public int MaxLoot;
        public int MinLoot;

        [FormerlySerializedAs("EffectiveDistance")]
        [Range(0.5f, 1f)]
        public float AttackEffectiveDistance = 0.5f;
        [Range(0.5f, 1f)]
        public float AttackCleavage;
        [Range(1f, 10f)]
        public float MoveSpeed = 3f;
        [Range(1f, 10f)]
        public float AttackCooldown = 3f;

        public GameObject Prefab;
    }
}