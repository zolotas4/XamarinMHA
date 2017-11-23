package app.model;

import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

import java.util.List;

@RepositoryRestResource(collectionResourceRel = "mentors", path = "mentors")
public interface MentorRepository extends MongoRepository<Mentor, String> {

    List<Mentor> findByLastName(@Param("name") String name);
    Mentor findByUserName(@Param("username") String username);
}
