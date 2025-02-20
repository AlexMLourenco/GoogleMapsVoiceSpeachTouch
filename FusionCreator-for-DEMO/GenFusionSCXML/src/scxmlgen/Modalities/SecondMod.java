package scxmlgen.Modalities;

import scxmlgen.interfaces.IModality;

/**
 *
 * @author nunof
 */
public enum SecondMod implements IModality{

    CAR("[transport][DRIVING]",1500),
    BUS("[transport][TRANSIT]",1500),
    FOOT("[transport][WALKING]",1500),
    BIKE("[transport][BICYCLING]",1500),
    BLUE("[color][BLUE]",1500),
    YELLOW("[color][YELLOW]",1500),
    RED("[color][RED]",1500);
        
    private String event;
    private int timeout;


    SecondMod(String m, int time) {
        event=m;
        timeout=time;
    }

    @Override
    public int getTimeOut() {
        return timeout;
    }

    @Override
    public String getEventName() {
        //return getModalityName()+"."+event;
        return event;
    }

    @Override
    public String getEvName() {
        return getModalityName().toLowerCase()+event.toLowerCase();
    }
    
}
