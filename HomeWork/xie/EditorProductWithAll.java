package RealWork;

import javax.swing.*;
import java.awt.*;

public class EditorProductWithAll extends Product {

    //定义编辑模板的组合，可以修改内容、字体大小、字体样式
    public void composition() {
        setLayout(new BorderLayout());

        String[] stlye = {"PLAIN","BOLD","ITALIC","CENTER_BASELINE"};
        String[] size = {"10","15","20","25"};

        JPanel panel = new JPanel();
        fontStyle = new JComboBox(stlye);
        fontSize = new JComboBox(size);

        panel.setLayout(new GridLayout(3, 3));
        panel.add(new JLabel("内容:"));
        panel.add(content = new JTextField(""));
        panel.add(new JLabel("字体样式:"));
        panel.add(fontStyle);
        panel.add(new JLabel("字体大小:"));
        panel.add(fontSize);
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
        return new Editor(content.getText(), fontStyle.getSelectedItem().toString(), Integer.parseInt(fontSize.getSelectedItem().toString()));
    }

}
