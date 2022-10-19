using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProjectionRasterStreamPlugin
{
    public class GifProvider
    {
        public GifProvider(string image)
        {
            try { m_Image = (Bitmap)Image.FromFile(image); }
            catch (System.Exception) { }
            m_nFrameCount = m_Image.GetFrameCount(FrameDimension.Time);
        }

        public Bitmap GetFrame(int frame)
        {
            try { m_Image.SelectActiveFrame(FrameDimension.Time, frame); }
            catch (System.Exception) { }
            return m_Image;
        }

        public Bitmap NextFrame()
        {
            if (m_nCurrentFrame == m_nFrameCount - 1) m_nCurrentFrame = 0;
            Bitmap frame = GetFrame(m_nCurrentFrame);
            m_nCurrentFrame++;
            return frame;
        }

        public Bitmap PreviousFrame()
        {
            if (m_nCurrentFrame == 0) m_nCurrentFrame = m_nFrameCount;
            Bitmap frame = GetFrame(m_nCurrentFrame);
            m_nCurrentFrame--;
            return frame;
        }

        public Size Size { get { return GetFrame(0).Size; } }

        public int FrameCount { get { return m_nFrameCount; } }

        Bitmap m_Image;
        int m_nFrameCount;
        int m_nCurrentFrame;
    }
}
