using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour {
    [SerializeField] PlayerStats playerStats;
    public PlayerStats PlayerStats {
        get { return playerStats; }
    }
    [SerializeField] Collector collectorPrefab;
    [SerializeField] GameObject thrusterPrefab;
    [SerializeField] GameObject destroyParticles;
    [SerializeField] AudioClip destroySound;

    AudioSource m_audio;
    SpriteRenderer m_sprite;
    PlayerInput playerInput;
    public PlayerInput Input { get { return playerInput; } }
    Collector collector;
    public Collector Collector { get { return collector; } }
    GameObject thruster;

    void Awake() 
    {
        m_audio = GetComponent<AudioSource>();
        m_sprite = GetComponentInChildren<SpriteRenderer>();

        collector = Instantiate( collectorPrefab, transform.position, transform.rotation, m_sprite.transform );
        thruster = (GameObject)Instantiate( thrusterPrefab, transform.position + new Vector3(0f, -.9f, 0f), thrusterPrefab.transform.rotation, m_sprite.transform );

        playerInput = GetComponent<PlayerInput>();
    }

    void Update() 
    {
        playerInput.HandleMovementInput();
        playerInput.HandleCollectorInput();
    }

    public void Destroy() 
    {
        transform.GetChild( 0 ).gameObject.SetActive( false );
        m_audio.PlayOneShot( destroySound );
        Instantiate( destroyParticles, transform.position, transform.rotation );
        Destroy( this.gameObject, 2f );
    }
}
