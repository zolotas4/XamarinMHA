package app.model;

import java.util.Date;

        import org.springframework.data.annotation.Id;
        import java.util.Date;

        import org.springframework.data.annotation.PersistenceConstructor;
        import org.springframework.format.annotation.DateTimeFormat;

public class Session {
    @Id
    private String id;
    private String person;
    private String mentor;
    @DateTimeFormat(pattern = "yyyy-MM-dd")
    private Date date;
    private int startingSlotNumber;
    private int duration;

    public int getActualDuration() {
        return actualDuration;
    }

    public void setActualDuration(int actualDuration) {
        this.actualDuration = actualDuration;
    }

    private int actualDuration;
    private boolean logged;
    private String comments;

    public int getDuration() {
        return duration;
    }

    public void setDuration(int duration) {
        this.duration = duration;
    }

    public boolean isLogged() {
        return logged;
    }

    public void setLogged(boolean logged) {
        this.logged = logged;
    }

    public String getComments() {
        return comments;
    }

    public void setComments(String comments) {
        this.comments = comments;
    }


    public int getStartingSlotNumber() {
        return startingSlotNumber;
    }

    public void setStartingSlotNumber(int startingSlotNumber) {
        this.startingSlotNumber = startingSlotNumber;
    }


    public Session(){}

    @PersistenceConstructor
    public Session(String person, String mentor, Date date, int startingSlotNumber, int duration) {
        this.id = id;
        this.person = person;
        this.mentor = mentor;
        this.date = date;
        this.startingSlotNumber = startingSlotNumber;
        this.duration = duration;
        this.logged = false;
        this.comments = "";
        this.actualDuration = 0;
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