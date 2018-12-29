package RealWork;

import javax.swing.*;
import java.awt.*;

public class Main {
    public static void main(String args[]) {
        // 事件分配线程
        EventQueue.invokeLater(() -> {
            JFrame frame = new MainFrame();
            frame.setTitle("刘心悠");
            frame.setLocationRelativeTo(null);
            frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
            frame.setVisible(true);
        });
    }
}

class MainFrame extends JFrame {
    AbstractFactory factory = new Factory();
    public MainFrame() {
        TextRectangle exampleTitle = factory.createTextRectangle(this);
        exampleTitle.setBounds(700, 0, 300, 420);
        exampleTitle.setInit(20, 370, 60, 40);
        exampleTitle.setMessage("图例");
        add(exampleTitle);

        ExampleRectangle exampleRectangle = factory.createExampleRectangle(this);
        exampleRectangle.setBounds(700, 420, 300, 380);
        exampleRectangle.setInit(20, 20, 85, 85);
        add(exampleRectangle);

        DepthRectangle depthRectangle1 = factory.createDepthRectangle(this);
        depthRectangle1.setBounds(0, 0, 300, 400);
        depthRectangle1.setInit(150, 180, 80, 50);
        depthRectangle1.setMessage("深度1：");
        depthRectangle1.setApartAndName(100, 150, "depth1");
        add(depthRectangle1);

        GreenRectangle greenRectangle = factory.createGreenRectangle(this);
        greenRectangle.setBounds(300, 150, 400, 650);
        greenRectangle.setInit(50, 30, 200, 350);
        add(greenRectangle);

        TextRectangle title = factory.createTextRectangle(this);
        title.setBounds(300, 0, 400, 150);
        title.setInit(40, 50, 150, 50);
        title.setMessage("标题：");
        add(title);

        DepthRectangle depthRectangle2 = factory.createDepthRectangle(this);
        depthRectangle2.setBounds(0, 400, 300, 400);
        depthRectangle2.setInit(150, 480, 80, 50);
        depthRectangle2.setMessage("深度2：");
        depthRectangle2.setApartAndName(100, 450, "depth2");
        add(depthRectangle2);

        greenRectangle.addObserver(depthRectangle1);
        greenRectangle.addObserver(depthRectangle2);
        greenRectangle.addObserver(exampleRectangle);

        pack();
    }
}
