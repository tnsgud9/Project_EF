using Collections;
using Entities.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace Entities.Player
{
    public interface IPlayerAttack
    {
        void Attack();
    }

    public class PlayerAttack : BaseBehaviour, IPlayerAttack
    {
        [Header("Bomb Placement Settings")] public GameObject bombPrefab; // 단일 폭탄 프리팹

        public int maxBombs = 2; // 최대 설치 가능 폭탄 수
        public float plantDelay = 0.5f; // 설치 중 움직임 제한 시간
        private int _activeBombCount; // 현재 설치된 폭탄 개수

        private ObjectPool<GameObject> _bombPool;
        private bool _isPlanting;

        private void Start()
        {
            // 폭탄 풀 생성
            _bombPool = new ObjectPool<GameObject>(
                CreateBomb,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                false,
                maxBombs,
                maxBombs
            );
        }

        private GameObject CreateBomb()
        {
            var bomb = Instantiate(bombPrefab);
            return bomb;
        }

        private void OnTakeFromPool(GameObject bomb)
        {
            bomb.SetActive(true);
        }

        private void OnReturnedToPool(GameObject bomb)
        {
            bomb.SetActive(false);
        }

        private void OnDestroyPoolObject(GameObject bomb)
        {
            Destroy(bomb);
        }

        public void PlantBomb()
        {
            if (_isPlanting) return;
            // 폭탄 설치 제한 조건
            if (_activeBombCount >= maxBombs) return;

            _isPlanting = true;

            // 플레이어 움직임 제한
            var movement = GetComponent<IPlayerMovement>();
            StartCoroutine(movement.DelayMovement(plantDelay));
            // 설치 지연 후 폭탄 배치
            Invoke(nameof(DeployBomb), plantDelay);
        }

        private void DeployBomb()
        {
            var bombObj = _bombPool.Get();
            bombObj.transform.position = transform.position;

            // 폭탄 초기화
            var bomb = bombObj.GetComponent<Bomb>();
            bomb.Initialize(this);

            _activeBombCount++;
            _isPlanting = false; // 설치 상태 해제
        }

        public void OnBombExploded(GameObject bomb)
        {
            // 폭탄 폭발 후 풀에 반환
            _bombPool.Release(bomb);
            _activeBombCount--;
        } // ReSharper disable Unity.PerformanceAnalysis
        public void Attack()
        {
            PlantBomb();
        }
    }
}