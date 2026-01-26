package com.cjs.mcq_service.repository;

import com.cjs.mcq_service.model.McqQuestion;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface McqQuestionRepository extends JpaRepository<McqQuestion, Long> {
    
    List<McqQuestion> findByCategory(String category);
    
    List<McqQuestion> findByCategoryAndDifficulty(String category, String difficulty);
    
    @Query("SELECT DISTINCT m.category FROM McqQuestion m")
    List<String> findDistinctCategories();
    
    @Query(value = "SELECT * FROM mcq_questions WHERE (:category IS NULL OR category = :category) " +
                   "AND (:difficulty IS NULL OR difficulty = :difficulty) " +
                   "ORDER BY RAND() LIMIT :limit", nativeQuery = true)
    List<McqQuestion> findRandomQuestions(String category, String difficulty, int limit);
}