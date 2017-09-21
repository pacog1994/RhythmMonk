using UnityEngine;
using System.Collections;

/// This class should be attached to the audio source for which synchronization should occur, and is 
/// responsible for syncing up the beginning of the audio clip with all active beat counters and pattern counters.

[RequireComponent(typeof(AudioSource))]
public class BeatSynchronizer : MonoBehaviour {

	public float bpm;       		// Tempo in beats per minute of the audio clip.
	public float startDelay;	    // Number of seconds to delay the start of audio playback.
	public delegate void AudioStartAction(double syncTime);
	public static event AudioStartAction OnAudioStart;
	
	
	void Start ()
	{
		double initTime = AudioSettings.dspTime;
		GetComponent<AudioSource>().PlayScheduled(initTime + startDelay);
		if (OnAudioStart != null) {
			OnAudioStart(initTime + startDelay);
		}
	}

}
