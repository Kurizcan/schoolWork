package RealWork;

import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseMotionAdapter;
import java.awt.geom.Ellipse2D;
import java.awt.geom.Line2D;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.Vector;

public class GreenRectangle extends AbstractRectangle implements ObserverSubject {
    // 定义油层颜色
    public final static int Yellow = 1;
    public final static int Red = 2;
    public final static int Green = 3;
    public final static int Blue = 4;

    //定义一个观察者数组
    private Vector<Observer> obsVector = new Vector<Observer>();
    // 获取数据库的油层信息
    public dbConnector dbConnector = new dbConnector();
    public Hashtable oil;
    Ellipse2D ellipse2D;
    Color color = Color.BLACK;

    protected void setInit(double x, double y, double width, double height) {
        startPoint = new Point2D.Double(x, y);
        rectangle = new Rectangle2D.Double(startPoint.getX(), startPoint.getY(), width, height);
        message = " ";
        f = new Font("Serif", Font.BOLD, 20);
    }

    public GreenRectangle(Frame frame) {
        // 初始化界面
        point2DS = new ArrayList<>();
        this.frame = frame;

        addMouseListener(new MouseHandler());
        addMouseMotionListener(new MouseMotionHandler_Green());
    }

    public void SearchOil(int a, int b) {
        oil = dbConnector.GetOiled(a, b);
    }

    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);
        Point2D start = new Point2D.Double(rectangle.getX() + rectangle.getWidth() / 3, rectangle.getY());
        Point2D end = new Point2D.Double(rectangle.getX() + rectangle.getWidth() / 7, rectangle.getY() + rectangle.getHeight());
        Line2D line = new Line2D.Double(start, end);

        Graphics2D graphics2D = (Graphics2D) g;
        graphics2D.setPaint(color);
        graphics2D.draw(line);

        // 获取直线函数

        double k = (start.getY() - end.getY()) / (start.getX() - end.getX());
        double b = start.getY() - k * start.getX();

        SearchOil((int) start.getY(), (int) end.getY());

        notifyObservers(oil, (int) start.getY(), (int) end.getY());

        Enumeration e = oil.keys();
        while (e.hasMoreElements()){
            int valueY = (int)e.nextElement();
            double valueX = (valueY - b) / k;
            ellipse2D = new Ellipse2D.Double(valueX, valueY, 5, 5);
            int color = (int)oil.get(valueY);
            if (color == Yellow)
                graphics2D.setColor(Color.yellow);
            else if (color == Red)
                graphics2D.setColor(Color.RED);
            else if (color == Blue)
                graphics2D.setColor(Color.blue);
            else if (color == Green)
                graphics2D.setColor(Color.GREEN);
            graphics2D.fill(ellipse2D);
        }
    }

    //增加一个观察者
    public void addObserver(Observer o){
        this.obsVector.add(o);
    }
    //删除一个观察者
    public void delObserver(Observer o){
        this.obsVector.remove(o);
    }
    //通知所有观察者
    public void notifyObservers(double x, double y, int sign){
        for(Observer o:this.obsVector){
            o.update(x, y, sign);
        }
    }

    public void notifyObservers(Hashtable hashtable, int start, int end) {
        for(Observer o:this.obsVector){
            o.update_get(hashtable, start, end);
        }
    }

    protected class MouseMotionHandler_Green extends MouseMotionHandler {
        @Override
        public void mouseMoved(MouseEvent e) {
            super.mouseMoved(e);
        }

        @Override
        public void mouseDragged(MouseEvent e) {
            double lastX = 0;
            double lastY = 0;
            double changeX = 0;
            double changeY = 0;
            if (stage == 2) {
                int x = e.getX();
                int y = e.getY();
                rectangle.setFrame(x - rectangle.getWidth() / 2,  y - rectangle.getHeight() / 2, rectangle.getWidth(), rectangle.getHeight());
                notifyObservers(rectangle.getX(), rectangle.getY(), stage);
            }
            else if (stage == 0) {

            }
            else {
                if(stage == 1) {
                    lastX = leftUpPoint.getX();
                    lastY = leftUpPoint.getY();
                    rectangle.setFrameFromDiagonal(rightDownPoint, e.getPoint());
                    changeX = rectangle.getX() - lastX;
                    changeY = rectangle.getY() - lastY;
                    notifyObservers(changeX, changeY, stage);

                }
                else if (stage == 3) {
                    lastX = leftDownPoint.getX();
                    lastY = leftDownPoint.getY();
                    rectangle.setFrameFromDiagonal(rightUpPoint, e.getPoint());
                    changeX = rectangle.getX() - lastX;
                    changeY = rectangle.getY() + rectangle.getHeight() - lastY;
                    notifyObservers(changeX, changeY, stage);
                }
                else if (stage == 4) {
                    lastY = rightUpPoint.getY();
                    rectangle.setFrameFromDiagonal(leftDownPoint, e.getPoint());
                    changeY = rectangle.getY() - lastY;
                    notifyObservers(changeX, changeY, stage);
                }
                else if (stage == 5) {
                    lastY = rightDownPoint.getY();
                    rectangle.setFrameFromDiagonal(leftUpPoint, e.getPoint());
                    changeY = rectangle.getY() + rectangle.getHeight() - lastY;
                    notifyObservers(changeX, changeY, stage);
                }
            }
            repaint();
        }
    }

    @Override
    public void isShowDialog() {
        if (dialog == null) dialog = creator.createProduct(EditorOnlyColor.class);

        dialog.composition();

        // pop up dialog
        if (dialog.showDialog(frame, "属性编辑"))
        {
            // if accepted, retrieve user input
            Editor editor = dialog.getEditor();
            if (editor.getFontStyle().equals("BLack"))
                color = Color.BLACK;
            else if (editor.getFontStyle().equals("Blue"))
                color = Color.BLUE;
            else if (editor.getFontStyle().equals("Red"))
                color = Color.RED;
            else if (editor.getFontStyle().equals("Green"))
                color = Color.GREEN;
            repaint();
        }
    }
}

