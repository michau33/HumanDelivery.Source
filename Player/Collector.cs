using System.Collections;
using UnityEngine;

public class Collector : MonoBehaviour {

#region Variables
    [SerializeField] float cooldownTime = 5f;
    [SerializeField] AudioClip collectSound; 

    AudioSource m_audio;
    ParticleSystem m_particles;
    GameObject objToCollect;
    bool isObjCollected;
    bool isCollecting;
    bool isOnCooldown;
    public bool IsCollecting { get { return isCollecting; } private set {} }
    public bool IsOnCooldown { get { return isOnCooldown; } set { isOnCooldown = value; } }
#endregion

    void Awake() {
        m_particles = GetComponent<ParticleSystem>();
        m_audio = GetComponent<AudioSource>();

        isCollecting = false;
        isOnCooldown = false;
        isObjCollected = false;
        objToCollect = null;
    }

    void Update () {
        //print( "Object to collect: " + objToCollect );
        //print( "Is object collected ?" + isObjCollected );
        HandleCollecting();
	}

    void HandleCollecting() {
        // if you using collector and entity got hit by m_particles and the same entity is not collected do following
        if( this.isCollecting && this.objToCollect && !this.isObjCollected ) {
            // tell Prisoner ( for now ) to change it's gravity to 0 and stop any movement
            Component collectable = this.objToCollect.GetComponent (typeof(ICollectable));
		    if (collectable != null) {
			    (collectable as ICollectable).OnCollectBegin ();
		    }

            // normalized direction from drainer center target
            Vector2 dir = ( this.transform.position - this.objToCollect.transform.position ).normalized;
            // distance between drainer and target
            float distance = Vector2.Distance( this.objToCollect.transform.position, this.transform.position );

            if( distance >= 0.1f ) {
                // if distance is not less than small amount translate target's position every frame
                this.objToCollect.transform.Translate( dir * 5f * Time.deltaTime );
            } else {
                // when distance is less than the chosen amount that means target is collected;
                this.objToCollect.SendMessage("Destroy");
                this.isObjCollected = true;
                
                StartCoroutine( Cooldown() );
            }
        }

        // when you stop collecting but you still have target ( stop pressing button )
        if( !this.isCollecting && this.objToCollect ) {
            // tell Prisoner ( for now ) to change it's gravity to 1;
            Component collectable = this.objToCollect.GetComponent (typeof(ICollectable));
		    if (collectable != null) {
			    (collectable as ICollectable).OnCollectEnd ();
		    }
            // set current targe to null
            this.objToCollect = null;
        }
    }

    IEnumerator Cooldown() {
        // TODO Use EventManager here
        //EventManager.instance.PostNotification(this, "OnCooldown");
        isOnCooldown = true;
        yield return new WaitForSeconds( cooldownTime );
        isOnCooldown = false;
    }

    public void StartCollecting() { 
        m_particles.Play(); 
        m_audio.PlayOneShot( collectSound ); 
        isCollecting = true; 
    }

    public void StopCollecting() { 
        m_particles.Stop(); 
        isCollecting = false; 
    }

    // Check if m_particles collided with enemy tagged "Entity" and if so drain an enemy
    void OnParticleCollision( GameObject other ) {
        if( other.tag == "Entity") {
            if( isCollecting && objToCollect == null ) {
                objToCollect = other.gameObject;
            }
        }
    }
}
