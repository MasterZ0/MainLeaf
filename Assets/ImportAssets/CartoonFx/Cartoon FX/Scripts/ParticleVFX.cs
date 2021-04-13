using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleVFX : PooledObject {
	public bool OnlyDeactivate;
	ParticleSystem ps;

	private void Start() {
		ps = GetComponent<ParticleSystem>();
	}

    protected override void StartObject() {
		StartCoroutine(CheckIfAlive());
	}
	IEnumerator CheckIfAlive ()	{
		
		while(ps != null)
		{
			yield return new WaitForSeconds(0.5f);
			if(!ps.IsAlive(true))
			{
				if (OnlyDeactivate) {
					gameObject.SetActive(false);
				}
				else
					ReturnToPool();
				break;
			}
		}
	}
}
