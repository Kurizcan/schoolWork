package RealWork;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseMotionAdapter;
import java.awt.geom.Ellipse2D;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;

public abstract class AbstractRectangle extends JComponent {
    protected static final int DEFAULT_WIDTH = 1000;
    protected static final int DEFAULT_HEIGHT = 800;
    protected double width, height;
    protected Point2D startPoint, leftUpPoint, leftDownPoint, rightUpPoint, rightDownPoint;
    protected Ellipse2D leftUpCorner, leftDownCorner, rightUpCorner, rightDownCorner;
    protected Rectangle2D rectangle;
    protected ArrayList<Ellipse2D> point2DS;      // 监控点
    protected int stage;
    protected int click;
    String message;
    Font f;
    protected Frame frame;
    protected Product dialog = null;
    protected Creator creator = new EditorCreater();
    protected double x;
    protected double y;

    protected abstract void setInit(double x, double y, double width, double height);

    public abstract void isShowDialog();        // 编辑属性框

    public Dimension getPreferredSize() {
        return new Dimension(DEFAULT_WIDTH, DEFAULT_HEIGHT);
    }

    protected Ellipse2D addPoints(Point2D p){
        Ellipse2D ellipse2D = new Ellipse2D.Double(p.getX() - 5, p.getY() - 5, 10, 10);
        point2DS.add(ellipse2D);
        return ellipse2D;
    }

    protected void drawPoints(Ellipse2D ellipse, Graphics2D graphics) {
        graphics.setColor(Color.RED);
        graphics.fill(ellipse);
    }


    protected Ellipse2D findPoints(Point2D p){
        for(Ellipse2D r : point2DS){
            if(r.contains(p)){
                return r;
            }
        }
        return null;
    }

    protected class MouseHandler extends MouseAdapter {
        @Override
        public void mousePressed(MouseEvent event) {

        }

        @Override
        public void mouseClicked(MouseEvent e) {
            boolean IS_EnterY = e.getY() > leftUpPoint.getY() && e.getY() < leftUpPoint.getY() + rectangle.getHeight();
            boolean IS_EnterX = e.getX() > leftUpPoint.getX() && e.getX() < leftUpPoint.getX() + rectangle.getWidth();

            System.out.println("x: " + e.getX() + " y: " + e.getY());

            if (IS_EnterX && IS_EnterY) {
                if (e.getClickCount() == 1) {
                    click = 1;
                    repaint();
                }
                else if (e.getClickCount() == 2) {
                    isShowDialog();
                }
            }
            else {
                click = 0;
                repaint();
            }


        }
    }

    protected class MouseMotionHandler extends MouseMotionAdapter {
        @Override
        public void mouseMoved(MouseEvent e) {
            boolean IS_EnterY = e.getY() > leftUpPoint.getY() && e.getY() < leftUpPoint.getY() + rectangle.getHeight();
            boolean IS_EnterX = e.getX() > leftUpPoint.getX() && e.getX() < leftUpPoint.getX() + rectangle.getWidth();
            if(findPoints(e.getPoint()) != null){
                if (leftUpCorner.contains(e.getPoint()))
                    stage = 1;
                else if (leftDownCorner.contains(e.getPoint()))
                    stage = 3;
                else if (rightUpCorner.contains(e.getPoint()))
                    stage = 4;
                else if (rightDownCorner.contains(e.getPoint()))
                    stage = 5;
                setCursor(Cursor.getPredefinedCursor(Cursor.CROSSHAIR_CURSOR));
                // 改变框的大小
            }
            else if (IS_EnterX && IS_EnterY){
                stage = 2;
                setCursor(Cursor.getPredefinedCursor(Cursor.MOVE_CURSOR));
                // 移动框
            }
            else{
                setCursor(Cursor.getDefaultCursor());
            }
        }

        @Override
        public void mouseDragged(MouseEvent e) {
            if (stage == 2) {
                int x = e.getX();
                int y = e.getY();
                rectangle.setFrame(x - rectangle.getWidth() / 2,  y - rectangle.getHeight() / 2, rectangle.getWidth(), rectangle.getHeight());
            }
            else if(stage == 1)
                rectangle.setFrameFromDiagonal(e.getPoint(), rightDownPoint);
            else if (stage == 3)
                rectangle.setFrameFromDiagonal(rightUpPoint, e.getPoint());
            else if (stage == 4)
                rectangle.setFrameFromDiagonal(leftDownPoint, e.getPoint());
            else if (stage == 5)
                rectangle.setFrameFromDiagonal(leftUpPoint, e.getPoint());
            repaint();
        }
    }

    public void paintComponent(Graphics g) {
        Graphics2D graphics2D = (Graphics2D) g;

        graphics2D.setFont(f);

        int stringMessageX = (int) (rectangle.getX());
        int stringMessageY = (int) (rectangle.getY() + f.getSize());

        graphics2D.drawString(message, stringMessageX, stringMessageY);
        graphics2D.setPaint(Color.red);

        point2DS.clear();
        leftUpPoint = new Point2D.Double(rectangle.getX(), rectangle.getY());
        leftDownPoint = new Point2D.Double(leftUpPoint.getX(), leftUpPoint.getY() + rectangle.getHeight());
        rightUpPoint = new Point2D.Double(leftUpPoint.getX() + rectangle.getWidth(), leftUpPoint.getY());
        rightDownPoint = new Point2D.Double(leftUpPoint.getX() + rectangle.getWidth(), leftUpPoint.getY() + rectangle.getHeight());

        graphics2D.draw(rectangle);

        leftUpCorner = addPoints(leftUpPoint);
        leftDownCorner = addPoints(leftDownPoint);
        rightDownCorner = addPoints(rightDownPoint);
        rightUpCorner = addPoints(rightUpPoint);
        if (click == 1) {
            drawPoints(leftUpCorner, graphics2D);
            drawPoints(leftDownCorner, graphics2D);
            drawPoints(rightDownCorner, graphics2D);
            drawPoints(rightUpCorner, graphics2D);
        }
    }

    public void setMessage(String message) {
        this.message = message;
    }

}
