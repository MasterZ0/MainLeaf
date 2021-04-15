using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleVFX : PooledObject {
	ParticleSystem ps;

	private void Start() {
		ps = GetComponent<ParticleSystem>();
	}

    protected override void OnEnablePooledObject() {
		StartCoroutine(CheckIfAlive());
	}
	IEnumerator CheckIfAlive ()	{
		
		while(ps != null)
		{
			yield return new WaitForSeconds(0.5f);
			if(!ps.IsAlive(true)) {
				DesactivePooledObject();
				break;
			}
		}
	}
}
