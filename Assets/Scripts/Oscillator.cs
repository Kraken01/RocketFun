using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    
    float movementFactor;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
        float cycle = Time.time / period; // how many cycle is completed in a timestamp
                                         //  (eg 10 sec has elapsed and period is 2 then cycle is 5)
        float tau = Mathf.PI * 2; // tau is radians (angle) in a full circle which is a constant value of 6.283(2 * PI)

        float rawSineWave = Mathf.Sin(cycle * tau); // oscillating from -1 to +1 using sin

        movementFactor = (rawSineWave + 1f) / 2f; // redefining sin for 0 to +1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
