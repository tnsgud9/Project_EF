using System.Collections.Generic;
using System.Linq;
using Camera;
using Collections;
using Commons;
using DG.Tweening;
using Entities;
using Stage;
using UI;
using UnityEngine;

namespace Managers
{
    public class StageManager : Singleton<StageManager>
    {
        public List<StageData> stages;
        public GameObject currentMap;
        public List<GameObject> currentEnemies;
        public AudioPreset bossAppearAudioPreset;
        public bool allEnemyTracking;
        private List<StageData>.Enumerator _stageIter;
        public AudioSystem audioSystem;

        protected override void Awake()
        {
            base.Awake();
            audioSystem = new AudioSystem(GetComponent<AudioSource>());
            _stageIter = stages.GetEnumerator();
            _stageIter.MoveNext();
            ChangeStage(_stageIter.Current);
            EventBus<Enums.Event>.Subscribe(Enums.Event.GameOver, () => { audioSystem.Stop(); });
        }

        public void Update()
        {
            if (allEnemyTracking && currentEnemies.Sum(it => it.GetComponent<Health>().CurrentHealth) <= 0)
            {
                allEnemyTracking = false;
                EventBus<Enums.Event>.Publish(Enums.Event.StageClear);
                UiManager.Instance.EnableUI(typeof(UIGameClear));
            }
        }

        public void ChangeStage(StageData stageData)
        {
            // 1. 이전 레벨 삭제
            Destroy(GameObject.FindGameObjectWithTag("Map"));
            foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy")) Destroy(enemyObj);

            // 2. 신규 스테이지 생성 및 보스 생성
            currentMap = Instantiate(stageData.mapPrefab);
            UnityEngine.Camera.main.GetComponent<CameraFollow>().spriteRenderer =
                currentMap.GetComponent<SpriteRenderer>();

            currentEnemies = stageData.enemies.Select(enemy =>
                Instantiate(enemy.enemyPrefab, enemy.position, Quaternion.identity)).ToList();
            allEnemyTracking = true;

            // 생성된 유닛 통제
            EventBus<Enums.Event>.Publish(Enums.Event.StageReady);

            // 보스 타이틀 출력
            audioSystem.Play(bossAppearAudioPreset);
            var bossTitleUI = UiManager.Instance.GetUI<UIStageTitle>();
            bossTitleUI.SetTitleText(stageData.titleName);

            bossTitleUI.rectTransform.anchoredPosition = Vector2.left * 1175f;
            var sequence = DOTween.Sequence();
            // 목표 위치로 이동
            sequence.Append(bossTitleUI.rectTransform.DOAnchorPos(Vector3.zero, 0.5f).SetEase(Ease.InOutSine));
            // 대기 시간
            sequence.AppendInterval(2f);
            // 원래 위치로 돌아가기
            sequence.Append(bossTitleUI.rectTransform.DOAnchorPos(Vector2.right * 1175f, 0.5f).SetEase(Ease.InOutSine)
                .OnKill(
                    () =>
                    {
                        audioSystem.Play(stageData.bgAudioPreset);
                        EventBus<Enums.Event>.Publish(Enums.Event.StageStart); // 게임 시작
                    }));
        }

        public void NextStage()
        {
            _stageIter.MoveNext();
            if (_stageIter.Current != null)
                ChangeStage(_stageIter.Current);
            else
                Debug.LogWarning("No stage found or reached end");
        }
    }
}