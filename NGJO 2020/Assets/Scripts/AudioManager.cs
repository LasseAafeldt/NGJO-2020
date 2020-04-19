using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	public static AudioManager singleton;
	AudioSource backgroundSource;

	private void Awake() {
		if (singleton == null) {
			singleton = this;
		} else if (singleton != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		backgroundSource = GetComponent<AudioSource>();
	}

	public void PlayBackground(AudioClip clip) {
		if (clip != null && clip != singleton.backgroundSource.clip) {
			singleton.backgroundSource.Stop();
			singleton.backgroundSource.clip = clip;
			singleton.backgroundSource.Play();
		}
	}
}
