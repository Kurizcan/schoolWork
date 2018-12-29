package RealWork;

import java.awt.*;

public abstract class AbstractFactory {
    public abstract TextRectangle createTextRectangle(Frame frame);
    public abstract DepthRectangle createDepthRectangle(Frame frame);
    public abstract GreenRectangle createGreenRectangle(Frame frame);
    public abstract ExampleRectangle createExampleRectangle(Frame frame);
}

