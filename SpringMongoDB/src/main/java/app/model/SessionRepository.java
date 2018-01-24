package app.model;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

import java.util.Date;
import java.util.List;


        import org.springframework.data.mongodb.repository.MongoRepository;
        import org.springframework.data.mongodb.repository.Query;
        import org.springframework.data.repository.query.Param;
        import org.springframework.data.rest.core.annotation.RepositoryRestResource;
        import org.springframework.format.annotation.DateTimeFormat;

        import java.util.Date;
        import java.util.List;

@RepositoryRestResource(collectionResourceRel = "sessions", path = "sessions")
public interface SessionRepository extends MongoRepository<Session, String> {
    List<Session> findByPerson(@Param("person") String person);
    @Query("{ 'mentor' : ?0 , 'date' : {$lt: ?1}}")
    List<Session> findSessionsBeforeToday(@Param("mentor") String mentor, @Param("date") @DateTimeFormat(pattern = "yyyy-MM-dd") Date date);
    @Query("{ 'person' : ?0 , 'date' : {$gt: ?1}}")
    List<Appointment> findByPersonGreaterThanOrderByDateDesc(@Param("person") String person, @Param("date") @DateTimeFormat(pattern = "yyyy-MM-dd") Date date);
}
