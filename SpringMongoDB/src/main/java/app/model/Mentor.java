package app.model;

import com.mongodb.gridfs.GridFSDBFile;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.DBRef;

import java.awt.Image;

public class Mentor {

    @Id
    private String id;

    private String firstName;
    private String lastName;
    private String userName;
    private String password;
    private String email;
    @DBRef
    private Photo photo;

    public Mentor() {}

    public Mentor(String firstName, String lastName, String userName, String password, String email, Photo photo) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.userName = userName;
        this.password = password;
        this.email = email;
        this.photo = photo;
    }

    public Photo getPhoto() {return  photo; }

    public void setPhoto(Photo photo) { this.photo = photo; }

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