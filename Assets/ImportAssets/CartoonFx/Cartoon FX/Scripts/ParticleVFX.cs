using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleVFX : PooledObject {
	[SerializeField] private ParticleSystem particleSystem;

	private void Awake() {
		if(particleSystem == null)
			particleSystem = GetComponent<ParticleSystem>();
	}

    protected override void OnEnablePooledObject() {
		StartCoroutine(CheckIfAlive());
	}
	IEnumerator CheckIfAlive() {
		while (particleSystem) {
			yield return new WaitForSeconds(0.5f);
			if (!particleSystem.IsAlive(true)) {
				ReturnToPool();
			}
		}
	}
}
