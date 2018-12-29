package RealWork;

import java.util.Hashtable;

public interface Observer {
    public void update(double x, double y, int sign);
    public void update_get(Hashtable hashtable, int start, int end);
}
