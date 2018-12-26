package RealWork;

import java.util.Hashtable;
import java.util.Vector;

public interface ObserverSubject {
    //增加一个观察者
    public void addObserver(Observer o);
    //删除一个观察者
    public void delObserver(Observer o);
    //通知所有观察者
    public void notifyObservers(double x, double y, int sign);
}
