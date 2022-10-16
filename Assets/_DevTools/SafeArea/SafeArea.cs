using UnityEngine;

namespace Crystal
{
    /// <summary>
    ///     Safe area implementation for notched mobile devices. Usage:
    ///     (1) Add this component to the top level of any GUI panel.
    ///     (2) If the panel uses a full screen background image, then create an immediate child and put the component on that
    ///     instead, with all other elements childed below it.
    ///     This will allow the background image to stretch to the full extents of the screen behind the notch, which looks
    ///     nicer.
    ///     (3) For other cases that use a mixture of full horizontal and vertical background stripes, use the Conform X & Y
    ///     controls on separate elements as needed.
    /// </summary>
    public class SafeArea : MonoBehaviour
    {
        [SerializeField]
        private bool ConformX = true; // Conform to screen safe area on X-axis (default true, disable to ignore)

        [SerializeField]
        private bool ConformY = true; // Conform to screen safe area on Y-axis (default true, disable to ignore)

        private Rect LastSafeArea = new(0, 0, 0, 0);

        private RectTransform Panel;

        private void Awake()
        {
            Panel = GetComponent<RectTransform>();

            if (Panel == null)
            {
                Debug.LogError("Cannot apply safe area - no RectTransform found on " + name);
                Destroy(gameObject);
            }

            Refresh();
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            var safeArea = GetSafeArea();

            if (safeArea != LastSafeArea)
                ApplySafeArea(safeArea);
        }

        private Rect GetSafeArea()
        {
            var safeArea = Screen.safeArea;

            if (Application.isEditor && Sim != SimDevice.None)
            {
                var nsa = new Rect(0, 0, Screen.width, Screen.height);

                switch (Sim)
                {
                    case SimDevice.iPhoneX:
                        if (Screen.height > Screen.width) // Portrait
                            nsa = NSA_iPhoneX[0];
                        else // Landscape
                            nsa = NSA_iPhoneX[1];
                        break;
                    case SimDevice.iPhoneXsMax:
                        if (Screen.height > Screen.width) // Portrait
                            nsa = NSA_iPhoneXsMax[0];
                        else // Landscape
                            nsa = NSA_iPhoneXsMax[1];
                        break;
                }

                safeArea = new Rect(Screen.width * nsa.x, Screen.height * nsa.y, Screen.width * nsa.width,
                    Screen.height * nsa.height);
            }

            return safeArea;
        }

        private void ApplySafeArea(Rect r)
        {
            LastSafeArea = r;

            // Ignore x-axis?
            if (!ConformX)
            {
                r.x = 0;
                r.width = Screen.width;
            }

            // Ignore y-axis?
            if (!ConformY)
            {
                r.y = 0;
                r.height = Screen.height;
            }

            // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
            var anchorMin = r.position;
            var anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            Panel.anchorMin = anchorMin;
            Panel.anchorMax = anchorMax;

            //Debug.LogFormat ("New safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
            //    name, r.x, r.y, r.width, r.height, Screen.width, Screen.height); dev
        }

        #region Simulations

        /// <summary>
        ///     Simulation device that uses safe area due to a physical notch or software home bar. For use in Editor only.
        /// </summary>
        public enum SimDevice
        {
            /// <summary>
            ///     Don't use a simulated safe area - GUI will be full screen as normal.
            /// </summary>
            None,

            /// <summary>
            ///     Simulate the iPhone X and Xs (identical safe areas).
            /// </summary>
            iPhoneX,

            /// <summary>
            ///     Simulate the iPhone Xs Max and XR (identical safe areas).
            /// </summary>
            iPhoneXsMax
        }

        /// <summary>
        ///     Simulation mode for use in editor only. This can be edited at runtime to toggle between different safe areas.
        /// </summary>
        public static SimDevice Sim = SimDevice.None;

        /// <summary>
        ///     Normalised safe areas for iPhone X with Home indicator (ratios are identical to iPhone Xs). Absolute values:
        ///     PortraitU x=0, y=102, w=1125, h=2202 on full extents w=1125, h=2436;
        ///     PortraitD x=0, y=102, w=1125, h=2202 on full extents w=1125, h=2436 (not supported, remains in Portrait Up);
        ///     LandscapeL x=132, y=63, w=2172, h=1062 on full extents w=2436, h=1125;
        ///     LandscapeR x=132, y=63, w=2172, h=1062 on full extents w=2436, h=1125.
        ///     Aspect Ratio: ~19.5:9.
        /// </summary>
        private readonly Rect[] NSA_iPhoneX =
        {
            new(0f, 102f / 2436f, 1f, 2202f / 2436f), // Portrait
            new(132f / 2436f, 63f / 1125f, 2172f / 2436f, 1062f / 1125f) // Landscape
        };

        /// <summary>
        ///     Normalised safe areas for iPhone Xs Max with Home indicator (ratios are identical to iPhone XR). Absolute values:
        ///     PortraitU x=0, y=102, w=1242, h=2454 on full extents w=1242, h=2688;
        ///     PortraitD x=0, y=102, w=1242, h=2454 on full extents w=1242, h=2688 (not supported, remains in Portrait Up);
        ///     LandscapeL x=132, y=63, w=2424, h=1179 on full extents w=2688, h=1242;
        ///     LandscapeR x=132, y=63, w=2424, h=1179 on full extents w=2688, h=1242.
        ///     Aspect Ratio: ~19.5:9.
        /// </summary>
        private readonly Rect[] NSA_iPhoneXsMax =
        {
            new(0f, 102f / 2688f, 1f, 2454f / 2688f), // Portrait
            new(132f / 2688f, 63f / 1242f, 2424f / 2688f, 1179f / 1242f) // Landscape
        };

        #endregion
    }
}