package com.cjs.mcq_service.dto.response;

import lombok.Data;

@Data
public class McqAttemptResultDto {
    private Boolean isCorrect;
    private String correctOption;
    private String explanation;
    private QuizSessionDto quizSession;
}