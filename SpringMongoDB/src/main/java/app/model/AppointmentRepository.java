package app.model;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;
import org.springframework.format.annotation.DateTimeFormat;

import java.util.Date;
import java.util.List;

@RepositoryRestResource(collectionResourceRel = "appointments", path = "appointments")
public interface AppointmentRepository extends MongoRepository<Appointment, String> {
    List<Appointment> findByMentorGreaterThanOrderByDateDesc(@Param("mentor") String mentor, @Param("date") @DateTimeFormat(pattern = "yyyy-MM-dd") Date startingDateTime);
    List<Appointment> findByDate(@Param("date") @DateTimeFormat(pattern = "yyyy-MM-dd")  Date date);
    List<Appointment> findByMentorAndDateBetween(@Param("mentor") String mentor, @Param("date1") @DateTimeFormat(pattern = "yyyy-MM-dd") Date date1, @Param("date2") @DateTimeFormat(pattern = "yyyy-MM-dd") Date date2);
    List<Appointment> findByMentorAndDate(@Param("mentor") String mentor, @Param("date") @DateTimeFormat(pattern = "yyyy-MM-dd") Date date);
    List<Appointment> findByMentorOrderByDateAsc(@Param("mentor") String mentor);
    List<Appointment> findByPerson(@Param("person") String person);
    @Query("{ 'mentor' : ?0 , 'date' : {$gt: ?1}}")
    List<Appointment> findAppointmentsFromNowOn(@Param("mentor") String mentor, @Param("date") @DateTimeFormat(pattern = "yyyy-MM-dd") Date date);
    @Query("{ 'person' : ?0 , 'date' : {$gt: ?1}}")
    List<Appointment> findByPersonGreaterThanOrderByDateDesc(@Param("person") String person, @Param("date") @DateTimeFormat(pattern = "yyyy-MM-dd") Date date);
}
