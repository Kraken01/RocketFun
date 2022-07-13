using UnityEngine; //monobehaviour class present in unity engine namespace


public class Movement : MonoBehaviour //inheriting monobehaviour class
{

    [SerializeField] float playerThrust = 1f;
    [SerializeField] float playerRotation = 1f;
    [SerializeField] AudioClip engineThrust;


    Rigidbody rigidbody; //rigidbody type variable
    AudioSource audioSource;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>(); //catching reference of rigid body
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Playerthrust();
        Playerrotation();
    }

    void Playerthrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * playerThrust * Time.deltaTime);//vector3.up is a short hand for(0, 1, 0) 
            //relative force is used to add force related to the cordinates of the object
           if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineThrust);
            }
        }
        else
        {
            audioSource.Stop();
        }
        
    }
    void Playerrotation()
    {

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(playerRotation);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-playerRotation);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rigidbody.freezeRotation = true; //freezing rotation so we can manually control rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);//vector3.forward(0, 0, 1)
        rigidbody.freezeRotation = false; //unfreezing rotation so physics can take over
    }
}
