using UnityEngine;
using System.Collections;

public class Inkubator_State : MonoBehaviour {

    public bool isActive;

	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            setInactive();
        }
        else
        {
            setActive();
        }
            
    }

    void setInactive()
    {
        int children = transform.childCount;
            for (int i = 0; i < children; ++i)
            {
               
                GameObject tempObject = transform.GetChild(i).gameObject;
                if (!tempObject.name.Equals("Sockel")
                    && !tempObject.name.Equals("Sturz")
                    && !tempObject.name.Equals("Inkubator_light"))
                {
                    
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                if (tempObject.name.Equals("Inkubator_light"))
                {
                    tempObject.light.intensity = 1;

                }
            }     
        }

    void setActive()
    {
        int children = transform.childCount;
            for (int i = 0; i < children; ++i)
            {
               
                GameObject tempObject = transform.GetChild(i).gameObject;
                if (!tempObject.name.Equals("Sockel")
                    && !tempObject.name.Equals("Sturz")
                    && !tempObject.name.Equals("Inkubator_light"))
                {
                    Debug.Log(transform.GetChild(i).gameObject.name);
                    transform.GetChild(i).gameObject.SetActive (true);
                }
                if (tempObject.name.Equals("Inkubator_light"))
                {
                    tempObject.light.intensity = 3;

                }
            }     
        }
    }

