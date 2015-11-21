/*
    TrackingStationAmbientlight is an addon to KSP that allows easy
    configuration of the Ambientlight in the tracking station
 
    Copyright 2015 by Mhoram Kerbin
 
    TrackingStationAmbientlight is free software: you can redistribute it
    and/or modify it under the terms of the GNU General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    TrackingStationAmbientlight is distributed in the hope that it will be
    useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with TrackingStationAmbientlight.  If not, see
    <http://www.gnu.org/licenses/>.

 */

using System.Linq;
using UnityEngine;

namespace TrackingStationAmbientlight
{
    [KSPAddon(KSPAddon.Startup.TrackingStation, false)]
    public class TrackingStationAmbientlight : MonoBehaviour
    {
        private static bool hasRun = false;
        private static bool makeInactive = false;
        private class TsaConfig
        {
            public bool active { get; set; }
            public float red { get; set; }
            public float green { get; set; }
            public float blue { get; set; }
        }

        private static Color color;
        public void Start()
        {
            if (makeInactive)
            {
                return;
            }
            try
            {
                if (!hasRun) {
                    hasRun = true;
                    if (AssemblyLoader.loadedAssemblies.ToList().Exists(lA => lA.dllName == "AmbientLightAdjustment"))
                    {
                        mes("Disabling TrackingStationAmbientlight, since AmbientLightAdjustment is installed");
                        makeInactive = true;
                        return;
                    }
                    var cfg = GameDatabase.Instance.GetConfigNodes("TRACKING_STATION_AMBIENTLIGHT_CONFIG").FirstOrDefault();
                    if (cfg == null)
                    {
                        mes("TrackingStationAmbientlight: Could not load Confignode");
                        makeInactive = true;
                        return;
                    }
                    TsaConfig tsaColor = ResourceUtilities.LoadNodeProperties<TsaConfig>(cfg);
                    if (tsaColor == null) {
                        mes("TrackingStationAmbientlight: Could not get Confignode properties");
                        makeInactive = true;
                        return;
                    }
                    if(!tsaColor.active)
                    {
                        mes("TrackingStationAmbientlight: Deactivating based on configuration", false);
                        makeInactive = true;
                        return;
                    }
                    color = new Color(tsaColor.red / 256f, tsaColor.green / 256f, tsaColor.blue / 256f);
                }

                RenderSettings.ambientLight = color;
            }
            catch (System.Exception x)
            {
                Debug.LogError("Exception in TrackingStationAmbientlight: " + x.ToString());
            }
        }
        private void mes(string txt, bool screen = true)
        {
            Debug.Log(txt);
            if (screen)
            {
                ScreenMessages.PostScreenMessage(txt, 5, ScreenMessageStyle.UPPER_RIGHT);
            }
        }
    }
}
