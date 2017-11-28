package app.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.FileSystemResource;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.mail.javamail.JavaMailSender;
import org.springframework.mail.javamail.MimeMessageHelper;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.mail.MessagingException;
import javax.mail.internet.MimeMessage;
import java.io.File;

@Controller
@RequestMapping("/email")
public class EmailController {

    @Autowired
    public JavaMailSender emailSender;

    @RequestMapping(value="/send/{email}/{firstlastname}/", method= RequestMethod.GET)
    @ResponseBody
    public String uploadFile(@PathVariable("email") String emailaddress, @PathVariable("firstlastname") String firstlastname) throws MessagingException {
        String name = firstlastname;
        String emailAddress = emailaddress;
        MimeMessage message = emailSender.createMimeMessage();
        MimeMessageHelper helper = new MimeMessageHelper(message, true);

        helper.setTo(emailAddress);
        helper.setSubject("Welcome to MHA");
        helper.setText("<html><body>Dear " + firstlastname + ", <br/><br/> Fill the attached form and submit it using the application. <br/><br/> Cheers,<br/>MHA Admin Team</body></html>", true);
        FileSystemResource file
                = new FileSystemResource(new File("files/square.pdf"));
        helper.addAttachment("VerificationForm.pdf", file);

        emailSender.send(message);
        return "Message sent";
    }
}
