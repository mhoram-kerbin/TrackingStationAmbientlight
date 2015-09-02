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

        private class TsaConfig
        {
            public bool active { get; set; }
            public float red { get; set; }
            public float green { get; set; }
            public float blue { get; set; }
        }

        public void Start()
        {
            try
            {
                var cfg = GameDatabase.Instance.GetConfigNodes("TRACKING_STATION_AMBIENTLIGHT_CONFIG").FirstOrDefault();
                if (cfg != null)
                {
                    TsaConfig color = ResourceUtilities.LoadNodeProperties<TsaConfig>(cfg);
                    if (color.active)
                    {
                        RenderSettings.ambientLight = new Color(color.red / 256f, color.green / 256f, color.blue / 256f);
                    }
                }
            }
            catch (System.ArgumentNullException x)
            {
                print("ArgumentNullException in TrackingStationAmbientlight: " + x.ToString());
            }
        }
    }
}
