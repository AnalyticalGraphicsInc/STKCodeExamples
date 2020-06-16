using System;
using AGI.STKGraphics;
using AGI.STKObjects;
using System.Drawing;

namespace AGI
{
    public delegate void ActionDelegate();

    //
    // OverlayButtonHolder
    //
    public class OverlayButton : IDisposable
    {
        //
        // Properties
        //
        public IAgStkGraphicsScreenOverlay Overlay { get { return (IAgStkGraphicsScreenOverlay)m_Overlay; } }
        public static int ButtonSize { get { return m_ButtonSize; } }

        //
        // Constructors
        //
        public OverlayButton(ActionDelegate action, string image, int xOffset, double panelWidth, AgStkObjectRoot root)
        {
            m_Manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            m_Action = action;
            m_MouseEnterTranslucency = 0.01f;
            m_MouseExitTranslucency = 0.25f;

            m_Overlay = m_Manager.Initializers.TextureScreenOverlay.Initialize();

            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay) m_Overlay;
            overlay.X = ((double)xOffset) / panelWidth;
            overlay.XUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
            overlay.Width = ((double)ButtonSize) / panelWidth;
            overlay.WidthUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
            overlay.Height = 1;
            overlay.HeightUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
            overlay.Translucency = m_MouseExitTranslucency;

            m_Overlay.Texture = m_Manager.Textures.LoadFromStringUri(image);

            m_Enabled = true;
            m_DisabledImage = image;
            m_EnabledImage = m_DisabledImage;

            m_Offset = xOffset;
        }

        public OverlayButton(ActionDelegate action, string image, int xOffset, double panelWidth, double scale, double rotate, AgStkObjectRoot root)
            : this(action, image, xOffset, panelWidth, root)
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Overlay;
            overlay.Scale = scale;
            overlay.RotationAngle = rotate;
        }

        //
        // Public Methods
        //

        //
        // Sets both the enabled image and disabled image to the input image
        //
        public void SetTexture(string image)
        {
            m_EnabledImage = image;
            m_DisabledImage = image;
        }

        //
        // Sets both an enabled image and a disabled image for an on/off button
        //
        public void SetTexture(string enabledImage, string disabledImage)
        {
            m_EnabledImage = enabledImage;
            m_DisabledImage = disabledImage;
        }

        //
        // Sets the on/off texture of a button
        //
        public void SetState(bool state)
        {
            m_State = state;

            if (state)
            {
                m_Overlay.Texture = m_Manager.Textures.LoadFromStringUri(m_EnabledImage);
            }
            else
            {
                m_Overlay.Texture = m_Manager.Textures.LoadFromStringUri(m_DisabledImage);
            }
        }

        //
        // Enables or disables a button
        //
        public void SetEnabled(bool enabled)
        {
            m_Enabled = enabled;
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Overlay;

            if (enabled)
            {
                overlay.Color = System.Drawing.Color.White;
            }
            else
            {
                overlay.Color = System.Drawing.Color.Gray;
            }
        }

        public void Resize(double panelWidth)
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Overlay;

            overlay.Width = ((double)ButtonSize) / panelWidth;
            overlay.WidthUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
            overlay.Height = 1;
            overlay.HeightUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;

            overlay.X = ((double)m_Offset) / ((double)panelWidth);
            overlay.XUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
        }

        //
        // Event handlers
        //
        public virtual void MouseEnter()
        {
            if (m_Enabled)
            {
                IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Overlay;
                overlay.Translucency = m_MouseEnterTranslucency;
                if (m_Manager.Scenes.Count > 0)
                {
                    m_Manager.Scenes[0].Render();
                }
            }
        }
        public virtual void MouseLeave()
        {
            if (m_Enabled)
            {
                IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Overlay;
                overlay.Translucency = m_MouseExitTranslucency;
                overlay.Color = System.Drawing.Color.White;
                if (m_Manager.Scenes.Count > 0)
                {
                    m_Manager.Scenes[0].Render();
                }
            }
        }
        public virtual void MouseDown()
        {
            if (m_Enabled)
            {
                IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Overlay;
                overlay.Color = System.Drawing.Color.DarkGray;
                if (m_Manager.Scenes.Count > 0)
                {
                    m_Manager.Scenes[0].Render();
                }
            }
        }
        public virtual void MouseUp()
        {
            if (m_Enabled)
            {
                IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Overlay;
                overlay.Color = System.Drawing.Color.White;
                if (m_Manager.Scenes.Count > 0)
                {
                    m_Manager.Scenes[0].Render();
                }
            }
        }
        public virtual void MouseClick()
        {
            if (m_Enabled)
            {
                SetState(!m_State);

                if (m_Action != null)
                {
                    m_Action();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OverlayButton()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_Overlay = null;
            }
        }

        //
        // Members
        //
        private static int m_ButtonSize = 35;

        private float m_MouseEnterTranslucency;
        private float m_MouseExitTranslucency;
        private IAgStkGraphicsTextureScreenOverlay m_Overlay;

        private ActionDelegate m_Action;

        private bool m_State;
        private bool m_Enabled;
        private string m_EnabledImage;
        private string m_DisabledImage;

        private int m_Offset;

        private IAgStkGraphicsSceneManager m_Manager;
    }
}
