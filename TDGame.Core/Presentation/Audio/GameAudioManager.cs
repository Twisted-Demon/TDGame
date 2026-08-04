using Dreambit;
using Dreambit.ECS;
using Dreambit.ECS.Audio;

namespace TDGame.Core;

public class GameAudioManager
    : SingletonComponent<GameAudioManager>
{
    private SoundEffectEmitter _emitter;
    
    private SoundCue _gameAmbienceCue;
    private SoundCue _backgroundMusicCue;

    public override void OnCreated()
    {
        _emitter = Entity.AttachComponent<SoundEffectEmitter>();
        _emitter.MasterVolume = 0.15f;
        
        _gameAmbienceCue =
            Resources.LoadAsset<SoundCue>(
                "audio/ambience/game-ambience.sound-cue");
        
        _backgroundMusicCue =
            Resources.LoadAsset<SoundCue>(
                "audio/music/background-music.sound-cue");

        //
        _emitter.Play(_backgroundMusicCue);
        _emitter.Play(_gameAmbienceCue);
    }
    
    public void Play(SoundCue cue) => _emitter.Play(cue);
}
