package RealWork;

public class Editor {
    private String Content;
    private int FontSize;
    private String FontStyle;

    public Editor(String Content, String FontStyle, int FontSize) {
        this.Content = Content;
        this.FontStyle = FontStyle;
        this.FontSize = FontSize;
    }

    public Editor(String FontStyle, int FontSize) {
        this.FontStyle = FontStyle;
        this.FontSize = FontSize;
    }

    public void setContent(String content) {
        Content = content;
    }

    public void setFontSize(int fontSize) {
        FontSize = fontSize;
    }

    public void setFontStyle(String fontStyle) {
        FontStyle = fontStyle;
    }

    public String getContent() {
        return Content;
    }

    public int getFontSize() {
        return FontSize;
    }

    public String getFontStyle() {
        return FontStyle;
    }
}
