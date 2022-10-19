// Java API
import java.awt.*;
import java.awt.event.*;
import java.awt.image.*;
import javax.swing.*;
import javax.swing.border.*;

// Sample API
import agi.stk.plugin.graphics.cigo.osm.*;

class OpenStreetMapTileViewerTestRunnable
implements Runnable
{
	public void run()
	{
		try
		{
			OpenStreetMapTileViewerTest t = null;
			t = new OpenStreetMapTileViewerTest();
			t.setVisible(true);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
}

public class OpenStreetMapTileViewerTest
extends JFrame
implements ActionListener
{
	private static final long	serialVersionUID	= 1L;

	private ImageDrawingComponent	m_IDC;

	private JLabel m_ZoomJLabel;
	private JTextField m_ZoomJTextField;
	
	private JLabel m_XJLabel;
	private JTextField m_XJTextField;

	private JLabel m_YJLabel;
	private JTextField m_YJTextField;

	private JButton m_DisplayTileJButton;
	
	public static void main(String[] args)
	{
		try
		{
			OpenStreetMapTileViewerTestRunnable r = null;
			r = new OpenStreetMapTileViewerTestRunnable();
			SwingUtilities.invokeLater(r);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	public OpenStreetMapTileViewerTest()
	throws Exception
	{
		this.setTitle(OpenStreetMapTileViewerTest.class.getName());
		
		Container c = this.getContentPane();
		c.setLayout(new BorderLayout());

		this.m_IDC = new ImageDrawingComponent();
		c.add(this.m_IDC, BorderLayout.CENTER);

		JPanel tileInputPanel = new JPanel();
		tileInputPanel.setLayout(new GridLayout(1, 7));
		c.add(tileInputPanel, BorderLayout.SOUTH);

		final int zoom = 0;
		final int x = 0;
		final int y = 0;
		
		this.m_ZoomJLabel = new JLabel();
		this.m_ZoomJLabel.setText("Zoom:");
		this.m_ZoomJLabel.setBorder(new EmptyBorder(0,10,0,10));
		this.m_ZoomJLabel.setHorizontalAlignment(SwingConstants.RIGHT);
		tileInputPanel.add(this.m_ZoomJLabel);
		
		this.m_ZoomJTextField = new JTextField();
		this.m_ZoomJTextField.setText(Integer.toString(zoom));
		this.m_ZoomJTextField.setMargin(new Insets(0,10,0,10));
		this.m_ZoomJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		tileInputPanel.add(this.m_ZoomJTextField);

		this.m_XJLabel = new JLabel();
		this.m_XJLabel.setText("X:");
		this.m_XJLabel.setBorder(new EmptyBorder(0,10,0,10));
		this.m_XJLabel.setHorizontalAlignment(SwingConstants.RIGHT);
		tileInputPanel.add(this.m_XJLabel);
		
		this.m_XJTextField = new JTextField();
		this.m_XJTextField.setText(Integer.toString(x));
		this.m_XJTextField.setMargin(new Insets(0,10,0,10));
		this.m_XJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		tileInputPanel.add(this.m_XJTextField);

		this.m_YJLabel = new JLabel();
		this.m_YJLabel.setText("Y:");
		this.m_YJLabel.setBorder(new EmptyBorder(0,10,0,10));
		this.m_YJLabel.setHorizontalAlignment(SwingConstants.RIGHT);
		tileInputPanel.add(this.m_YJLabel);
		
		this.m_YJTextField = new JTextField();
		this.m_YJTextField.setText(Integer.toString(y));
		this.m_YJTextField.setMargin(new Insets(0,10,0,10));
		this.m_YJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		tileInputPanel.add(this.m_YJTextField);

		this.m_DisplayTileJButton = new JButton();
		this.m_DisplayTileJButton.setText("Display Tile");
		this.m_DisplayTileJButton.addActionListener(this);
		tileInputPanel.add(this.m_DisplayTileJButton);
		
		this.setSize(new Dimension(700, 500));
		this.setLocationRelativeTo(null);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		
		SwingUtilities.invokeLater(new Runnable()
		{
			public void run()
			{
				try
				{
					String tileData = zoom + "/" + x + "/" + y;
					displayTile(tileData);
				}
				catch(Throwable t)
				{
					t.printStackTrace();
				}
			}
		});
	}

	public void actionPerformed(ActionEvent e)
	{
		try
		{
			Object src = e.getSource();
			if(src.equals(this.m_DisplayTileJButton))
			{
				String zoom = this.m_ZoomJTextField.getText();
				String x = this.m_XJTextField.getText();
				String y = this.m_YJTextField.getText();
				
				String tileData = zoom + "/" + x +"/" + y;
				
				displayTile(tileData);
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void displayTile(String tileData)
	throws Exception
	{
		BufferedImage bi = null;
		bi = OpenStreetMapWebSvcClient.writeTileToBufferedImage(tileData);
		this.m_IDC.drawBufferedImage(bi);
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
		if(this.m_bi != null)
		{
			int w = this.getWidth();
			int h = this.getHeight();
	
			Graphics2D g2 = (Graphics2D)g;
			g2.drawImage(this.m_bi, 0, 0, w, h, null);

			// or
			
			//int biw = this.m_bi.getWidth();
			//int bih = this.m_bi.getHeight();
	
			//int x = w / 2 - biw / 2;
			//int y = h / 2 - bih / 2;
	
			//g2.drawImage(this.m_bi, x, y, null);
		}
	}
}