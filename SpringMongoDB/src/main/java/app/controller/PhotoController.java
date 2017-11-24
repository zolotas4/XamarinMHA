package app.controller;

import org.apache.tomcat.util.http.fileupload.IOUtils;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.multipart.MultipartFile;

import java.io.*;
import java.nio.file.Files;

@Controller
@RequestMapping("/photo")
public class PhotoController {

    @RequestMapping(value="/upload/{username}/", method=RequestMethod.POST)
    @ResponseBody
    public String uploadFile(@PathVariable("username") String username, @PathVariable("file") MultipartFile file){
        String name = username;
        if (!file.isEmpty()) {
            try {
                byte[] bytes = file.getBytes();
                BufferedOutputStream stream =
                        new BufferedOutputStream(new FileOutputStream(new File(name + ".jpeg")));
                stream.write(bytes);
                stream.close();
                System.out.println("You successfully uploaded " + name + " into " + name + "-uploaded !");
                return "You successfully uploaded " + name + " into " + name + "-uploaded !";
            } catch (Exception e) {
                System.out.println("You failed to upload " + name + " => " + e.getMessage());
                return "You failed to upload " + name + " => " + e.getMessage();
            }
        } else {
            System.out.println("You failed to upload " + name + " because the file was empty.");
            return "You failed to upload " + name + " because the file was empty.";
        }
    }

    @ResponseBody
    @RequestMapping(value = "/download/{username}", method = RequestMethod.GET, produces = MediaType.IMAGE_JPEG_VALUE)
    public ResponseEntity<byte[]> getPhoto(@PathVariable("username") String username) throws IOException {
        String filename = username;
        File fi = new File(username + ".jpeg");
        byte[] image = Files.readAllBytes(fi.toPath());
        return ResponseEntity.ok().contentType(MediaType.IMAGE_JPEG).body(image);
    }

}
