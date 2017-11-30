package app.model;

import org.springframework.data.annotation.Id;
import java.util.Date;

import org.springframework.data.annotation.PersistenceConstructor;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.format.annotation.DateTimeFormat.ISO;

public class Appointment {


    @Id
    private String id;
    private String person;
    private String mentor;
    @DateTimeFormat(iso = ISO.DATE_TIME)
    private Date startingDateTime;
    @DateTimeFormat(iso = ISO.DATE_TIME)
    private Date endingDateTime;

    public Appointment(){}

    @PersistenceConstructor
    public Appointment(String person, String mentor, Date startingDateTime, Date endingDateTime) {
        this.id = id;
        this.person = person;
        this.mentor = mentor;
        this.startingDateTime = startingDateTime;
        this.endingDateTime = endingDateTime;
    }

    public Date getEndingDateTime() {
        return endingDateTime;
    }

    public void setEndingDateTime(Date endingDateTime) {
        this.endingDateTime = endingDateTime;
    }

    public Date getStartingDateTime() {
        return startingDateTime;
    }

    public void setStartingDateTime(Date startingDateTime) {
        this.startingDateTime = startingDateTime;
    }


    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getPerson() {
        return person;
    }

    public void setPerson(String person) {
        this.person = person;
    }

    public String getMentor() {
        return mentor;
    }

    public void setMentor(String mentor) {
        this.mentor = mentor;
    }
}
