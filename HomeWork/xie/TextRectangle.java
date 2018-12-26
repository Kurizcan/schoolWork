package RealWork;

import java.awt.*;
import java.awt.geom.Point2D;
import java.awt.geom.Rectangle2D;
import java.util.ArrayList;

public class TextRectangle extends AbstractRectangle {

    public TextRectangle(Frame frame) {
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
    public void isShowDialog() {
        if (dialog == null) dialog = creator.createProduct(EditorProductWithAll.class);

        dialog.composition();
        dialog.setEditor(message);

        // pop up dialog
        if (dialog.showDialog(frame, "属性编辑"))
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
}
