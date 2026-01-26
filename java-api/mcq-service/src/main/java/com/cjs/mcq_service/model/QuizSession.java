package com.cjs.mcq_service.model;

import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.AllArgsConstructor;

import java.time.LocalDateTime;
import java.util.List;

@Entity
@Table(name = "quiz_sessions")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class QuizSession {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    
    @Column(name = "user_id")
    private Long userId;
    
    private Integer totalQuestions;
    
    private Integer correctAnswers = 0;
    
    private Integer incorrectAnswers = 0;
    
    private Double scorePercentage = 0.0;
    
    @Column(name = "started_at")
    private LocalDateTime startedAt = LocalDateTime.now();
    
    @Column(name = "completed_at")
    private LocalDateTime completedAt;
    
    private Boolean isCompleted = false;
    
    @OneToMany(mappedBy = "quizSession", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<McqAttempt> attempts;
}