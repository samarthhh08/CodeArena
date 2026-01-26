package com.cjs.mcq_service.dto.request;

import jakarta.validation.constraints.NotBlank;
import lombok.Data;

@Data
public class CreateMcqDto {
    
    @NotBlank
    private String question;
    
    @NotBlank
    private String optionA;
    
    @NotBlank
    private String optionB;
    
    @NotBlank
    private String optionC;
    
    @NotBlank
    private String optionD;
    
    @NotBlank
    private String correctOption; // A, B, C, or D
    
    @NotBlank
    private String correctExplanation;
    
    private String incorrectExplanationA;
    private String incorrectExplanationB;
    private String incorrectExplanationC;
    private String incorrectExplanationD;
    
    private String category = "General";
    private String difficulty = "Medium";
}