package RealWork;

import java.awt.*;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;
import java.util.Hashtable;

public class DepthRectangle extends AbstractRectangle implements Observer{
    public double apartX;
    public double apartY;
    public String name;

    public DepthRectangle(Frame frame) {
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
        f = new Font("Serif", Font.BOLD, 15);
    }

    public void setApartAndName(double x, double y, String name) {
        this.apartX = x;
        this.apartY = y;
        this.name = name;
    }

    public void update(double x, double y, int sign) {

        //System.out.println("收到信息");
        if (sign == 2)
            rectangle.setFrame(x + apartX, y + apartY, rectangle.getWidth(), rectangle.getHeight());
        else if(sign == 1) {
            // 左上角被移动
            if (name.equals("depth1"))
                rectangle.setFrame(x + leftUpPoint.getX(), y + leftUpPoint.getY(), rectangle.getWidth(), rectangle.getHeight());
            else if (name.equals("depth2")) {
                rectangle.setFrame(x + leftUpPoint.getX(), leftUpPoint.getY(), rectangle.getWidth(), rectangle.getHeight());
                apartY -= y;
            }
        }
        else if (sign == 3) {
            // 左下角被移动
            if (name.equals("depth1"))
                rectangle.setFrame(x + leftUpPoint.getX(), leftUpPoint.getY(), rectangle.getWidth(), rectangle.getHeight());
            else if (name.equals("depth2")) {
                rectangle.setFrame(x + leftUpPoint.getX(), y + leftUpPoint.getY(), rectangle.getWidth(), rectangle.getHeight());
                apartY += y;
            }
        }
        else if (sign == 4) {
            // 右上角被移动
            if (name.equals("depth1"))
                rectangle.setFrame(leftUpPoint.getX(), leftUpPoint.getY() + y, rectangle.getWidth(), rectangle.getHeight());
            else
                apartY -= y;
        }
        else if (sign == 5) {
            // 右下角被移动
            if (name.equals("depth2")){
                rectangle.setFrame(leftUpPoint.getX(), leftUpPoint.getY() + y, rectangle.getWidth(), rectangle.getHeight());
                apartY += y;
            }
        }
        repaint();
    }

    public void update_get(Hashtable hashtable, int start, int end) {
         if (name.equals("depth1"))
             message = String.valueOf(start) + " m";
         else if (name.equals("depth2"))
             message = String.valueOf(end) + " m";
        repaint();
    }

    @Override
    public void isShowDialog() {
        if (dialog == null) dialog = creator.createProduct(EditorProductNoContent.class);

        dialog.composition();

        // pop up dialog
        if (dialog.showDialog(frame, "属性编辑"))
        {
            // if accepted, retrieve user input
            Editor editor = dialog.getEditor();
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
}
