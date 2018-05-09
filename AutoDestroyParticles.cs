using System.Collections;
using UnityEngine;

public class AutoDestroyParticles : MonoBehaviour {

	ParticleSystem particles;

	void Awake() { particles = GetComponent<ParticleSystem>(); }

	void Update () {
		if( particles )
			if( !particles.IsAlive() )
				Destroy( this.gameObject );
	}
}
