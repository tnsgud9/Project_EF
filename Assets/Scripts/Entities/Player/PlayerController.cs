using Collections;
using Commons;
using Entities.Abilities;
using Managers;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : BaseBehaviour, IController
    {
        public AudioPreset deathAudio;
        [Inject] public PlayerInputHandler inputHandler;
        [InjectChild] public Animator animator;
        [InjectAdd] private AudioSource _audioSource;
        [Inject] private PlayerMovement _movement;

        private PlayerAliveState _playerAliveState;
        [Inject] private PlayerAttack _playerAttack;
        private PlayerDeathState _playerDeathState;
        private StateContext<PlayerController> _stateContext;

        private void Start()
        {
            // Exta Component Init
            AudioSystem = new AudioSystem(_audioSource);

            // Input 이벤트 구독
            inputHandler.OnMoveEnter += HandleMoveEnter;
            inputHandler.OnMoveStay += HandleMoveStay;
            inputHandler.OnMoveExit += HandleMoveExit;

            inputHandler.OnAttackEnter += HandleAttackEnter;
            inputHandler.OnAttackStay += HandleAttackStay;
            inputHandler.OnAttackExit += HandleAttackExit;

            // EventBinding
            Health.OnDie += () => _stateContext.CurrentState = _playerDeathState;
            Health.OnHealthChanged += (currentHealth, damage) => { };
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            // 기본 State 설정
            _stateContext = new StateContext<PlayerController>(this);
            _playerAliveState = new PlayerAliveState();
            _playerDeathState = new PlayerDeathState(deathAudio);
            _stateContext.CurrentState = _playerAliveState;

            GameManager.Instance.playerController = this; // TODO: Controller는 Manager를 직접 지정할 수 없음 수정 필요
        }

        [Inject] public IHealth Health { get; set; }

        public void KnockBack(float knockBackForce = 1f, float timeDelay = 0.6f)
        {
            _movement.KnockBack(knockBackForce, timeDelay);
            _playerAttack.enabled = false;
            StartCoroutine(Logic.WaitThenCallback(timeDelay, () => _playerAttack.enabled = true));
        }

        public void AddAbility(AbilityData abilityData,
            Enums.AbilityMethodType abilityMethodType = Enums.AbilityMethodType.Add)
        {
            foreach (var ability in GetComponentsInChildren<IAbility>()) ability.AddEffect(abilityData);
        }

        public IAudioSystem AudioSystem { get; set; }

        private void HandleMoveEnter()
        {
        }

        private void HandleMoveStay()
        {
            _movement.Move(inputHandler.movementInput);
        }

        private void HandleMoveExit()
        {
            _movement.Move(Vector2.zero);
        }

        private void HandleAttackEnter()
        {
            _playerAttack.Attack();
        }

        private void HandleAttackStay()
        {
        }

        private void HandleAttackExit()
        {
        }
    }
}