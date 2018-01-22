package app.model;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;
import org.springframework.data.repository.query.Param;


import java.util.Date;
import java.util.List;

@RepositoryRestResource(collectionResourceRel = "availability", path = "availability")
public interface AvailabilitySlotsRepository extends MongoRepository<AvailabilitySlot, String> {
    List<AvailabilitySlot> findByMentor(@Param("mentor") String mentor);
    List<AvailabilitySlot> findByMentorAndAvailable(@Param("mentor") String mentor, @Param("available") String available);
    //@Query("{ 'mentor' : ?0 , 'startingDateTime' : {$gt: ?1, $lt: ?2}}")
    //List<AvailabilitySlot> findByMentorBetweenDates(@Param("mentor") String mentor, @Param("date1") @DateTimeFormat(iso = ISO.DATE_TIME) Date date1, @Param("date2") @DateTimeFormat(iso = ISO.DATE_TIME) Date date2);
}
