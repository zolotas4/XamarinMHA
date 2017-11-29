package app.model;

import org.springframework.data.annotation.Id;
import java.util.Date;

import org.springframework.data.mongodb.core.mapping.DBRef;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.format.annotation.DateTimeFormat.ISO;

public class Appointment {

    @Id
    private String id;
    @DBRef
    private Person person;
    @DBRef
    private Mentor mentor;
    @DateTimeFormat(iso = ISO.DATE_TIME)
    private Date startingDateTime;
    @DateTimeFormat(iso = ISO.DATE_TIME)
    private Date endingDateTime;

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

    public Person getPerson() {
        return person;
    }

    public void setPerson(Person person) {
        this.person = person;
    }

    public Mentor getMentor() {
        return mentor;
    }

    public void setMentor(Mentor mentor) {
        this.mentor = mentor;
    }
}
