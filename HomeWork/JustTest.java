package HomeWork;


import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseMotionAdapter;
import java.awt.geom.Ellipse2D;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;

public class JustTest {
    public static void main(String[] args) {
        // 事件分配线程
        EventQueue.invokeLater(() -> {
            JFrame frame = new RectFrame();
            frame.setTitle("刘心悠");
            frame.setLocationRelativeTo(null);
            frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
            frame.setVisible(true);
        });
    }
}

class RectFrame extends JFrame {
    public RectFrame() {
        add(new DrawComponent(this));
        pack();
    }
}

class DrawComponent extends JComponent {
    private static final int DEFAULT_WIDTH = 800;
    private static final int DEFAULT_HEIGHT = 500;
    private double width, height;
    private Point2D startPoint, leftUpPoint, leftDownPoint, rightUpPoint, rightDownPoint;
    private Ellipse2D leftUpCorner, leftDownCorner, rightUpCorner, rightDownCorner;
    private Rectangle2D rectangle;
    private ArrayList<Ellipse2D> point2DS;      // 监控点
    private int stage;
    private int click;
    private EditorPanel dialog = null;
    private Frame ParentFrame;

    String message;
    Font f;

    private int stringStartX, stringStartY;

    public DrawComponent(Frame frame) {
        // 初始化界面
        point2DS = new ArrayList<>();
        startPoint = new Point2D.Double(100, 100);

        width = 100;
        height = 50;

        rectangle = new Rectangle2D.Double(startPoint.getX(), startPoint.getY(), width, height);
        message = "Hello, World!";
        f = new Font("Serif", Font.BOLD, 10);

        stringStartX = 200;
        stringStartY = 200;

        ParentFrame = frame;

        addMouseListener(new MouseHandler());
        addMouseMotionListener(new MouseMotionHandler());
    }

    public void paintComponent(Graphics g) {
        Graphics2D graphics2D = (Graphics2D) g;

        //f = new Font("Dialog", Font.PLAIN, 10);

        graphics2D.setFont(f);

        int stringMessageX = (int) (rectangle.getX());
        int stringMessageY = (int) (rectangle.getY() + f.getSize());

        graphics2D.drawString(message, stringMessageX, stringMessageY);
        graphics2D.setPaint(Color.red);

        int x = 100;
        int y = 100;

        //rectangle = new Rectangle2D.Double(x, y, bounds.getWidth(), bounds.getHeight());

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

    private Ellipse2D addPoints(Point2D p){
        Ellipse2D ellipse2D = new Ellipse2D.Double(p.getX() - 5, p.getY() - 5, 10, 10);
        point2DS.add(ellipse2D);
        return ellipse2D;
    }

    private void drawPoints(Ellipse2D ellipse, Graphics2D graphics) {
        graphics.setColor(Color.RED);
        graphics.fill(ellipse);
    }


    private Ellipse2D findPoints(Point2D p){
        for(Ellipse2D r : point2DS){
            if(r.contains(p)){
                return r;
            }
        }
        return null;
    }

    private class MouseHandler extends MouseAdapter {
        @Override
        public void mousePressed(MouseEvent event) {

        }

        @Override
        public void mouseClicked(MouseEvent e) {
            boolean IS_EnterY = e.getY() > leftUpPoint.getY() && e.getY() < leftUpPoint.getY() + rectangle.getHeight();
            boolean IS_EnterX = e.getX() > leftUpPoint.getX() && e.getX() < leftUpPoint.getX() + rectangle.getWidth();

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

    private class MouseMotionHandler extends MouseMotionAdapter {
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

    public void isShowDialog() {
        if (dialog == null) dialog = new EditorPanel();

        dialog.setEditor(message);

        // pop up dialog
        if (dialog.showDialog(ParentFrame, "属性编辑"))
        {
            // if accepted, retrieve user input
            Editor editor = dialog.getEditor();
            message = editor.getContent();
            if (editor.getFontStyle().equals("PLAIN"))
                f = new Font("Serif", Font.PLAIN, editor.getFontSize());
            else if (editor.getFontStyle().equals("BOLD"))
                f = new Font("Serif", Font.BOLD, editor.getFontSize());
            else if (editor.getFontStyle().equals("ITALIC"))
                f = new Font("Serif", Font.ITALIC, editor.getFontSize());
            else if (editor.getFontStyle().equals("CENTER_BASELINE"))
                f = new Font("Serif", Font.CENTER_BASELINE, editor.getFontSize());

            repaint();
        }
    }

    public Dimension getPreferredSize() {
        return new Dimension(DEFAULT_WIDTH, DEFAULT_HEIGHT);
    }
}