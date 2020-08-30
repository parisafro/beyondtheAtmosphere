using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class warpspeed : MonoBehaviour
{
    public VisualEffect warpSpeedVFX;
    private bool warpActive;
    public float rate = 0.02f;
    public MeshRenderer cylender;
    public float delay= 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        warpSpeedVFX.Play();
        warpSpeedVFX.SetFloat("warp-amount", 0);
        cylender.material.SetFloat("Active_", 0);

    }

    // Update is called once per frame
    void Update()
    {

        IEnumerable ActivateParticles()
        {
            if (warpActive)
            {
                warpSpeedVFX.Play();
                float amount = warpSpeedVFX.GetFloat("warp-amount");
                while(amount<1 & warpActive)
                {
                    amount =+ rate;
                    warpSpeedVFX.SetFloat("warp-amount", amount);
                    yield return new WaitForSeconds(0.1f);
                }

            }
            else
            {
                float amount = warpSpeedVFX.GetFloat("warp-amount");
                while (amount > 0 & !warpActive)
                {
                    amount =- rate;
                    warpSpeedVFX.SetFloat("warp-amount", amount);
                    yield return new WaitForSeconds(0.1f);

                    if(amount <= 0 + rate)
                    {
                        amount = 0;
                        warpSpeedVFX.SetFloat("warp-amount", amount);
                        warpSpeedVFX.Stop();
                    }
                }
            }
        }

        IEnumerable ActivateShader()
        {
            if (warpActive)
            {
                yield return new WaitForSeconds(delay);
                float amount = cylender.material.GetFloat("Active_");
                while (amount < 1 & warpActive)
                {
                    amount = +rate;
                    cylender.material.SetFloat("Active_", amount);
                    yield return new WaitForSeconds(0.1f);
                }

            }
            else
            {
                
                float amount = cylender.material.GetFloat("Active_");
                while (amount > 0 & !warpActive)
                {
                    amount = -rate;
                    cylender.material.SetFloat("Active_", amount);
                    yield return new WaitForSeconds(0.1f);

                    if (amount <= 0 + rate)
                    {
                        amount = 0;
                        cylender.material.SetFloat("Active_", amount);
                        warpSpeedVFX.Stop();
                    }
                }
            }
        }

    }
}
