package com.cjs.mcq_service.dto.response;

import lombok.Data;

@Data
public class McqAttemptDetailDto {
    private Long questionId;
    private String question;
    private String selectedOption;
    private String correctOption;
    private Boolean isCorrect;
    private String explanation;
}