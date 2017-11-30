package app.model;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.format.annotation.DateTimeFormat.ISO;

import java.util.Date;
import java.util.List;

@RepositoryRestResource(collectionResourceRel = "appointments", path = "appointments")
public interface AppointmentRepository extends MongoRepository<Appointment, String> {
    List<Appointment> findByMentorGreaterThanOrderByStartingDateTimeDesc(@Param("mentor") String mentor, @Param("startingDateTime") @DateTimeFormat(iso = ISO.DATE_TIME) Date startingDateTime);
    List<Appointment> findByStartingDateTime(@Param("startingDateTime") @DateTimeFormat(iso = ISO.DATE_TIME)  Date startingDateTime);
    List<Appointment> findByMentorOrderByStartingDateTimeAsc(@Param("mentor") String mentor);
    List<Appointment> findByPerson(@Param("person") String person);
    @Query("{ 'mentor' : ?0 , 'startingDateTime' : {$gt: ?1}}")
    List<Appointment> findAppointmentsFromNowOn(@Param("mentor") String mentor, @Param("date") @DateTimeFormat(iso = ISO.DATE_TIME) Date date);
}
