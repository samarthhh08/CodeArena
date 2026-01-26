package com.cjs.mcq_service.dto.request;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.Data;

@Data
public class SubmitMcqAnswerDto {
    
    @NotNull
    private Long questionId;
    
    @NotBlank
    private String selectedOption; // A, B, C, or D
    
    @NotNull
    private Long quizSessionId;
}