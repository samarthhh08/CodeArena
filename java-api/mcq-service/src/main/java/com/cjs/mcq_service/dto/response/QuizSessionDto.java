package com.cjs.mcq_service.dto.response;

import lombok.Data;
import java.time.LocalDateTime;

@Data
public class QuizSessionDto {
    private Long id;
    private Integer totalQuestions;
    private Integer correctAnswers;
    private Integer incorrectAnswers;
    private Double scorePercentage;
    private LocalDateTime startedAt;
    private LocalDateTime completedAt;
    private Boolean isCompleted;
}