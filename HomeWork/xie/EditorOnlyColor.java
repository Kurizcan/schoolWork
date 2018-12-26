package RealWork;

import javax.swing.*;
import java.awt.*;

public class EditorOnlyColor extends Product {

    //定义编辑模板的组合，可以修改内容、字体大小、字体样式
    public void composition() {
        setLayout(new BorderLayout());

        String[] stlye = {"BLack","Blue","Red","Green"};

        JPanel panel = new JPanel();
        fontStyle = new JComboBox(stlye);

        panel.setLayout(new GridLayout(1, 1));
        panel.add(new JLabel("线段颜色:"));
        panel.add(fontStyle);
        add(panel, BorderLayout.CENTER);

        // create Ok and Cancel buttons that terminate the dialog
        okButton = new JButton("Ok");
        okButton.addActionListener(event -> {
            ok = true;
            dialog.setVisible(false);
        });

        JButton cancelButton = new JButton("Cancel");
        cancelButton.addActionListener(event -> dialog.setVisible(false));

        // add buttons to southern border

        JPanel buttonPanel = new JPanel();
        buttonPanel.add(okButton);
        buttonPanel.add(cancelButton);
        add(buttonPanel, BorderLayout.SOUTH);
    }

    public Editor getEditor()
    {
        return new Editor(fontStyle.getSelectedItem().toString(), 0);
    }
}
