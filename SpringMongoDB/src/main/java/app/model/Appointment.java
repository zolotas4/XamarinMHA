package app.model;

import org.springframework.data.annotation.Id;
import java.util.Date;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.format.annotation.DateTimeFormat.ISO;

public class Appointment {


    @Id private String id;
    private String userId;
    @DateTimeFormat(iso = ISO.DATE_TIME) private Date startingDateTime;

    public Date getEndingDateTime() {
        return endingDateTime;
    }

    public void setEndingDateTime(Date endingDateTime) {
        this.endingDateTime = endingDateTime;
    }

    @DateTimeFormat(iso = ISO.DATE_TIME) private Date endingDateTime;

    public Date getStartingDateTime() {
        return startingDateTime;
    }

    public void setStartingDateTime(Date startingDateTime) {
        this.startingDateTime = startingDateTime;
    }

    public String getUser_id() {
        return userId;
    }

    public void setUser_id(String user_id) {
        this.userId = userId;
    }
}
