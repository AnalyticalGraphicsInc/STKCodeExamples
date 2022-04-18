package agi.stk.plugin.graphics.rasterstream.basic;

public class BitmapData
{
	public static final int	PixelFormat8bppIndexed	= 0;
	public static final int	PixelFormat24bppRgb		= 1;
	public static final int	PixelFormat32bppRgb		= 2;
	public static final int	PixelFormat32bppArgb	= 3;

	private final byte[]	byteData;
	private final int[]		intData;
	private final int		width;
	private final int		height;
	private final int		stride;
	private final int		pixelFormat;

	public BitmapData(byte[] data, int width, int height, int stride, int pixelFormat)
	{
		this.byteData = data;
		this.intData = null;
		this.width = width;
		this.height = height;
		this.stride = stride;
		this.pixelFormat = pixelFormat;
	}

	public BitmapData(int[] data, int width, int height, int stride, int pixelFormat)
	{
		this.byteData = null;
		this.intData = data;
		this.width = width;
		this.height = height;
		this.stride = stride;
		this.pixelFormat = pixelFormat;
	}

	public byte[] getByteData()
	{
		return byteData;
	}

	public int[] getIntData()
	{
		return intData;
	}

	public int getHeight()
	{
		return height;
	}

	public int getPixelFormat()
	{
		return pixelFormat;
	}

	public int getStride()
	{
		return stride;
	}

	public int getWidth()
	{
		return width;
	}
}
