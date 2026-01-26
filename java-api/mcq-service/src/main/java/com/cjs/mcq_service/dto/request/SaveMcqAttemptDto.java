package com.cjs.mcq_service.dto.request;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.Data;

@Data
public class SaveMcqAttemptDto {
    @NotNull
    private Long questionId;
    
    @NotNull
    private Long quizSessionId;
    
    @NotBlank
    private String selectedOption;
    
    @NotNull
    private Boolean isCorrect;
}