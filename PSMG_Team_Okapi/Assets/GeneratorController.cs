using UnityEngine;
using System.Collections;

public class GeneratorController : MonoBehaviour {

    public GameObject associatedSwitch;
    public bool isOn;
    public float speed;
    public float lightBrightness;
    public float lightReach;
    public float flareBrightness;
    public float particleSpeed;
    public float animationSpeed;

    //private Animation animation;
    private ParticleSystem particleSys;
    private GameObject light;
    private LensFlare flare;
    


    // Use this for initialization
    void Start()
    {
        animation["GeneratorAnimation"].speed = 0;
        animation.wrapMode = WrapMode.Loop;

        Transform mychildtransform = transform.FindChild("Point light");
        light = mychildtransform.gameObject;

        flare = light.GetComponent<LensFlare>();

        particleSys = transform.FindChild("Particle System").gameObject.particleSystem;
       
        
        if (associatedSwitch != null)
        {
            associatedSwitch.GetComponent<Generator_Switch_Controller>().OnActivateGeneratorSwitch += OnActivateSwitch;
        }
    }

    private void OnActivateSwitch(GameObject generator)
    {  
        isOn = !isOn;
        Debug.Log("Generator: " + isOn);        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            animation["GeneratorAnimation"].speed = Mathf.Lerp(animation["GeneratorAnimation"].speed, animationSpeed, Time.deltaTime * speed);
            //Debug.Log(animation["Default Take"].speed);

            light.light.intensity = Mathf.Lerp(light.light.intensity, lightBrightness, Time.deltaTime * speed);
            light.light.range = Mathf.Lerp(light.light.range, lightReach, Time.deltaTime * speed);
            flare.brightness = Mathf.Lerp(flare.brightness, flareBrightness, Time.deltaTime * speed);
            if(!particleSys.isPlaying)
            {
                particleSys.Play();
            }
            particleSys.playbackSpeed = Mathf.Lerp(particleSys.playbackSpeed, particleSpeed, Time.deltaTime * speed);
            if(!audio.isPlaying)
            {
                audio.Play();
            }
        }else
        {
            animation["GeneratorAnimation"].speed = 0;
            light.light.intensity = 0;
            light.light.range = 0;
            flare.brightness = 0;
            particleSys.playbackSpeed = 0;
            
        }
    }
}
