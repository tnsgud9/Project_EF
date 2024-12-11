using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    [CreateAssetMenu(fileName = "StageData", menuName = "StageData")]
    public class StageData : ScriptableObject
    {
        public string titleName;
        public AudioPreset bgAudioPreset;
        public GameObject mapPrefab;
        [SerializeField] public List<EnemeyInfo> enemies;

        // Editor에서 Gizmo를 그리기 위해 OnDrawGizmos()를 추가합니다.
        private void OnDrawGizmos()
        {
            // 적들의 위치에 원을 그립니다.
            foreach (var enemyInfo in enemies)
            {
                Gizmos.color = Color.red; // 원의 색상 설정
                Gizmos.DrawSphere(enemyInfo.position, 0.5f); // 위치에 반지름 0.5의 구체를 그립니다.
            }
        }
    }

    [Serializable]
    public class EnemeyInfo
    {
        public GameObject enemyPrefab;
        public Vector3 position;
    }
}