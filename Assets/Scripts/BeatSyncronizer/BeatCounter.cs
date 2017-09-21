using UnityEngine;
using System.Collections;
using SynchronizerData;

/// This class is responsible for counting and notifying its observers when a beat occurs, specified by beatValue.
/// An offset beat value will shift the beat. A negative offset shifts to the left (behind the beat).
/// The accuracy of the beat counter is handled by loopTime, which controls how often it checks whether a beat has happened.
/// Higher settings for loopTime decreases load on the CPU, but will result in less accurate beat synchronization.

public class BeatCounter : MonoBehaviour {
	
	public BeatValue beatValue = BeatValue.QuarterBeat;
	public int beatScalar = 1;
	public BeatValue beatOffset = BeatValue.None;
	public bool negativeBeatOffset = false;
	public BeatType beatType = BeatType.OnBeat;
	public float loopTime = 30f;
	public AudioSource audioSource;
	public GameObject[] observers;
	
	private float nextBeatSample;
	private float samplePeriod;
	private float sampleOffset;
	private float currentSample;

	
	void Awake ()
	{
		// Calculate number of samples between each beat.
		float audioBpm = audioSource.GetComponent<BeatSynchronizer>().bpm;
		samplePeriod = (60f / (audioBpm * BeatDecimalValues.values[(int)beatValue])) * audioSource.clip.frequency;

		if (beatOffset != BeatValue.None) {
			sampleOffset = (60f / (audioBpm * BeatDecimalValues.values[(int)beatOffset])) * audioSource.clip.frequency;
			if (negativeBeatOffset) {
				sampleOffset = samplePeriod - sampleOffset;
			}
		}

		samplePeriod *= beatScalar;
		sampleOffset *= beatScalar;
		nextBeatSample = 0f;
	}

	/// Initializes and starts the coroutine that checks for beat occurrences. The nextBeatSample field is initialized to 
	/// exactly match up with the sample that corresponds to the time the audioSource clip started playing (via PlayScheduled).
    /// Equal to the audio system's DSP time plus the specified delay time.
	void StartBeatCheck (double syncTime)
	{
		nextBeatSample = (float)syncTime * audioSource.clip.frequency;
		StartCoroutine(BeatCheck());
	}
	
	/// Subscribe the BeatCheck() coroutine to the beat synchronizer's event.
	void OnEnable ()
	{
		BeatSynchronizer.OnAudioStart += StartBeatCheck;
	}

	/// Unsubscribe the BeatCheck() coroutine from the beat synchronizer's event.
	/// 
	/// This should NOT (and does not) call StopCoroutine. It simply removes the function that was added to the
	/// event delegate in OnEnable().
	void OnDisable ()
	{
		BeatSynchronizer.OnAudioStart -= StartBeatCheck;
	}

    /// This method checks if a beat has occurred in the audio by comparing the current sample position of
    /// the audio system's DSP time to the next expected sample value of the beat.
    /// The frequency of the checks is controlled by the loopTime field.
    ///
    /// The WaitForSeconds() yield statement places the execution of the coroutine right after the Update() call, so by 
    /// setting the loopTime to 0, this method will update as frequently as Update(). If even greater accuracy is 
    /// required, WaitForSeconds() can be replaced by WaitForFixedUpdate(), which will place this coroutine's execution
    /// right after FixedUpdate().
    IEnumerator BeatCheck ()
	{
		while (audioSource.isPlaying) {
			currentSample = (float)AudioSettings.dspTime * audioSource.clip.frequency;
			
			if (currentSample >= (nextBeatSample + sampleOffset)) {
				foreach (GameObject obj in observers) {
					obj.GetComponent<BeatObserver>().BeatNotify(beatType);
				}
				nextBeatSample += samplePeriod;
			}

			yield return new WaitForSeconds(loopTime / 1000f);
		}
	}

}
