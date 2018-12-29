package RealWork;

import java.awt.*;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;
import java.util.Hashtable;

public class ExampleRectangle extends AbstractRectangle implements Observer{
    // 定义油层颜色
    public final static int Yellow = 1;
    public int HasYellow = 0;
    public final static int Red = 2;
    public int HasRed = 0;
    public final static int Green = 3;
    public int HasGreen = 0;
    public final static int Blue = 4;
    public int HasBlue = 0;

    public ExampleRectangle(Frame frame) {
        // 初始化界面
        point2DS = new ArrayList<>();
        this.frame = frame;

        addMouseListener(new MouseHandler());
        addMouseMotionListener(new MouseMotionHandler());
    }

    protected void setInit(double x, double y, double width, double height) {
        startPoint = new Point2D.Double(x, y);
        rectangle = new Rectangle2D.Double(startPoint.getX(), startPoint.getY(), width, height);
        message = " ";
        f = new Font("Serif", Font.BOLD, 20);
    }

    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);
        Graphics2D graphics2D = (Graphics2D) g;
        if (HasGreen == 1) {
            Rectangle2D rectangle_Green = new Rectangle2D.Double(rectangle.getX() + 20, rectangle.getY() + 25, 10, 10);
            graphics2D.setColor(Color.green);
            graphics2D.fill(rectangle_Green);
        }
        if (HasBlue == 1) {
            Rectangle2D rectangle_Blue = new Rectangle2D.Double(rectangle.getX() + 45, rectangle.getY() + 25, 10, 10);
            graphics2D.setColor(Color.blue);
            graphics2D.fill(rectangle_Blue);
        }
        if (HasRed == 1) {
            Rectangle2D rectangle_Red = new Rectangle2D.Double(rectangle.getX() + 20, rectangle.getY() + 55, 10, 10);
            graphics2D.setColor(Color.red);
            graphics2D.fill(rectangle_Red);
        }
        if (HasYellow == 1) {
            Rectangle2D rectangle_Yellow = new Rectangle2D.Double(rectangle.getX() + 45, rectangle.getY() + 55, 10, 10);
            graphics2D.setColor(Color.yellow);
            graphics2D.fill(rectangle_Yellow);
        }

    }

    public void update(double x, double y, int sign) {

    }

    public void update_get(Hashtable hashtable, int start, int end) {
        HasYellow = HasRed = HasBlue = HasGreen = 0;
        if (hashtable.containsValue(Yellow))
            HasYellow = 1;
        if (hashtable.containsValue(Red))
            HasRed = 1;
        if (hashtable.containsValue(Blue))
            HasBlue = 1;
        if (hashtable.containsValue(Green))
            HasGreen = 1;
        repaint();
    }

    @Override
    public void isShowDialog() {

    }
}
