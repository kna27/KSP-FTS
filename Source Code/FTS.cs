using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExplosionAbort
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ExplosionAbort : MonoBehaviour
    {

        public bool canAbort = true;
        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.A))
            {
                canAbort = !canAbort;
                if (canAbort)
                {
                    ScreenMessages.PostScreenMessage("Flight Termination System Activated", 2);
                }
                else
                {
                    ScreenMessages.PostScreenMessage("Flight Termination System Decctivated", 2);
                }
            }

            bool key = Input.GetKey(KeyCode.Backspace);
            if (key)
            {
                if (canAbort)
                {
                    List<Part> parts = FlightGlobals.ActiveVessel.parts;
                    QuickSaveLoad.QuickSave();
                    if (FlightGlobals.ActiveVessel.altitude <= 1000 || parts.Count >= 150)
                    {
                        StartCoroutine(explodeParts(0.001f));
                    }
                    else
                    {
                        StartCoroutine(explodeParts(0.005f));
                    }
                }

            }
        }

        IEnumerator explodeParts(float time)
        {
            List<Part> parts = FlightGlobals.ActiveVessel.parts;
            for (int i = parts.Count - 1; i >= 0; i--)
            {
                parts[i].explode();
                yield return new WaitForSeconds(time);
            }
        }
    }
}
