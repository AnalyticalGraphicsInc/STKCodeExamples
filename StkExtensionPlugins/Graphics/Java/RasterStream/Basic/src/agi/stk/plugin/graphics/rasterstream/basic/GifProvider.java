package agi.stk.plugin.graphics.rasterstream.basic;

// Java API
import java.awt.*;
import java.awt.geom.*;
import java.awt.image.*;
import java.io.*;
import java.util.*;
import javax.imageio.*;
import javax.imageio.metadata.*;
import javax.imageio.stream.*;
import javax.xml.xpath.*;
import org.w3c.dom.*;

// AGI Java API
import agi.core.*;

public class GifProvider
{
	private final HashMap<Integer, BufferedImage>	m_frameCache;
	private final XPathExpression					m_horizontalOffsetExpression;
	private final XPathExpression					m_verticalOffsetExpression;
	private final Dimension							m_size;

	private ImageReader								m_imageReader;
	private int										m_frameCount;
	private int										m_currentFrameNumber;

	public GifProvider(String image)
	throws AgCoreException
	{
		try
		{
			this.m_frameCache = new HashMap<Integer, BufferedImage>();

			XPathFactory xpathFactory = XPathFactory.newInstance();
			XPath xpath = xpathFactory.newXPath();
			this.m_horizontalOffsetExpression = xpath.compile("Dimension/HorizontalPixelOffset/@value");
			this.m_verticalOffsetExpression = xpath.compile("Dimension/VerticalPixelOffset/@value");

			ImageInputStream stream = ImageIO.createImageInputStream(new File(image));
			Iterator<ImageReader> readers = ImageIO.getImageReaders(stream);
			if(readers.hasNext())
			{
				this.m_imageReader = readers.next();
				this.m_imageReader.setInput(stream);
			}

			this.m_frameCount = this.m_imageReader.getNumImages(true);
			this.m_size = new Dimension(this.m_imageReader.getWidth(0), this.m_imageReader.getHeight(0));
		}
		catch(Throwable t)
		{
			throw new AgCoreException(t);
		}
	}

	public final BufferedImage getFrame(int frameNumber)
	throws Exception
	{
		if(this.m_frameCache.containsKey(frameNumber))
		{
			return this.m_frameCache.get(frameNumber);
		}

		BufferedImage rawFrame = this.m_imageReader.read(frameNumber);

		IIOMetadata metadata = this.m_imageReader.getImageMetadata(frameNumber);
		Node metadataTree = metadata.getAsTree(IIOMetadataFormatImpl.standardMetadataFormatName);

		// calculate the offset for this frame and redraw it in the right spot
		// with a translation
		BufferedImage frame = new BufferedImage(this.m_size.width, this.m_size.height, BufferedImage.TYPE_INT_ARGB);
		Graphics2D g = (Graphics2D)frame.getGraphics();

		int horizontalOffset = Integer.parseInt(this.m_horizontalOffsetExpression.evaluate(metadataTree));
		int verticalOffset = Integer.parseInt(this.m_verticalOffsetExpression.evaluate(metadataTree));
		g.drawImage(rawFrame, AffineTransform.getTranslateInstance(horizontalOffset, verticalOffset), null);

		this.m_frameCache.put(frameNumber, frame);
		return frame;
	}

	public final BufferedImage nextFrame()
	throws Exception
	{
		if(this.m_currentFrameNumber >= this.m_frameCount)
		{
			this.m_currentFrameNumber = 0;
		}

		BufferedImage frame = getFrame(this.m_currentFrameNumber);
		this.m_currentFrameNumber++;
		return frame;
	}

	public final byte[] nextFrameAsByteArray()
	throws Exception
	{
		BufferedImage bi = nextFrame();
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		ImageIO.write(bi, "gif", baos);
		baos.flush();
		byte[] imageInByte = baos.toByteArray();
		baos.close();
		return imageInByte;
	}

	public final BitmapData nextFrameAsBitmapData()
	throws Exception
	{
		BufferedImage bi = nextFrame();
		return toBitmapData(bi);
	}

	public final BufferedImage previousFrame()
	throws Exception
	{
		if(this.m_currentFrameNumber <= 0)
		{
			this.m_currentFrameNumber = this.m_frameCount-1;
		}

		BufferedImage frame = getFrame(this.m_currentFrameNumber);
		this.m_currentFrameNumber--;
		return frame;
	}

	public final byte[] previousFrameAsByteArray()
	throws Exception
	{
		BufferedImage bi = previousFrame();
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		ImageIO.write(bi, "gif", baos);
		baos.flush();
		byte[] imageInByte = baos.toByteArray();
		baos.close();
		return imageInByte;
	}

	public final BitmapData previousFrameAsBitmapData()
	throws Exception
	{
		BufferedImage bi = previousFrame();
		return toBitmapData(bi);
	}

	private static BitmapData toBitmapData(BufferedImage image)
	{
		SampleModel sampleModel = image.getSampleModel();
		int scanlineStride;

		// What Java generally calls "BGR" order (meaning that the bytes appear
		// in the order B, G, R) is what .NET (and therefore the Mummra layer)
		// calls RGB. So these mapping sort of look backwards.
		int pixelFormat;
		switch(image.getType())
		{
			case BufferedImage.TYPE_3BYTE_BGR:
			{
				pixelFormat = BitmapData.PixelFormat24bppRgb;
				ComponentSampleModel componentSampleModel = (ComponentSampleModel)sampleModel;
				scanlineStride = componentSampleModel.getScanlineStride();
				break;
			}
			case BufferedImage.TYPE_INT_RGB:
			{
				pixelFormat = BitmapData.PixelFormat32bppRgb;
				SinglePixelPackedSampleModel singlePixelPackedSampleModel = (SinglePixelPackedSampleModel)sampleModel;
				scanlineStride = singlePixelPackedSampleModel.getScanlineStride();
				break;
			}
			case BufferedImage.TYPE_INT_ARGB:
			{
				pixelFormat = BitmapData.PixelFormat32bppArgb;
				SinglePixelPackedSampleModel singlePixelPackedSampleModel = (SinglePixelPackedSampleModel)sampleModel;
				scanlineStride = singlePixelPackedSampleModel.getScanlineStride();
				break;
			}
			default:
			{
				BufferedImage convertedImage;
				if(image.getColorModel().hasAlpha())
				{
					convertedImage = new BufferedImage(image.getWidth(), image.getHeight(), BufferedImage.TYPE_INT_ARGB);
					pixelFormat = BitmapData.PixelFormat32bppArgb;
					SinglePixelPackedSampleModel singlePixelPackedSampleModel = (SinglePixelPackedSampleModel)convertedImage.getSampleModel();
					scanlineStride = singlePixelPackedSampleModel.getScanlineStride();
				}
				else
				{
					convertedImage = new BufferedImage(image.getWidth(), image.getHeight(), BufferedImage.TYPE_3BYTE_BGR);
					pixelFormat = BitmapData.PixelFormat24bppRgb;
					ComponentSampleModel componentSampleModel = (ComponentSampleModel)convertedImage.getSampleModel();
					scanlineStride = componentSampleModel.getScanlineStride();
				}

				Graphics gfx = convertedImage.getGraphics();
				gfx.drawImage(image, 0, 0, null);
				gfx.dispose();
				image = convertedImage;
				break;
			}
		}

		DataBuffer buffer = image.getRaster().getDataBuffer();
		if(buffer instanceof DataBufferByte)
		{
			DataBufferByte dataBufferByte = (DataBufferByte)buffer;
			return new BitmapData(dataBufferByte.getData(), image.getWidth(), image.getHeight(), scanlineStride, pixelFormat);
		}
		else if(buffer instanceof DataBufferInt)
		{
			DataBufferInt dataBufferInt = (DataBufferInt)buffer;
			return new BitmapData(dataBufferInt.getData(), image.getWidth(), image.getHeight(), scanlineStride, pixelFormat);
		}
		else
		{
			throw new UnsupportedOperationException("TODO");
		}
	}
	
	public final Dimension getSize()
	{
		return this.m_size;
	}

	public final int getFrameCount()
	{
		return this.m_frameCount;
	}
}