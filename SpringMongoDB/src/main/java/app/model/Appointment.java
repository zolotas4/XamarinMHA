package app.model;

import org.springframework.data.annotation.Id;
import java.util.Date;

import org.springframework.data.annotation.PersistenceConstructor;
import org.springframework.format.annotation.DateTimeFormat;

public class Appointment {

    @Id
    private String id;
    private String person;
    private String mentor;
    @DateTimeFormat(pattern = "yyyy-MM-dd")
    private Date date;
    private int slotNumber;

    public int getSlotNumber() {
        return slotNumber;
    }

    public void setSlotNumber(int slotNumber) {
        this.slotNumber = slotNumber;
    }


    public Appointment(){}

    @PersistenceConstructor
    public Appointment(String person, String mentor, Date date, int slotNumber) {
        this.id = id;
        this.person = person;
        this.mentor = mentor;
        this.date = date;
        this.slotNumber = slotNumber;
    }


    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
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