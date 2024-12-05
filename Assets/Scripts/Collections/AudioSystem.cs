using UnityEngine;

namespace Collections
{
    public interface IAudioSystem
    {
        public void PlayOneShot(AudioPreset preset);
        public void Stop();
        public void Play(AudioPreset preset, ulong delay = 0);
    }


    public class AudioSystem : IAudioSystem
    {
        private readonly AudioSource _source;
        // private Dictionary<string, AudioSource> _oneShotAudioSources = new();

        public AudioSystem(AudioSource source)
        {
            _source = source;
        }

        public void Play(AudioPreset preset, ulong delay = 0)
        {
            _source.clip = preset.audioClip;
            _source.volume = preset.volume;
            _source.pitch = preset.pitch;
            _source.loop = preset.loop;
            _source.Play(delay);

            // NOTE: PlayDelayed가 정확한 타이밍에 재생되지 않는 이슈가 이전 유니티 버전에 존재, 딜레이의 오차가 발생시, 엔진코드 확인 필요
        }

        public void Stop()
        {
            _source.Stop();
        }

        public void PlayOneShot(AudioPreset preset)
        {
            // TODO: PlayOneShot은 현재 AuidoSource에 영향을 받기에 개별적으로 프리셋까지 적용 및 재생하는 기능이 필요.
            // _source.clip = preset.audioClip;
            // _source.volume = preset.volume;
            // _source.pitch = preset.pitch;
            // _source.loop = preset.loop;
            _source.PlayOneShot(preset.audioClip, preset.volume);
        }
    }
}