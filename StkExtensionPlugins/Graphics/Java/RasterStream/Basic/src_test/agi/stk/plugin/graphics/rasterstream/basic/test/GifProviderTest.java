package agi.stk.plugin.graphics.rasterstream.basic.test;

// Java API
import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.awt.image.*;
import java.io.*;

// AGI Java API
import agi.core.*;
import agi.stk.plugin.graphics.rasterstream.basic.*;

class GifProviderTestRunnable
implements Runnable
{
	public void run()
	{
		try
		{
			GifProviderTest t = null;
			t = new GifProviderTest();
			t.setVisible(true);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
}

public class GifProviderTest
extends JFrame
implements ActionListener
{
	private static final long	serialVersionUID	= 1L;

	private GifProvider				m_GifProvider;

	private ImageDrawingComponent	m_IDC;

	private JButton					m_NextFrameSaveByteArrayJButton;

	private JButton					m_PreviousFrameJButton;
	private JButton					m_PlayBackwardJButton;
	private JButton					m_PauseJButton;
	private JButton					m_PlayForwardJButton;
	private JButton					m_NextFrameJButton;

	private PlayThread				m_PlayThread;

	public static void main(String[] args)
	{
		try
		{
			GifProviderTestRunnable r = null;
			r = new GifProviderTestRunnable();
			SwingUtilities.invokeLater(r);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public GifProviderTest()
	throws Exception
	{
		Container c = this.getContentPane();
		c.setLayout(new BorderLayout());

		this.m_IDC = new ImageDrawingComponent();
		c.add(this.m_IDC, BorderLayout.CENTER);

		this.m_NextFrameSaveByteArrayJButton = new JButton();
		this.m_NextFrameSaveByteArrayJButton.setText("Next Frame Save Byte Array");
		this.m_NextFrameSaveByteArrayJButton.addActionListener(this);
		c.add(this.m_NextFrameSaveByteArrayJButton, BorderLayout.NORTH);

		JPanel buttonPanel = new JPanel();
		buttonPanel.setLayout(new GridLayout(1, 5));

		this.m_PreviousFrameJButton = new JButton();
		this.m_PreviousFrameJButton.setText("Previous Frame");
		this.m_PreviousFrameJButton.addActionListener(this);
		buttonPanel.add(this.m_PreviousFrameJButton);

		this.m_PlayBackwardJButton = new JButton();
		this.m_PlayBackwardJButton.setText("<< Play Backward");
		this.m_PlayBackwardJButton.addActionListener(this);
		buttonPanel.add(this.m_PlayBackwardJButton);

		this.m_PauseJButton = new JButton();
		this.m_PauseJButton.setText("Pause ||");
		this.m_PauseJButton.addActionListener(this);
		buttonPanel.add(this.m_PauseJButton);

		this.m_PlayForwardJButton = new JButton();
		this.m_PlayForwardJButton.setText("Play Forward >>");
		this.m_PlayForwardJButton.addActionListener(this);
		buttonPanel.add(this.m_PlayForwardJButton);

		this.m_NextFrameJButton = new JButton();
		this.m_NextFrameJButton.setText("Next Frame");
		this.m_NextFrameJButton.addActionListener(this);
		buttonPanel.add(this.m_NextFrameJButton);

		c.add(buttonPanel, BorderLayout.SOUTH);

		this.setSize(new Dimension(700, 400));
		this.setLocationRelativeTo(null);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		SwingUtilities.invokeLater(new Runnable()
		{
			public void run()
			{
				try
				{
					m_GifProvider = createDefaultGifProvider();
					m_IDC.drawBufferedImage(m_GifProvider.nextFrame());
				}
				catch(Throwable t)
				{
					t.printStackTrace();
				}
			}
		});
	}

	private GifProvider createDefaultGifProvider()
	throws AgCoreException
	{
		String fileSep = AgSystemPropertiesHelper.getFileSeparator();
		String userDir = AgSystemPropertiesHelper.getUserDir();

		StringBuilder sb = new StringBuilder();
		sb.append(userDir);
		sb.append(fileSep);
		sb.append("..");
		sb.append(fileSep);
		sb.append("..");
		sb.append(fileSep);
		sb.append("..");
		sb.append(fileSep);
		sb.append("..");
		sb.append(fileSep);
		sb.append("..");
		sb.append(fileSep);
		sb.append("CustomApplications");
		sb.append(fileSep);
		sb.append("Data");
		sb.append(fileSep);
		sb.append("HowTo");
		sb.append(fileSep);
		sb.append("Textures");
		sb.append(fileSep);
		sb.append("SpinSat_256.gif");

		return new GifProvider(sb.toString());
	}

	public void actionPerformed(ActionEvent e)
	{
		try
		{
			Object src = e.getSource();
			if(src.equals(this.m_PreviousFrameJButton))
			{
				stepBackward();
			}
			else if(src.equals(this.m_PlayBackwardJButton))
			{
				this.m_PlayThread = new PlayThread();
				this.m_PlayThread.setDirection(PlayThread.PLAY_BACKWARD);
				this.m_PlayThread.start();
			}
			else if(src.equals(this.m_PauseJButton))
			{
				this.m_PlayThread.interrupt();
				this.m_PlayThread = null;
			}
			else if(src.equals(this.m_PlayForwardJButton))
			{
				this.m_PlayThread = new PlayThread();
				this.m_PlayThread.setDirection(PlayThread.PLAY_FORWARD);
				this.m_PlayThread.start();
			}
			else if(src.equals(this.m_NextFrameJButton))
			{
				stepForward();
			}
			else if(src.equals(this.m_NextFrameSaveByteArrayJButton))
			{
				stepForwardAndSaveByteArray();
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void stepBackward()
	throws Exception
	{
		BufferedImage bi = null;
		bi = this.m_GifProvider.previousFrame();
		this.m_IDC.drawBufferedImage(bi);
	}

	private void stepForward()
	throws Exception
	{
		BufferedImage bi = null;
		bi = this.m_GifProvider.nextFrame();
		this.m_IDC.drawBufferedImage(bi);
	}

	private void stepForwardAndSaveByteArray()
	throws Exception
	{
		int[] values = this.m_GifProvider.nextFrameAsBitmapData().getIntData();
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		DataOutputStream dos = new DataOutputStream(baos);
		for(int i = 0; i < values.length; ++i)
		{
			dos.writeInt(values[i]);
		}

		byte[] ba = baos.toByteArray();

		long time = System.currentTimeMillis();
		String userDir = AgSystemPropertiesHelper.getUserDir();
		String fileSep = AgSystemPropertiesHelper.getFileSeparator();
		String filePath = userDir + fileSep + time + ".gif";

		OutputStream out = new FileOutputStream(filePath);
		out.write(ba);
		out.close();
	}

	class PlayThread
	extends Thread
	{
		public static final int	PLAY_FORWARD	= 0;
		public static final int	PLAY_BACKWARD	= 1;
		private int				m_direction;

		public void setDirection(int direction)
		{
			this.m_direction = direction;
		}

		public void run()
		{
			try
			{
				while(true)
				{
					Thread.sleep(100);

					if(this.m_direction == PLAY_FORWARD)
					{
						SwingUtilities.invokeAndWait(new StepForwardRunnable());
					}
					else if(this.m_direction == PLAY_BACKWARD)
					{
						SwingUtilities.invokeAndWait(new StepBackwardRunnable());
					}
				}
			}
			catch(Throwable t)
			{
				// t.printStackTrace();
			}
		}
	}

	class StepBackwardRunnable
	implements Runnable
	{
		public void run()
		{
			try
			{
				stepBackward();
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	class StepForwardRunnable
	implements Runnable
	{
		public void run()
		{
			try
			{
				stepForward();
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}
}

class ImageDrawingComponent
extends Component
{
	private static final long	serialVersionUID	= 1L;

	private BufferedImage		m_bi;

	public ImageDrawingComponent()
	{
	}

	public void drawBufferedImage(BufferedImage bi)
	{
		this.m_bi = bi;
		this.repaint();
	}

	public Dimension getPreferredSize()
	{
		return new Dimension(this.m_bi.getWidth(), this.m_bi.getHeight());
	}

	public void paint(Graphics g)
	{
		int biw = this.m_bi.getWidth();
		int bih = this.m_bi.getHeight();

		int w = this.getWidth();
		int h = this.getHeight();

		int x = w / 2 - biw / 2;
		int y = h / 2 - bih / 2;

		Graphics2D g2 = (Graphics2D)g;
		g2.drawImage(this.m_bi, x, y, null);
	}
}