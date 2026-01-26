package com.cjs.mcq_service.model;

import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.AllArgsConstructor;

import java.time.LocalDateTime;

@Entity
@Table(name = "mcq_attempts")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class McqAttempt {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(name = "user_id")
    private Long userId;
    
    @Column(name = "quiz_session_id")
    private Long quizSessionId;
    
    @Column(name = "question_id")
    private Long questionId;
    
    private String selectedOption; // A, B, C, or D
    
    private Boolean isCorrect;
    
    @Column(name = "attempted_at")
    private LocalDateTime attemptedAt = LocalDateTime.now();
    
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "question_id", insertable = false, updatable = false)
    private McqQuestion question;
    
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "quiz_session_id", insertable = false, updatable = false)
    private QuizSession quizSession;
}