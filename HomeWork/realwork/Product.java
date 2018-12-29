package RealWork;

import javax.swing.*;
import java.awt.*;

public abstract class Product extends JPanel {
    protected JTextField content;
    protected JButton okButton;
    protected boolean ok;
    protected JDialog dialog;
    protected JComboBox fontStyle;		//定义下拉框
    protected JComboBox fontSize;

    //编辑器的组合方法
    public abstract void composition();

    public abstract Editor getEditor();

    public void setEditor(String text) {
        content.setText(text);
    }

    public boolean showDialog(Component parent, String title)
    {
        ok = false;

        // locate the owner frame

        Frame owner = null;
        if (parent instanceof Frame)
            owner = (Frame) parent;
        else
            owner = (Frame) SwingUtilities.getAncestorOfClass(Frame.class, parent);

        // if first time, or if owner has changed, make new dialog

        if (dialog == null || dialog.getOwner() != owner)
        {
            dialog = new JDialog(owner, true);
            dialog.add(this);
            dialog.getRootPane().setDefaultButton(okButton);
            dialog.pack();
        }

        // set title and show dialog

        dialog.setTitle(title);
        dialog.setLocation( owner.getX() + owner.getWidth() / 2, owner.getY() + owner.getHeight() / 2);
        dialog.setVisible(true);
        return ok;
    }
}
