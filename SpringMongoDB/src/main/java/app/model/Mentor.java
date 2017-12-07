package app.model;

import org.springframework.data.annotation.Id;
import org.springframework.data.annotation.PersistenceConstructor;

public class Mentor {

    @Id
    private String id;
    private String firstName;
    private String lastName;
    private String userName;
    private String password;
    private String email;
    private int startSlot;
    private int endSlot;

    public Mentor() {}

    public int getStartSlot() {
        return startSlot;
    }

    public void setStartSlot(int startSlot) {
        this.startSlot = startSlot;
    }

    public int getEndSlot() {
        return endSlot;
    }

    public void setEndSlot(int endSlot) {
        this.endSlot = endSlot;
    }

    @PersistenceConstructor
    public Mentor(String firstName, String lastName, String userName, String password, String email, int startSlot, int endSlot) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.userName = userName;
        this.password = password;
        this.email = email;
        this.startSlot = startSlot;
        this.endSlot = endSlot;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getPassword() { return password; }

    public void setPassword(String password) { this.password = password; }

    public String getEmail() { return email; }

    public void setEmail(String email) { this.email = email; }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getUserName() { return userName; }

    public void setUserName(String userName) { this.userName = userName; }
}