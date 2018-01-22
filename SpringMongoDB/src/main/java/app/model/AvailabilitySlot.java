package app.model;

import javax.print.attribute.standard.MediaSize;
import java.util.Date;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.format.annotation.DateTimeFormat.ISO;
import org.springframework.data.annotation.PersistenceConstructor;
import org.springframework.data.annotation.Id;


public class AvailabilitySlot {

    public String getMentor() {
        return mentor;
    }

    public void setMentor(String mentor) {
        this.mentor = mentor;
    }

    public Date getStartingDateTime() {
        return startingDateTime;
    }

    public void setStartingDateTime(Date startingDateTime) {
        this.startingDateTime = startingDateTime;
    }

    public Date getEndingDateTime() {
        return endingDateTime;
    }

    public void setEndingDateTime(Date endingDateTime) {
        this.endingDateTime = endingDateTime;
    }

    public boolean isAvailable() {
        return available;
    }

    public void setAvailable(boolean available) {
        this.available = available;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    @Id private String id;
    private String mentor;
    @DateTimeFormat(iso =ISO.DATE_TIME)
    private Date startingDateTime;
    @DateTimeFormat(iso = ISO.DATE_TIME)
    private Date endingDateTime;
    private boolean available;

    public AvailabilitySlot(){}

    @PersistenceConstructor
    public AvailabilitySlot(String mentor, Date startingDateTime, Date endingDateTime, Boolean available) {
        this.mentor = mentor;
        this.startingDateTime = startingDateTime;
        this.endingDateTime = endingDateTime;
        this.available = available;
    }
}
