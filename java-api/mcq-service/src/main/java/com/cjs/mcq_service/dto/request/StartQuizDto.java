package com.cjs.mcq_service.dto.request;

import lombok.Data;

@Data
public class StartQuizDto {
    private Integer questionCount = 10;
    private String category;
    private String difficulty;
}