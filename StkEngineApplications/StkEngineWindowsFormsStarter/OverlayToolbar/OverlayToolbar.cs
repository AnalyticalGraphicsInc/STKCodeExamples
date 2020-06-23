using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKVgt;
using AGI.STKUtil;

namespace AGI
{
    //
    // OverlayToolbar
    //
    public class OverlayToolbar : IDisposable
    {
        //
        // Properties
        //
        public IAgStkGraphicsScreenOverlay Overlay { get { return (IAgStkGraphicsScreenOverlay)m_Panel; } }

        //
        // Constructor
        //
        public OverlayToolbar(AgStkObjectRoot root, AGI.STKX.Controls.AxAgUiAxVOCntrl control)
        {
            m_Root = root;
            m_Control3D = control;
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;

            m_ButtonHolders = new List<OverlayButton>();
            IAgStkGraphicsScreenOverlayManager ScreenOverlayManager = manager.ScreenOverlays;

            //
            // Panel
            //
            m_Panel = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 0, m_PanelWidth, OverlayButton.ButtonSize);
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft;
            overlay.BorderSize = 2;
            overlay.Color = Color.Transparent;
            overlay.BorderColor = Color.Transparent;
            overlay.Translucency = m_PanelTranslucencyRegular;
            overlay.BorderTranslucency = m_PanelBorderTranslucencyRegular;

            IAgStkGraphicsScreenOverlayCollectionBase managerOverlays = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            managerOverlays.Add((IAgStkGraphicsScreenOverlay)m_Panel);

            //
            // Buttons
            //
            string enabledImage, disabledImage;
            ActionDelegate action;

            //
            // ShowHide button
            //
            enabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\visible.png");
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\invisible.png");
            action = new ActionDelegate(ShowHideAction);
            AddButton(enabledImage, disabledImage, action);

            //
            // Reset button
            //
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\reset.png");
            action = new ActionDelegate(ResetAction);
            AddButton(disabledImage, action);

            // DecreaseDelta button
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\decreasedelta.png");
            action = new ActionDelegate(DecreaseDeltaAction);
            AddButton(disabledImage, action);

            //
            // StepBack button
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\stepreverse.png");
            action = new ActionDelegate(StepReverseAction);
            AddButton(disabledImage, action);
            m_StepReverseButton = m_ButtonHolders[m_ButtonHolders.Count - 1];

            //
            // PlayBack button
            //
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\playreverse.png");
            action = new ActionDelegate(PlayReverseAction);
            AddButton(disabledImage, action);

            //
            // Pause button
            //
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\pause.png");
            action = new ActionDelegate(PauseAction);
            AddButton(disabledImage, action);

            //
            // Play button
            //
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\playforward.png");
            action = new ActionDelegate(PlayForwardAction);
            AddButton(disabledImage, action);

            //
            // StepForward button
            //
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\stepforward.png");
            action = new ActionDelegate(StepForwardAction);
            AddButton(disabledImage, action);
            m_StepForwardButton = m_ButtonHolders[m_ButtonHolders.Count - 1];

            //
            // IncreaseDelta button
            //
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\increasedelta.png");
            action = new ActionDelegate(IncreaseDeltaAction);
            AddButton(disabledImage, action);

            //
            // Zoom button
            //
            enabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\zoompressed.png");
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\zoom.png");
            action = new ActionDelegate(ZoomAction);
            AddButton(enabledImage, disabledImage, action);
            m_ZoomButton = m_ButtonHolders[m_ButtonHolders.Count - 1];

            //
            // Pan button
            //
            enabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\panpressed.png");
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\pan.png");
            action = new ActionDelegate(PanAction);
            AddButton(enabledImage, disabledImage, action);

            //
            // Home button
            //
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\home.png");
            action = new ActionDelegate(HomeAction);
            AddButton(disabledImage, action);

            //
            // Moon Button
            //
            disabledImage = Path.Combine(Application.StartupPath, "ToolbarImages\\moon.png");
            action = new ActionDelegate(MoonAction);
            AddButton(disabledImage, action);

            IAgStkGraphicsScreenOverlayCollectionBase collectionBase = (IAgStkGraphicsScreenOverlayCollectionBase)overlay.Overlays;

            //
            // Scale button
            //
            m_ScaleButton = new OverlayButton(
                new ActionDelegate(ScaleAction),
                Path.Combine(Application.StartupPath, "ToolbarImages\\scale.png"), 
                0, m_PanelWidth, 0.5, 0, m_Root);
            IAgStkGraphicsOverlay scaleOverlay = (IAgStkGraphicsOverlay)m_ScaleButton.Overlay;
            scaleOverlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight;
            collectionBase.Add(m_ScaleButton.Overlay);
            m_ButtonHolders.Add(m_ScaleButton);

            //
            // Rotate button
            //
            m_RotateButton = new OverlayButton(
                new ActionDelegate(RotateAction),
                Path.Combine(Application.StartupPath, "ToolbarImages\\rotate.png"), 
                0, m_PanelWidth, 0.5, 0, m_Root);
            IAgStkGraphicsOverlay rotateOverlay = (IAgStkGraphicsOverlay)m_RotateButton.Overlay;
            rotateOverlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomRight;
            collectionBase.Add(m_RotateButton.Overlay);
            m_ButtonHolders.Add(m_RotateButton);
            DockBottom();
        }

        //
        // Public Methods
        //

        //
        // Adds a one-way button to the panel
        //
        public void AddButton(string image, ActionDelegate action)
        {
            AddButton(image, image, action);
        }

        //
        // Adds a two-way button to the panel
        //
        public void AddButton(string enabledImage, string disabledImage, ActionDelegate action)
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
            m_PanelWidth += OverlayButton.ButtonSize;
            overlay.Width = m_PanelWidth;

            OverlayButton newButton;
            newButton = new OverlayButton(action, disabledImage, m_LocationOffset, m_PanelWidth, m_Root);
            newButton.SetTexture(enabledImage, disabledImage);

            IAgStkGraphicsScreenOverlayCollectionBase collectionBase = (IAgStkGraphicsScreenOverlayCollectionBase)overlay.Overlays;
            collectionBase.Add(newButton.Overlay);
            m_ButtonHolders.Add(newButton);

            m_LocationOffset += OverlayButton.ButtonSize;

            foreach (OverlayButton button in m_ButtonHolders)
            {
                button.Resize(m_PanelWidth);
            }
        }

        public void DockRight()
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight;
            overlay.RotationAngle = Math.PI / 2;
            OrientButtons();
            overlay.TranslationX = -((overlay.Width / 2) - OverlayButton.ButtonSize / 2) * overlay.Scale;
            m_BaseAnchorPoint = new Point((int)overlay.TranslationX, (int)overlay.TranslationY);
        }

        public void DockBottom()
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomCenter;
            overlay.RotationAngle = 0;
            OrientButtons();
            overlay.TranslationY = 0;
            m_BaseAnchorPoint = new Point((int)overlay.TranslationX, (int)overlay.TranslationY);
        }

        public void DockLeft()
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterLeft;
            overlay.RotationAngle = Math.PI / 2;
            OrientButtons();
            overlay.TranslationX = -((overlay.Width / 2) - OverlayButton.ButtonSize / 2) * overlay.Scale;
            m_BaseAnchorPoint = new Point((int)overlay.TranslationX, (int)overlay.TranslationY);
        }

        public void DockTop()
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter;
            overlay.RotationAngle = 0;
            OrientButtons();
            overlay.TranslationY = 0;
            m_BaseAnchorPoint = new Point((int)overlay.TranslationX, (int)overlay.TranslationY);
        }

        //
        // Removes the panel from the scene manager
        //
        public void Remove()
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase collectionBase = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            collectionBase.Remove((IAgStkGraphicsScreenOverlay)m_Panel);
        }

        //
        // Private Methods
        //

        //
        // Orients all of the buttons on the Panel so that they do not rotate with the panel,
        // but, rather, flip every 90 degrees in order to remain upright.
        //
        private void OrientButtons()
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
            if ((overlay.RotationAngle <= -Math.PI / 4) || (overlay.RotationAngle > 5 * Math.PI / 4))
            {
                foreach (OverlayButton buttonHolder in m_ButtonHolders)
                {
                    if (buttonHolder != m_RotateButton && buttonHolder != m_ScaleButton)
                    {
                        IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)buttonHolder.Overlay;
                        buttonOverlay.RotationAngle = Math.PI / 2;
                    }
                }
            }
            else if ((overlay.RotationAngle > -Math.PI / 4) && (overlay.RotationAngle <= Math.PI / 4))
            {
                foreach (OverlayButton buttonHolder in m_ButtonHolders)
                {
                    if (buttonHolder != m_RotateButton && buttonHolder != m_ScaleButton)
                    {
                        IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)buttonHolder.Overlay;
                        buttonOverlay.RotationAngle = 0;
                    }
                }
            }
            else if ((overlay.RotationAngle > Math.PI / 4) && (overlay.RotationAngle <= 3 * Math.PI / 4))
            {
                foreach (OverlayButton buttonHolder in m_ButtonHolders)
                {
                    if (buttonHolder != m_RotateButton && buttonHolder != m_ScaleButton)
                    {
                        IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)buttonHolder.Overlay;
                        buttonOverlay.RotationAngle = -Math.PI / 2;
                    }
                }
            }
            else if ((overlay.RotationAngle > 3 * Math.PI / 4) && (overlay.RotationAngle <= 5 * Math.PI / 4))
            {
                foreach (OverlayButton buttonHolder in m_ButtonHolders)
                {
                    if (buttonHolder != m_RotateButton && buttonHolder != m_ScaleButton)
                    {
                        IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)buttonHolder.Overlay;
                        buttonOverlay.RotationAngle = -Math.PI;
                    }
                }
            }
        }

        //
        // Finds a button using a pick result
        //
        private OverlayButton FindButton(IAgStkGraphicsScreenOverlayPickResultCollection picked)
        {
            foreach (IAgStkGraphicsScreenOverlayPickResult pickResult in picked)
            {
                OverlayButton button = FindButton(pickResult.Overlay);
                if (button != null) return button;
            }
            return null;
        }

        //
        // Finds a button using an overlay
        //
        private OverlayButton FindButton(IAgStkGraphicsScreenOverlay Overlay)
        {
            foreach (OverlayButton button in m_ButtonHolders)
            {
                if (button.Overlay == Overlay)
                {
                    return button;
                }
            }
            return null;
        }

        //
        // Finds an overlay panel using a pick result
        //
        private bool OverlayPanelPicked(IAgStkGraphicsScreenOverlayPickResultCollection picked)
        {
            foreach (IAgStkGraphicsScreenOverlayPickResult pickResult in picked)
            {
                if (pickResult.Overlay == m_Panel)
                {
                    return true;
                }
            }
            return false;
        }

        //
        // Enables/disables the buttons
        //
        private void EnableButtons(OverlayButton excludeButton, bool bPickingEnabled)
        {
            foreach (OverlayButton button in m_ButtonHolders)
            {
                if (button != excludeButton)
                {
                    IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.Overlay;
                    buttonOverlay.PickingEnabled = bPickingEnabled;
                }
            }
        }

        //
        // Event handlers
        //

        //
        // When the mouse is moved
        //
        public void Control3D_MouseMove(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
        {
            IAgStkGraphicsScreenOverlayPickResultCollection picked = null;
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
            if (manager.Scenes.Count > 0)
            {
                picked = manager.Scenes[0].PickScreenOverlays(e.x, e.y);
            }

            OverlayButton button = FindButton(picked);

            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;

            if (!m_Tranforming)
            {
                if (OverlayPanelPicked(picked) && !m_PanelCurrentlyPicked)
                {
                    overlay.BorderTranslucency = m_PanelBorderTranslucencyPicked;
                    overlay.Translucency = m_PanelTranslucencyPicked;
                    m_PanelCurrentlyPicked = true;
                    m_PanelTranslucencyChanged = true;
                }
                else if (!OverlayPanelPicked(picked) && m_PanelCurrentlyPicked)
                {
                    overlay.BorderTranslucency = m_PanelBorderTranslucencyRegular;
                    overlay.Translucency = m_PanelTranslucencyRegular;
                    m_PanelCurrentlyPicked = false;
                    m_PanelTranslucencyChanged = true;
                }
                if (m_PanelTranslucencyChanged)
                {
                    m_PanelTranslucencyChanged = false;
                    if (!m_Animating)
                    {
                        if (manager.Scenes.Count > 0)
                        {
                            manager.Scenes[0].Render();
                        }
                    }
                }
            }

            if (button != null)
            {
                if (m_MouseOverButton != null && m_MouseOverButton != button)
                {
                    m_MouseOverButton.MouseLeave();
                }
                m_MouseOverButton = button;
                m_MouseOverButton.MouseEnter();
            }
            else
            {
                if (m_AnchorPoint != Point.Empty)
                {
                    int offsetX = (e.x - m_AnchorPoint.X);
                    int offsetY = (m_AnchorPoint.Y - e.y);

                    // This fixes the bug with the ScreenOverlayOrigin being different.
                    // Before, if you dragged left with +x to the left, the panel would
                    // have gone right.
                    if (overlay.Origin == AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomRight ||
                        overlay.Origin == AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight ||
                        overlay.Origin == AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight)
                    {
                        overlay.TranslationX = m_BaseAnchorPoint.X - offsetX;
                    }
                    else
                    {
                        overlay.TranslationX = m_BaseAnchorPoint.X + offsetX;
                    }

                    // check that translations in the x direction are inside the window
                    double dist = (m_Control3D.Width - m_PanelWidth) / 2;
                    double minX = -dist;
                    double maxX = dist;
                    if (overlay.TranslationX < minX)
                        overlay.TranslationX = minX;
                    else if (overlay.TranslationX > maxX)
                        overlay.TranslationX = maxX;

                    if (overlay.Origin == AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight ||
                        overlay.Origin == AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopCenter ||
                        overlay.Origin == AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft)
                    {
                        overlay.TranslationY = m_BaseAnchorPoint.Y - offsetY;

                        // check that translations in the y direction are inside the window
                        double maxYBorder = m_Control3D.Height;
                        double minYBorder = overlay.Height;
                        if (overlay.TranslationY < minYBorder)
                            overlay.TranslationY = minYBorder;
                        else if (overlay.TranslationY > maxYBorder)
                            overlay.TranslationY = maxYBorder;
                    }
                    else
                    {
                        overlay.TranslationY = m_BaseAnchorPoint.Y + offsetY;

                        // check that translations in the y direction are inside the window
                        double yBorderDist = m_Control3D.Height - overlay.Height;
                        if (overlay.TranslationY < 0)
                            overlay.TranslationY = 0;
                        else if (overlay.TranslationY > yBorderDist)
                            overlay.TranslationY = yBorderDist;
                    }

                    if (!m_Animating)
                    {
                        if (manager.Scenes.Count > 0)
                        {
                            manager.Scenes[0].Render();
                        }
                    }
                }
                else if (m_RotatePoint != Point.Empty)
                {
                    Point current = new Point(e.x, e.y);
                    current.Offset(m_RotatePoint);
                    object[] bounds = (object[])overlay.ControlBounds;
                    double centerX = ((double)overlay.ControlPosition.GetValue(0)) + ((int)bounds.GetValue(2)) / 2;
                    double centerY = ((double)overlay.ControlPosition.GetValue(1)) + ((int)bounds.GetValue(3)) / 2;
                    double adjacent = (e.x - centerX);
                    double opposite = ((m_Control3D.Bounds.Height - e.y) - centerY);

                    if (adjacent >= 0)
                    {
                        overlay.RotationAngle = Math.Atan(opposite / adjacent);
                    }
                    else
                    {
                        overlay.RotationAngle = Math.PI + Math.Atan(opposite / adjacent);
                    }

                    OrientButtons();

                    if (!m_Animating)
                    {
                        if (manager.Scenes.Count > 0)
                        {
                            manager.Scenes[0].Render();
                        }
                    }
                }
                else if (m_ScalePoint != Point.Empty)
                {
                    // Complete rework of scaling..
                    double scale = 1;

                    // Get the cos,sin and tan to make this easier to understand.
                    double cos = Math.Cos(overlay.RotationAngle);
                    double sin = Math.Sin(overlay.RotationAngle);
                    double tan = Math.Tan(overlay.RotationAngle);

                    double xVector = (e.x - m_ScalePoint.X);
                    double yVector = (m_ScalePoint.Y - e.y);

                    // Get the projection of e.X and e.Y in the direction
                    // of the toolbar's horizontal.
                    double x = ((xVector * cos + yVector * sin) * cos);
                    double y = ((xVector * cos + yVector * sin) * sin);

                    // Figure out if we are shrinking or growing the toolbar
                    // (This is dependant on the quadrant we are in)
                    double magnitude = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (sin >= 0 && cos >= 0 && tan >= 0)
                    {
                        magnitude = (x < 0 | y < 0) ? -magnitude : magnitude;
                    }
                    else if (sin >= 0)
                    {
                        magnitude = (x > 0 | y < 0) ? -magnitude : magnitude;
                    }
                    else if (tan >= 0)
                    {
                        magnitude = (x > 0 | y > 0) ? -magnitude : magnitude;
                    }
                    else if (cos >= 0)
                    {
                        magnitude = (x < 0 | y > 0) ? -magnitude : magnitude;
                    }

                    scale = ((magnitude + m_ScaleBounds.Width) / m_ScaleBounds.Width);

                    if (scale < 0)
                    {
                        scale = 0;
                    }


                    overlay.Scale = Math.Min(Math.Max(m_StartScale * scale, 0.5), 10);
                    double width = m_PanelWidth * overlay.Scale;
                    double startWidth = m_PanelWidth * m_StartScale;

                    // Translate the toolbar in order to account for the
                    // fact that rotation does not affect the location
                    // of the toolbar, but just rotates the texture.
                    // (This causes the toolbar, if +/-90 degrees to scale
                    // off the screen if not fixed).
                    overlay.TranslationX = m_ScaleOffset - (((width / 2) - (startWidth / 2)) * Math.Abs(Math.Sin(overlay.RotationAngle)));

                    if (!m_Animating)
                    {
                        if (manager.Scenes.Count > 0)
                        {
                            manager.Scenes[0].Render();
                        }
                    }
                }
                else if (m_MouseOverButton != null)
                {
                    m_MouseOverButton.MouseLeave();
                    m_MouseOverButton = null;
                }
            }
        }

        //
        // When the mouse pressed
        //
        public void Control3D_MouseDown(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEvent e)
        {
            IAgStkGraphicsScreenOverlayPickResultCollection picked = null;
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
            if (manager.Scenes.Count > 0)
            {
                picked = manager.Scenes[0].PickScreenOverlays(e.x, e.y);
            }

            OverlayButton button = FindButton(picked);

            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;

            if (button == null && OverlayPanelPicked(picked))
            {
                m_MouseDown = true;
                m_Control3D.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeManual;
                m_AnchorPoint = new Point(e.x, e.y);

                overlay.Translucency = m_PanelTranslucencyClicked;
                overlay.BorderTranslucency = m_PanelBorderTranslucencyClicked;

                if (manager.Scenes.Count > 0)
                {
                    manager.Scenes[0].Render();
                }
            }

            if (button != null)
            {
                m_MouseDown = true;
                m_MouseOverButton = button;
                m_MouseDownButton = button;
                m_MouseOverButton.MouseDown();

                if (button == m_RotateButton)
                {
                    m_Tranforming = true;
                    m_Control3D.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeManual;
                    m_RotatePoint = new Point(e.x, e.y);
                    EnableButtons(m_RotateButton, false);
                }
                if (button == m_ScaleButton)
                {
                    m_Tranforming = true;
                    m_Control3D.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeManual;
                    m_ScalePoint = new Point(e.x, e.y);
                    m_StartScale = overlay.Scale;
                    m_ScaleOffset = overlay.TranslationX;
                    object[] bounds = (object[])overlay.ControlBounds;
                    m_ScaleBounds = new Rectangle((int)bounds.GetValue(0), (int)bounds.GetValue(1), (int)bounds.GetValue(2), (int)bounds.GetValue(3));
                    EnableButtons(m_ScaleButton, false);
                }
            }

            m_LastMouseClick = new Point(e.x, e.y);
        }

        //
        // When the mouse is unpressed
        //
        public void Control3D_MouseUp(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEvent e)
        {
            if (m_MouseDown)
            {
                IAgStkGraphicsScreenOverlayPickResultCollection picked = null;
                IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
                if (manager.Scenes.Count > 0)
                {
                    picked = manager.Scenes[0].PickScreenOverlays(e.x, e.y);
                } 
                
                OverlayButton button = FindButton(picked);

                IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;

                if (button == null && OverlayPanelPicked(picked))
                {
                    overlay.Translucency = m_PanelTranslucencyPicked;
                    overlay.BorderTranslucency = m_PanelBorderTranslucencyPicked;
                    if (!m_Animating)
                    {
                        if (manager.Scenes.Count > 0)
                        {
                            manager.Scenes[0].Render();
                        }
                    }
                }

                if (button != null)
                {
                    m_MouseOverButton = button;
                    m_MouseOverButton.MouseUp();

                    // Check if this button was under the mouse during both the mouse down and mouse up event
                    // (i.e. the button was clicked)
                    if (m_MouseOverButton == m_MouseDownButton)
                        m_MouseOverButton.MouseClick();
                }

                m_AnchorPoint = Point.Empty;
                m_RotatePoint = Point.Empty;
                m_ScalePoint = Point.Empty;

                EnableButtons(null, true);
                m_BaseAnchorPoint = new Point((int)overlay.TranslationX, (int)overlay.TranslationY);

                m_Tranforming = false;
                m_Control3D.MouseMode = AGI.STKX.AgEMouseMode.eMouseModeAutomatic;
                m_MouseDown = false;
            }
        }

        //
        // When the mouse is double clicked
        //
        public void Control3D_MouseDoubleClick(object sender, EventArgs e)
        {
            IAgStkGraphicsScreenOverlayPickResultCollection picked = null;
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
            if (manager.Scenes.Count > 0)
            {
                picked = manager.Scenes[0].PickScreenOverlays(m_LastMouseClick.X, m_LastMouseClick.Y);
            } 
            
            OverlayButton button = FindButton(picked);

            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;

            if (button == null && OverlayPanelPicked(picked))
            {
                overlay.TranslationX = 0;
                overlay.TranslationY = 0;

                overlay.RotationAngle = 0;
                overlay.Scale = 1;
                OrientButtons();
            }
        }

        //
        // Button actions
        //
        public void ShowHideAction()
        {
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)m_Panel;
            if (m_Visible)
            {
                double width = (double)overlay.Size.GetValue(0);
                double height = (double)overlay.Size.GetValue(1);
                double x = overlay.Scale * ((width / 2) - OverlayButton.ButtonSize / 2);
                double y = overlay.Scale * ((height / 2) - OverlayButton.ButtonSize / 2);
                double z = Math.Sqrt(x * x + y * y);
                double panelWidth = m_PanelWidth;
                double panelHeight = height;

                m_PanelWidth = OverlayButton.ButtonSize;
                overlay.Width = m_PanelWidth;
                m_ButtonHolders[0].Resize(m_PanelWidth);

                foreach (OverlayButton button in m_ButtonHolders)
                {
                    if (button != m_ButtonHolders[0])
                    {
                        IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.Overlay;
                        buttonOverlay.Translucency = 1;
                    }
                }

                overlay.TranslationX -= (int)(z * Math.Cos(overlay.RotationAngle) - overlay.Scale * panelWidth / 2 + overlay.Scale * OverlayButton.ButtonSize / 2);
                overlay.TranslationY -= (int)(z * Math.Sin(overlay.RotationAngle) - overlay.Scale * panelHeight / 2 + overlay.Scale * OverlayButton.ButtonSize / 2);
            }
            else
            {
                m_PanelWidth = (int)((m_ButtonHolders.Count - 1.5) * OverlayButton.ButtonSize);
                overlay.Width = m_PanelWidth;
                m_ButtonHolders[0].Resize(m_PanelWidth);

                double width = (double)overlay.Size.GetValue(0);
                double height = (double)overlay.Size.GetValue(1);
                double x = overlay.Scale * ((width / 2) - OverlayButton.ButtonSize / 2);
                double y = overlay.Scale * ((height / 2) - OverlayButton.ButtonSize / 2);
                double z = Math.Sqrt(x * x + y * y);

                foreach (OverlayButton button in m_ButtonHolders)
                {
                    if (button != m_ButtonHolders[0])
                    {
                        IAgStkGraphicsOverlay buttonOverlay = (IAgStkGraphicsOverlay)button.Overlay;
                        buttonOverlay.Translucency = 0;
                    }
                }

                overlay.TranslationX += (int)(z * Math.Cos(overlay.RotationAngle) - overlay.Scale * width / 2 + overlay.Scale * OverlayButton.ButtonSize / 2);
                overlay.TranslationY += (int)(z * Math.Sin(overlay.RotationAngle) - overlay.Scale * height / 2 + overlay.Scale * OverlayButton.ButtonSize / 2);
            }

            m_Visible = !m_Visible;
        }

        public void ResetAction()
        {
            m_Root.Rewind();
            m_Animating = false;
            EnableStepButtons();
        }

        public void StepReverseAction()
        {
            m_Root.StepBackward();
        }

        public void PlayReverseAction()
        {
            DisableStepButtons();
            m_Root.PlayBackward();
            m_Animating = true;
        }

        public void PauseAction()
        {
            m_Root.Pause();
            m_Animating = false;
            EnableStepButtons();
        }

        public void PlayForwardAction()
        {
            DisableStepButtons();
            m_Root.PlayForward();
            m_Animating = true;
        }

        public void StepForwardAction()
        {
            m_Root.StepForward();
        }

        public void DecreaseDeltaAction()
        {
            m_Root.Slower();
        }

        public void IncreaseDeltaAction()
        {
            m_Root.Faster();
        }

        public void ZoomAction()
        {
            m_Control3D.ZoomIn();
        }

        public void PanAction()
        {
            if (!m_Panning)
            {
                try
                {
                    m_Root.ExecuteCommand("Window3D * InpDevMode Mode PanLLA");
                    m_Panning = true;
                }
                catch (Exception)
                {
                    m_Panning = false;
                }
            }
            else
            {
                try
                {
                    m_Root.ExecuteCommand("Window3D * InpDevMode Mode ViewLatLonAlt");
                    m_Panning = false;
                }
                catch (Exception)
                {
                    m_Panning = true;
                }
            }
        }

        public void HomeAction()
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
            IAgCrdnAxes earthAxes = ((IAgCrdnSystemAssembled)m_Root.CentralBodies["Earth"].Vgt.Systems["ICRF"]).ReferenceAxes.GetAxes();
            if (manager.Scenes.Count > 0)
                manager.Scenes[0].Camera.ViewCentralBody("Earth", earthAxes);
            m_Panning = false;
        }

        public void MoonAction()
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)m_Root.CurrentScenario).SceneManager;
            IAgCrdnAxes moonAxes = ((IAgCrdnSystemAssembled)m_Root.CentralBodies["Moon"].Vgt.Systems["ICRF"]).ReferenceAxes.GetAxes();
            if (manager.Scenes.Count > 0)
                manager.Scenes[0].Camera.ViewCentralBody("Moon", moonAxes);
            m_Panning = false;
        }

        public void ScaleAction()
        {
        }

        public void RotateAction()
        {
        }

        private void EnableStepButtons()
        {
            m_StepForwardButton.SetEnabled(true);
            m_StepReverseButton.SetEnabled(true);
        }

        private void DisableStepButtons()
        {
            m_StepForwardButton.SetEnabled(false);
            m_StepReverseButton.SetEnabled(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OverlayToolbar()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_Panel != null)
                {
                    m_Panel = null;
                }
                if (m_RotateButton != null)
                {
                    m_RotateButton.Dispose();
                    m_RotateButton = null;
                }
                if (m_ScaleButton != null)
                {
                    m_ScaleButton.Dispose();
                    m_ScaleButton = null;
                }
                if (m_ZoomButton != null)
                {
                    m_ZoomButton.Dispose();
                    m_ZoomButton = null;
                }
                if (m_StepForwardButton != null)
                {
                    m_StepForwardButton.Dispose();
                    m_StepForwardButton = null;
                }
                if (m_StepReverseButton != null)
                {
                    m_StepReverseButton.Dispose();
                    m_StepReverseButton = null;
                }
            }
        }

        //
        // Members
        //
        private int m_PanelWidth = (int)(OverlayButton.ButtonSize * 0.5);
        private int m_LocationOffset;

        private float m_PanelTranslucencyRegular = 0.95f;
        private float m_PanelTranslucencyPicked = 0.85f;
        private float m_PanelTranslucencyClicked = 0.8f;
        private float m_PanelBorderTranslucencyRegular = 0.6f;
        private float m_PanelBorderTranslucencyPicked = 0.5f;
        private float m_PanelBorderTranslucencyClicked = 0.4f;

        private OverlayButton m_RotateButton;
        private OverlayButton m_ScaleButton;
        private OverlayButton m_ZoomButton;
        private OverlayButton m_StepForwardButton;
        private OverlayButton m_StepReverseButton;

        private Point m_AnchorPoint = Point.Empty;
        private Point m_RotatePoint = Point.Empty;
        private Point m_ScalePoint = Point.Empty;
        private Point m_BaseAnchorPoint = Point.Empty;
        private IAgStkGraphicsTextureScreenOverlay m_Panel;

        private Rectangle m_ScaleBounds;
        private double m_StartScale;
        private double m_ScaleOffset;

        private bool m_PanelTranslucencyChanged;
        private bool m_PanelCurrentlyPicked;
        private bool m_Visible = true;
        private bool m_Tranforming;
        private bool m_Animating = false;
        private bool m_Panning = false;

        private OverlayButton m_MouseOverButton;
        private OverlayButton m_MouseDownButton;
        private bool m_MouseDown;
        private Point m_LastMouseClick;
        private List<OverlayButton> m_ButtonHolders;

        private AgStkObjectRoot m_Root;
        private AGI.STKX.Controls.AxAgUiAxVOCntrl m_Control3D;
    }
}
