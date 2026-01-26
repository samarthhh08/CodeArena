package com.cjs.mcq_service.dto.response;

import lombok.Data;
import java.time.LocalDateTime;

@Data
public class McqQuestionDto {
    private Long id;
    private String question;
    private String optionA;
    private String optionB;
    private String optionC;
    private String optionD;
    private String category;
    private String difficulty;
    private LocalDateTime createdAt;
}