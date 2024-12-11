using Collections;
using Entities.Abilities;
using Entities.Weapons;
using Managers;
using UI;
using UnityEngine;
using UnityEngine.Pool;

namespace Entities.Player
{
    public interface IPlayerAttack
    {
        void Attack();
    }

    public class PlayerAttack : BaseBehaviour, IPlayerAttack, IAbility
    {
        private static readonly int Planting = Animator.StringToHash("Planting");
        [Header("Bomb Placement Settings")] public GameObject bombPrefab; // 단일 폭탄 프리팹

        public int maxBombs = 2; // 최대 설치 가능 폭탄 수
        public int damage = 2; // 공격 데미지
        public float bombRadius = 2f; // 폭발 범위
        public float cirtical = 0.01f; // 치명타율
        public float plantDelay = 0.5f; // 설치 중 움직임 제한 시간
        private int _activeBombCount; // 현재 설치된 폭탄 개수
        [InjectChild] private Animator _animator;

        private ObjectPool<GameObject> _bombPool;
        private bool _isPlanting;

        private void Start()
        {
            // 폭탄 풀 생성
            _bombPool = new ObjectPool<GameObject>(
                CreateBomb,
                GetBomb,
                ReleaseBomb,
                DestroyBomb,
                false,
                maxBombs + 1,
                maxBombs * 2
            );
            UiManager.Instance.GetUI<UIPlayerInfo>()?.SetPlayerBomb(maxBombs);
        }

        private GameObject CreateBomb()
        {
            var bomb = Instantiate(bombPrefab);
            return bomb;
        }

        private void GetBomb(GameObject bomb)
        {
            bomb.SetActive(true);
        }

        private void ReleaseBomb(GameObject bomb)
        {
            bomb.SetActive(false);
        }

        private void DestroyBomb(GameObject bomb)
        {
            Destroy(bomb);
        }

        public void BombExplotion()
        {
            _activeBombCount--;
            UiManager.Instance.GetUI<UIPlayerInfo>()?.SetPlayerBomb(maxBombs - _activeBombCount);
        }

        public void BombExploded(GameObject bomb)
        {
            // 폭탄 폭발 후 풀에 반환
            _bombPool.Release(bomb);
        } // ReSharper disable Unity.PerformanceAnalysis

        public void PlantBomb()
        {
            // 폭탄 설치 제한 조건
            if (_isPlanting) return;
            if (_activeBombCount >= maxBombs) return;


            _isPlanting = true;
            _animator.SetBool(Planting, _isPlanting);

            // 플레이어 움직임 제한
            var movement = GetComponent<IMovement>();
            StartCoroutine(movement?.DelayMovement(plantDelay));

            // 설치 지연 후 폭탄 배치
            Invoke(nameof(DeployBomb), plantDelay); // 비동기로 처리 
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
            _animator.SetBool(Planting, _isPlanting);
            UiManager.Instance.GetUI<UIPlayerInfo>()?.SetPlayerBomb(maxBombs - _activeBombCount);
        }

        public void Attack()
        {
            PlantBomb();
        }

        public void AddEffect(AbilityData abilityData)
        {
            maxBombs += abilityData.bombPlant;
            bombRadius += abilityData.bombRadius;
            plantDelay = Mathf.Clamp(plantDelay - abilityData.bombSetupTime, 0.01f, int.MaxValue);
            damage += abilityData.bombDamage;
            // cirtical += abilityData.bombCritical;
        }
    }
}