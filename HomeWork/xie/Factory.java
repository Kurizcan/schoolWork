package RealWork;

import java.awt.*;

public class Factory extends AbstractFactory {
    @Override
    public TextRectangle createTextRectangle(Frame frame) {
        return new TextRectangle(frame);
    }

    @Override
    public DepthRectangle createDepthRectangle(Frame frame) {
        return new DepthRectangle(frame);
    }

    @Override
    public GreenRectangle createGreenRectangle(Frame frame) { return new GreenRectangle(frame);
    }
    @Override
    public ExampleRectangle createExampleRectangle(Frame frame) {
        return new ExampleRectangle(frame);
    }
}
