package com.cjs.mcq_service.repository;

import com.cjs.mcq_service.model.QuizSession;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface QuizSessionRepository extends JpaRepository<QuizSession, Long> {
    List<QuizSession> findByUserIdOrderByStartedAtDesc(Long userId);
}