using Collections;
using Commons;
using Entities.Abilities;
using Managers;
using UI;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : BaseBehaviour, IController
    {
        [InjectChild] public Animator animator;
        public AudioPreset deathAudio;
        public AudioPreset hitAudio;
        [HideInInspector] [Inject] public PlayerInputHandler inputHandler;
        [HideInInspector] [Inject] public PlayerMovement movement;
        [HideInInspector] [Inject] public PlayerAttack playerAttack;

        [Inject] public Health health;

        [InjectAdd] private AudioSource _audioSource;
        private PlayerAliveState _playerAliveState;
        private PlayerDeathState _playerDeathState;
        private PlayerReadyState _playerReadyState;
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
            health.OnDie += () => _stateContext.CurrentState = _playerDeathState;
            health.OnHealthChanged += (currentHealth, damage) =>
            {
                AudioSystem.PlayOneShot(hitAudio);
                UiManager.Instance.GetUI<UIPlayerInfo>()?.SetPlayerHealth(currentHealth);
            };
            UiManager.Instance.GetUI<UIPlayerInfo>()?.SetPlayerHealth(health.CurrentHealth);
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageReady,
                () => { _stateContext.CurrentState = _playerReadyState; });
            EventBus<Enums.Event>.Subscribe(Enums.Event.StageStart,
                () => { _stateContext.CurrentState = _playerAliveState; });
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            // 기본 State 설정
            _stateContext = new StateContext<PlayerController>(this);
            _playerAliveState = new PlayerAliveState();
            _playerDeathState = new PlayerDeathState(deathAudio);
            _playerReadyState = new PlayerReadyState();
            _stateContext.CurrentState = _playerReadyState;

            GameManager.Instance.playerController = this; // TODO: Controller는 Manager를 직접 지정할 수 없음 수정 필요
            UiManager.Instance.GetUI<UIPlayerInfo>()?.SetPlayerHealth(health.CurrentHealth);
        }

        public void KnockBack(float knockBackForce = 1f, float timeDelay = 0.6f)
        {
            movement.KnockBack(knockBackForce, timeDelay);
            playerAttack.enabled = false;
            StartCoroutine(Logic.WaitThenCallback(timeDelay, () => playerAttack.enabled = true));
        }

        public void AddAbility(AbilityData abilityData,
            Enums.AbilityMethodType abilityMethodType = Enums.AbilityMethodType.Add)
        {
            foreach (var ability in GetComponentsInChildren<IAbility>()) ability.AddEffect(abilityData);
            UiManager.Instance.GetUI<UIPlayerInfo>().SetPlayerBomb(playerAttack.maxBombs);
            UiManager.Instance.GetUI<UIPlayerInfo>().SetPlayerHealth(health.CurrentHealth);
        }

        public IAudioSystem AudioSystem { get; set; }

        private void HandleMoveEnter()
        {
        }

        private void HandleMoveStay()
        {
            movement.Move(inputHandler.movementInput);
        }

        private void HandleMoveExit()
        {
            movement.Move(Vector2.zero);
        }

        private void HandleAttackEnter()
        {
            playerAttack.Attack();
        }

        private void HandleAttackStay()
        {
        }

        private void HandleAttackExit()
        {
        }
    }
}